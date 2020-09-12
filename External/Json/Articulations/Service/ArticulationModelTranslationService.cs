using System.Collections.Generic;

using ArticulationManager.Domain.Articulations;
using ArticulationManager.Domain.Articulations.Aggregate;
using ArticulationManager.Domain.MidiMessages;
using ArticulationManager.Domain.MidiMessages.Aggregate;
using ArticulationManager.Domain.Services;
using ArticulationManager.Json.Articulations.Model;

namespace ArticulationManager.Json.Articulations.Service
{
    public class ArticulationModelTranslationService : IDataTranslationService<ArticulationModel, Articulation>
    {
        public Articulation Translate( ArticulationModel source )
        {
            List<IMessage> noteOn = new List<IMessage>();
            List<IMessage> controlChange = new List<IMessage>();
            List<IMessage> programChange = new List<IMessage>();

            ConvertMessageList( source.NoteOn,        noteOn,        new INoteOnFactory.DefaultFactory() );
            ConvertMessageList( source.ControlChange, controlChange, new IControlChangeFactory.DefaultFactory() );
            ConvertMessageList( source.ProgramChange, programChange, new IProgramChangeFactory.DefaultFactory() );

            return new IArticulationFactory.DefaultFactory().Create(
                source.Id,
                source.Created,
                source.LastUpdated,
                source.DeveloperName,
                source.ProductName,
                source.ArticulationName,
                source.ArticulationType,
                source.ArticulationGroup,
                source.ArticulationColor,
                noteOn,
                controlChange,
                programChange
            );
        }

        private static void ConvertMessageList(
            IEnumerable<MidiMessageModel> src,
            List<IMessage> dest,
            IMidiMessageFactory messageFactory )
        {
            foreach( var i in src )
            {
                dest.Add(
                    messageFactory.Create(
                        i.Status,
                        i.Channel,
                        i.DataByte1,
                        i.DataByte2
                    )
                );
            }
        }
    }
}