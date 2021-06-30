using CommandLine;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Infrastructures.Database.LiteDB.KeySwitches;
using KeySwitchManager.Infrastructures.Storage.Spreadsheet.ClosedXml.KeySwitches;
using KeySwitchManager.Interactors.KeySwitches;
using KeySwitchManager.UseCase.KeySwitches.Import.Spreadsheet;

namespace KeySwitchManager.Applications.CLI.Commands
{
    public class ImportXlsx : ICommand
    {
        [Verb( "import-xlsx", HelpText = "import a xlsx to database")]
        public class CommandOption : ICommandOption
        {
            [Option( 'f', "database", Required = true )]
            public string DatabasePath { get; set; } = string.Empty;

            [Option( 'i', "input", Required = true )]
            public string InputPath { get; set; } = string.Empty;
        }

        public int Execute( ICommandOption opt )
        {
            var option = (CommandOption)opt;

            using var repository = new LiteDbKeySwitchRepository( new FilePath( option.DatabasePath ) );
            using var inputRepository = new ClosedXmlFileLoadRepository( new FilePath( option.InputPath ) );

            var presenter = new IImportSpreadsheetPresenter.Console();
            var interactor = new ImportSpreadSheetInteractor( repository, inputRepository, presenter );

            var request = new ImportSpreadSheetRequest();
            _ = interactor.Execute( request );

            return 0;
        }
    }
}