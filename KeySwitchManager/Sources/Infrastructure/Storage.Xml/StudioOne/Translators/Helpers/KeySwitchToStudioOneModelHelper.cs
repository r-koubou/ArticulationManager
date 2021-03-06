using System.Collections.Generic;
using System.Linq;
using System.Text;

using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Domain.KeySwitches.Models.Aggregations;
using KeySwitchManager.Domain.KeySwitches.Models.Values;
using KeySwitchManager.Domain.KeySwitches.Models.Values.Extensions;
using KeySwitchManager.Domain.MidiMessages.Models.Values;
using KeySwitchManager.Infrastructure.Storage.Xml.StudioOne.Models;

namespace KeySwitchManager.Infrastructure.Storage.Xml.StudioOne.Translators.Helpers
{
    internal static class KeySwitchToStudioOneModelHelper
    {
        public static RootElement Translate( KeySwitch source )
        {
            var rootElement = new RootElement
            {
                Name = $"{source.ProductName.Value} {source.InstrumentName.Value}"
            };

            var id = 0;

            foreach( var i in source.Articulations )
            {
                if( !i.MidiNoteOns.Any() )
                {
                    continue;
                }

                var attr = TranslateElementAttribute( i, id );
                id++;

                rootElement.Attributes.Add( attr );
            }

            return rootElement;
        }

        #region Translate Attribute
        private static ElementAttribute TranslateElementAttribute( Articulation articulation, int id )
        {
            var name = articulation.ArticulationName.Value;
            var pitch = articulation.MidiNoteOns[ 0 ].DataByte1.Value;
            var activation = TranslateActivation( articulation );

            string color = default!;

            if( articulation.ExtraData.ContainsKey( ExtraDataKeys.Color ) )
            {
                color = articulation.ExtraData[ ExtraDataKeys.Color ].Value;
            }

            var momentary = articulation.ExtraData.GetValueOrDefault(
                ExtraDataKeys.Momentary, new ExtraDataValue( "0" )
            ).Value == "0" ? 0 : 1;

            return new ElementAttribute( name, id, color, pitch, momentary, activation );
        }

        #region Translate Activations
        private static string TranslateActivation( Articulation articulation )
        {
            var activations = new List<string>();
            var sb = new StringBuilder( 128 );

            activations.AddRange(  TranslateActivationNote( articulation ) );
            activations.AddRange( TranslateActivationControlChange( articulation ) );
            activations.AddRange( TranslateActivationProgramChange( articulation ) );
            activations.AddRange( TranslateActivationBankChange( articulation ) );

            activations = activations.Distinct().ToList();

            var count = activations.Count;
            for( var i = 0; i < count; i++ )
            {
                var x = activations[ i ];
                sb.Append( x );

                if( i < count - 1 )
                {
                    sb.Append( '|' );
                }
            }

            return sb.ToString();
        }

        private static IReadOnlyList<string> TranslateActivationNote( Articulation articulation )
        {
            var result = new List<string>();

            foreach( var x in articulation.MidiNoteOns )
            {
                var byte1 = x.DataByte1.Value;
                var byte2 = x.DataByte2.Value;
                result.Add( $"note{byte1}.{byte2}" );
            }

            #region Convert from Extra keys
            void TranslateExtraData( string prefix, ExtraDataKey k )
            {

                articulation.ExtraData.KeyWithIndexCount( k, ( k, v, i ) =>
                {
                    var values = v.Value.Split( ExtraDataKeys.ValueSeparator );
                    var note = new MidiNoteName( values[ 0 ].Trim() ).ToMidiNoteNumber().Value;
                    var velocity = int.Parse( values[ 1 ].Trim() );
                    result.Add( $"{prefix}{note}.{velocity}" );

                });
            }

            TranslateExtraData( "note", ExtraDataKeys.NoteOnOff );
            TranslateExtraData( "on", ExtraDataKeys.NoteOn );
            TranslateExtraData( "off", ExtraDataKeys.NoteOff );
            #endregion

            return result;
        }

        private static IReadOnlyCollection<string> TranslateActivationControlChange( Articulation articulation )
        {
            var result = new List<string>();

            foreach( var x in articulation.MidiControlChanges )
            {
                var ccNo = x.Status.Value;
                var byte1 = x.DataByte1.Value;
                var byte2 = x.DataByte2.Value;

                if( ccNo is 0 or 32 )
                {
                    result.Add($"bc{byte1}.{byte2}" );
                }
                else
                {
                    result.Add( $"cc{byte1}.{byte2}" );
                }

            }

            return result;
        }

        private static IReadOnlyCollection<string> TranslateActivationProgramChange( Articulation articulation )
        {
            var result = new List<string>();

            foreach( var x in articulation.MidiProgramChanges )
            {
                var byte1 = x.DataByte1.Value;
                result.Add( $"pc{byte1}" );
            }

            return result;
        }

        private static IReadOnlyCollection<string> TranslateActivationBankChange( Articulation articulation )
        {
            var result = new List<string>();

            #region Convert from Extra keys

            articulation.ExtraData.KeyWithIndexCount( ExtraDataKeys.Bank, ( k, v, i ) =>
            {
                var values = v.Value.Split( ExtraDataKeys.ValueSeparator );
                var data1 = int.Parse( values[ 0 ].Trim() );
                var data2 = int.Parse( values[ 1 ].Trim() );
                result.Add( $"bc{data1}.{data2}" );
            });
            #endregion

            return result;
        }
        #endregion
        #endregion

    }
}