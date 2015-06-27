namespace Talent21.Service.Models
{
    public class LocationDictionaryEditViewModel : DictionaryEditViewModel
    {
        public string PinCode { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
    }

    public class LocationDictionaryViewModel : LocationDictionaryEditViewModel {}
}