using System.ComponentModel.DataAnnotations;
namespace Talent21.Service.Models
{
    public class ScheduleViewModel
    {
        [Required]
        public int CandidateId { get; set; }

    }
}