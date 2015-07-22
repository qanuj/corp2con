namespace Talent21.Service.Models
{
    public class PersonEditViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PictureUrl { get; set; }
    }
    public class PersonViewModel : PersonEditViewModel
    {
        public int Id { get; set; }
        public string Location { get; set; }
        public string Mobile { get; set; }
        public string Twitter { get; set; }
        public string WebSite { get; set; }
        public string Rss { get; set; }
        public string LinkedIn { get; set; }
        public string Google { get; set; }
        public string Yahoo { get; set; }
        public string Facebook { get; set; }
        public string About { get; set; }
        public string AlternateNumber { get; set; }
        public string Profile { get; set; }
    }
}