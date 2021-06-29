﻿using KeySwitchManager.AppCore.Views.LogView;
using KeySwitchManager.UseCase.KeySwitches.Export.Daw;

namespace KeySwitchManager.AppCore.Controllers.Export
{
    public class ExportDawPresenter : IExportDawPresenter
    {
        private ILogTextView TextView { get; }

        public ExportDawPresenter( ILogTextView textView )
        {
            TextView = textView;
        }

        public void Present<T>( T param )
        {
            if( param != null )
            {
                TextView.Append( param.ToString() ?? string.Empty );
            }
        }

        public void Complete( ExportDawResponse response )
        {
            TextView.Append( "Complete" );
        }
    }
}
