using KeySwitchManager.Commons.Data;
using KeySwitchManager.Domain.KeySwitches.Models;

namespace KeySwitchManager.Infrastructure.Storage.KeySwitches
{
    public abstract class KeySwitchFileRepository : OnMemoryKeySwitchRepository
    {
        public IPath DataPath { get; }

        protected KeySwitchFileRepository( IPath dataPath, bool loadFromPathNow )
        {
            DataPath = dataPath;

            if( DataPath.IsFile && loadFromPathNow )
            {
                // ReSharper disable once VirtualMemberCallInConstructor
                Load();
            }
        }

        public abstract override int Flush();
        public abstract void Load();
    }
}