using System;
using System.Collections.Generic;

using ClosedXML.Excel;

using KeySwitchManager.Domain.KeySwitches.Midi.Models.Entities;

namespace KeySwitchManager.Infrastructure.Storage.Spreadsheet.ClosedXml.KeySwitches.Translators
{
    [Flags]
    internal enum TranslateMidiMessageType
    {
        Data1 = 0x1,
        Data2 = 0x2,
        Data3 = 0x4,
    }

    internal class MidiMessageCellInfo
    {
        public string HeaderCellName { get; }
        public int MidiDataValue { get; }

        public MidiMessageCellInfo( string headerCellName, int midiDataValue )
        {
            HeaderCellName = headerCellName;
            MidiDataValue  = midiDataValue;
        }
    }

    internal interface IMidiMessageTranslator
    {
        int Translate(
            IEnumerable<IMidiMessage> midiMessages,
            IXLWorksheet sheet,
            int headerRow,
            int row,
            string midiData1HeaderName,
            string midiData2HeaderName,
            string midiData3HeaderName,
            TranslateMidiMessageType type );
    }
}