using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e10.Shared.Providers
{
    public interface IEmailConfigProvider
    {
        string From { get; }
        string Name { get; }
        string Server { get; }
        int Port { get; }
        bool IsGmail { get; }
        string Password { get;}
    }

    public class DoNotReplyAte10EmailConfigProvider : IEmailConfigProvider
    {
        public string From { get { return "donotreply@e10.in"; } }
        public string Name { get { return "Do Not Reply"; } }
        public string Server { get { return "smtp.gmail.com"; } }
        public int Port { get { return 587; } }
        public bool IsGmail { get { return true; } }
        public string Password { get { return "bj%YkV5I5}HlW?Yj"; } }
    }
}
