using KeySwitchManager.Domain.KeySwitches.Midi.Models.Values;
using KeySwitchManager.Domain.KeySwitches.Models.Values;

namespace KeySwitchManager.Domain.KeySwitches.Midi.Models.Entities
{
    /// <summary>
    /// Represents a MIDI program change.
    /// </summary>
    public class MidiProgramChange : IMidiMessage
    {
        public IMidiMessageData Status { get; }
        public IMidiMessageData DataByte1 { get; }
        public IMidiMessageData DataByte2 { get; }

        public MidiProgramChange( MidiStatus status, MidiProgramChangeNumber number )
        {
            Status    = status;
            DataByte1 = number;
            DataByte2 = new GenericMidiData( 0 );
        }
    }
}