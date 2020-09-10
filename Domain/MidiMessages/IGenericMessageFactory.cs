using ArticulationManager.Domain.MidiMessages.Aggregate;
using ArticulationManager.Domain.MidiMessages.Value;

namespace ArticulationManager.Domain.MidiMessages
{
    public interface IGenericMessageFactory : IMidiMessageFactory
    {
        public GenericMessage Create( int status, int data1, int data2 );

        public class DefaultFactory : IGenericMessageFactory
        {
            public IMessage Create( int status, int channel, int data1, int data2 )
            {
                return new GenericMessage(
                    new StatusCode( status ),
                    new MidiChannel( channel ),
                    new GenericData( data1 ),
                    new GenericData( data2 )
                );
            }

            public GenericMessage Create( int status, int data1, int data2 )
            {
                return new GenericMessage(
                    new StatusCode( status ),
                    MidiChannel.Zero,
                    new GenericData( data1 ),
                    new GenericData( data2 )
                );
            }
        }
    }
}