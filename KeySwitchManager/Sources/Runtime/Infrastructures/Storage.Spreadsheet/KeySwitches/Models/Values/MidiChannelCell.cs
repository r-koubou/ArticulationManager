using ValueObjectGenerator;

namespace KeySwitchManager.Infrastructures.Storage.Spreadsheet.KeySwitches.Models.Values
{
    [ValueObject( typeof( int ) )]
    [ValueRange( MinValue, MaxValue )]
    public partial class MidiChannelCell
    {
        public const int MinValue = 0x00;
        public const int MaxValue = 0x0F;
    }
}