using Application.Core.Controllers.Export;

using CommandLine;

namespace KeySwitchManager.Applications.CLI.Commands
{
    public class ExportXlsx : ExportDawArticulation
    {
        [Verb( "xlsx", HelpText = "export a xlsx format from database")]
        public new class CommandOption : ExportDawArticulation.CommandOption
        {}

        protected override ExportSupportedFormat SupportedFormat => ExportSupportedFormat.Xlsx;
    }
}