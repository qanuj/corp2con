namespace Talent21.Data.Core
{
    public class Payment : Transaction
    {
        public string Gateway { get; set; }
        public string Capture { get; set; }
    }
}