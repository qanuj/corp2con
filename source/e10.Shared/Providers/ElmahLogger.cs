using System;
using System.Diagnostics;
using System.Web;
using Elmah;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Logging;

namespace e10.Shared.Providers
{
    public interface IUserProvider
    {
        string UserName { get; }
    }

    public class CurrentUserProvider : IUserProvider
    {
        public string UserName
        {
            get
            {
                var identity = System.Security.Principal.WindowsIdentity.GetCurrent();
                if (identity != null) return identity.Name;
                return string.Empty;
            }
        }
    }
    public class ElmahLogger : ILogger
    {
        public bool WriteCore(TraceEventType eventType, int eventId, object state, Exception exception, Func<object, Exception, string> formatter)
        {
            if (exception != null)
            {
                ErrorLog.GetDefault(HttpContext.Current).Log(new Error(exception));
            }
            return true;
        }
    }
}
