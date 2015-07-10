namespace Talent21.Service.Models
{
    public class DictionaryViewModel  
    {
        public string Code { get; set; }
        public string Title { get; set; }
    }
    public class DictionaryEditViewModel : DictionaryViewModel
    {
        public int Id { get; set; }
    }
}