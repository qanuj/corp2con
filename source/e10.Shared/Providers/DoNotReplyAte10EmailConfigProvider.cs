namespace e10.Shared.Providers
{
    public class DoNotReplyAte10EmailConfigProvider : IEmailConfigProvider
    {
        public string From { get { return "donotreply@e10.in"; } }
        public string Name { get { return "Do Not Reply"; } }
        public string Server { get { return "smtp.gmail.com"; } }
        public int Port { get { return 587; } }
        public bool IsGmail { get { return true; } }
        public string Password { get { return "bj%YkV5I5}HlW?Yj"; } }
        public string SendGridApiKey { get { return "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"; }}
    }
}