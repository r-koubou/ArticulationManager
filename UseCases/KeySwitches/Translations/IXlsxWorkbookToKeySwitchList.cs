using System.Collections.Generic;

using KeySwitchManager.Domain.Commons;
using KeySwitchManager.Domain.KeySwitches.Aggregate;
using KeySwitchManager.Domain.Translations;

namespace KeySwitchManager.UseCases.KeySwitches.Translations
{
    public interface IXlsxWorkbookToKeySwitchList : IDataTranslator<FilePath, IReadOnlyCollection<KeySwitch>>
    {
        public string DeveloperName { get; }
        public string ProductName { get; }
        public string Author { get; }
        public string Description { get; }
    }
}