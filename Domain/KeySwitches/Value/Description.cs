using System;

using KeySwitchManager.Common.Text;

namespace KeySwitchManager.Domain.KeySwitches.Value
{
    /// <summary>
    /// A description of keyswitch
    /// </summary>
    public class Description : IEquatable<Description>
    {
        public static readonly Description Empty = new Description( string.Empty );

        public string Value { get; }

        public Description( string name )
        {
            if( StringHelper.IsNullOrTrimEmpty( name ) )
            {
                name = string.Empty;
            }
            Value = name;
        }

        public bool Equals( Description? other )
        {
            return other != null && other.Value == Value;
        }

        public override string ToString() => Value;

    }
}