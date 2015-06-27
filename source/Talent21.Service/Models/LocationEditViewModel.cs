namespace Talent21.Service.Models
{
    public class LocationEditViewModel : EditDictionaryViewModel
    {
        public string PinCode { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
    }

    public class LocationViewModel : LocationEditViewModel {}
}