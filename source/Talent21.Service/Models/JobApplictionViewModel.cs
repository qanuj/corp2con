using System.ComponentModel.DataAnnotations;
namespace Talent21.Service.Models

{
    public class JobApplictionViewModel
    {
        [Required]
        public int Id { get; set; }

       public Data.Core.JobActionEnum Act { get; set; }
    }
}
