namespace Talent21.Service.Models
{
    /// <summary>
    /// Candidate Create and Job Application Information
    /// </summary>
    public class CreateCandidateAndApplyToJobViewModel
    {
        /// <summary>
        /// Name of the Candidate
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// JobId to Apply to
        /// </summary>
        public int JobId { get; set; }
    }
}
