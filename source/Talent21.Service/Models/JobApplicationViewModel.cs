using System.ComponentModel.DataAnnotations;
namespace Talent21.Service.Models

{
    /// <summary>
    /// 
    /// </summary>
    public class JobApplicationViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Data.Core.JobActionEnum Act { get; set; }
    }
}
