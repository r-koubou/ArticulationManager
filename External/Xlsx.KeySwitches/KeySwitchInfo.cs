using KeySwitchManager.Common.Text;

namespace KeySwitchManager.Xlsx.KeySwitches
{
    public class KeySwitchInfo
    {
        public string DeveloperName { get; }
        public string ProductName { get; }
        public string Author { get; }
        public string Description { get; }

        public KeySwitchInfo(
            string developerName,
            string productName,
            string author = "",
            string description = "" )
        {
            StringHelper.ValidateNullOrTrimEmpty( developerName );
            StringHelper.ValidateNullOrTrimEmpty( productName );
            StringHelper.ValidateNull( author );
            StringHelper.ValidateNull( description );

            DeveloperName = developerName;
            ProductName   = productName;
            Author        = author;
            Description   = description;
        }
    }
}