namespace KeySwitchManager.Xlsx.KeySwitches.Services
{
    public static class SpreadsheetConstants
    {
        public static readonly string ColumnArticulationName = "Articulation Name";
        public static readonly string ColumnMidiNote = "MIDI Note";
        public static readonly string ColumnMidiVelocity = "Velocity";
        public static readonly string ColumnMidiCc = "CC No";
        public static readonly string ColumnMidiCcValue = "CC Value";
        public static readonly string ColumnProgramChangeChannel = "PC Channel";
        public static readonly string ColumnProgramChangeData = "PC Data";

        public static readonly string ExtraColumnPrefix = "Ext.";

        // Position of Output name cell
        public static readonly int RowOutputIndex = 1;
        public static readonly int ColumnOutputNameIndex = 0;
        // Position of Guid cell
        public static readonly int RowGuidIndex = 3;
        public static readonly int ColumnGuidIndex = 0;

        // row 4~23: Reserved for maintenance

        // Start of data entry row index (19==header)
        public static readonly int HeaderRowIndex = 24;
        public static readonly int StartRowIndex = 25;
    }
}