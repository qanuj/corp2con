using System;
using System.ComponentModel.DataAnnotations;

namespace Talent21.Service.Models
{
    public class FeedbackCreateViewModel
    {
        public string Name { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }

    public class FeedbackViewModel : FeedbackCreateViewModel
    {
        public int Id { get; set; }
        public bool IsRead { get; set; }
        public DateTime Created { get; set; }
    }
}