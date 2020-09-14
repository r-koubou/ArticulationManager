using System;
using System.Collections.Generic;

using ArticulationManager.Common.Testing;
using ArticulationManager.Domain.MidiMessages;
using ArticulationManager.Domain.MidiMessages.Aggregate;
using ArticulationManager.Json.Articulations.Translations;

using NUnit.Framework;

namespace Json.Testing
{
    [TestFixture]
    public class EntityToJsonTest
    {
        [Test]
        public void ConvertToJsonTest()
        {
            var midiNoteFactory = new INoteOnFactory.Default();
            var midiCcFactory = new IControlChangeFactory.Default();
            var midiPcFactory = new IProgramChangeFactory.Default();

            var articulation = TestDataGenerator.CreateArticulation(
                new List<NoteOn> { midiNoteFactory.Create( 1, 23 ) },
                new List<ControlChange> { midiCcFactory.Create( 2, 34 ) },
                new List<ProgramChange> { midiPcFactory.Create( 3, 45 ) }
            );

            var entity = TestDataGenerator.CreateKeySwitch( articulation );

            var translator = new EntityToJsonModel();
            var json = translator.Translate( entity );

            var translator2 = new JsonModelToEntity();
            var cmp = translator2.Translate( json );

            Assert.AreEqual( entity, cmp );

            Console.WriteLine( json );
        }
    }
}