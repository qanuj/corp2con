namespace e10.Shared
{
    public class KeySecret
    {
        public string Key { get; set; }
        public string Secret { get; set; }

        public KeySecret(string name)
        {
            this.Key = System.Configuration.ConfigurationManager.AppSettings[string.Format("{0}Id", name)];
            this.Secret = System.Configuration.ConfigurationManager.AppSettings[string.Format("{0}Secret", name)];
        }
    }
}