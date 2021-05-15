using System.Collections.Generic;
using System.IO;
using System.Text;

using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Infrastructure.Storage.Json.KeySwitches.Translators;

namespace KeySwitchManager.Infrastructure.Storage.Json.KeySwitches.Helpers
{
    public static class KeySwitchFileWriter
    {
        public static void Write( Stream stream, IReadOnlyCollection<KeySwitch> keySwitches, Encoding encoding )
        {
            using var writer = new StreamWriter( stream, encoding );
            var jsonText = new KeySwitchExportTranslator().Translate( keySwitches );

            writer.WriteLine( jsonText );
        }

        public static void Write( Stream stream, IReadOnlyCollection<KeySwitch> keySwitches )
        {
            Write( stream, keySwitches, Encoding.UTF8 );
        }
    }
}