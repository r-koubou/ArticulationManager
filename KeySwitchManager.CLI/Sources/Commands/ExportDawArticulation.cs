using System;
using System.Linq;

using CommandLine;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Domain.KeySwitches.Helpers;
using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Infrastructure.Database.LiteDB.KeySwitches;
using KeySwitchManager.Interactor.KeySwitches;
using KeySwitchManager.UseCase.KeySwitches.Export.Daw;

namespace KeySwitchManager.CLI.Commands
{
    public abstract class ExportDawArticulation : ICommand
    {
        public class CommandOption : ICommandOption
        {
            [Option( 'q', "quiet" )]
            public bool Quiet { get; set; } = false;

            [Option( 'd', "developer" )]
            public string Developer { get; set; } = string.Empty;

            [Option( 'p', "product" )]
            public string Product { get; set; } = string.Empty;

            [Option( 'i', "instrument" )]
            public string Instrument { get; set; } = string.Empty;

            [Option( 'f', "database", Required = true )]
            public string DatabasePath { get; set; } = string.Empty;

            [Option( 'o', "outputdir", Required = true )]
            public string OutputDirectory { get; set; } = string.Empty;
        }

        public virtual int Execute( ICommandOption opt )
        {
            var option = (CommandOption)opt;

            using var repository = new LiteDbKeySwitchRepository( new FilePath( option.DatabasePath ) );

            var keySwitches = SearchHelper.Search( repository, option.Developer, option.Product, option.Instrument );

            if( !keySwitches.Any() )
            {
                if( !option.Quiet )
                {
                    Console.WriteLine( "records not found" );
                }

                return 0;
            }

            using var outputRepository = CreateOutputRepository( new DirectoryPath( option.OutputDirectory ) );

            IDawExportPresenter presenter = option.Quiet ?
                new IDawExportPresenter.Null() :
                new IDawExportPresenter.Console();

            var interactor = new DawExportInteractor( repository, outputRepository, presenter );

            var request = new DawExportRequest( option.Developer, option.Product, option.Instrument );
            var response = interactor.Execute( request );

            return response.Result ? 0 : 1;
        }

        public abstract IKeySwitchRepository CreateOutputRepository( DirectoryPath outputDirectory );
    }
}