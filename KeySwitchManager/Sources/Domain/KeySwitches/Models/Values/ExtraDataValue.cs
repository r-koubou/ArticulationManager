using ValueObjectGenerator;

namespace KeySwitchManager.Domain.KeySwitches.Models.Values
{
    [ValueObject( typeof( string ) )]
    [NotEmpty]
    public partial class ExtraDataValue
    {
        public static readonly ExtraDataValue Empty = new ExtraDataValue();

        private ExtraDataValue()
        {
            Value = string.Empty;
        }
    }
}