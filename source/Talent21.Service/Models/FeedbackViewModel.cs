using System.ComponentModel.DataAnnotations;

namespace Talent21.Service.Models
{
    public class FeedbackViewModel
    {
        public string Name { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }
}