using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;

namespace Talent21.Service.Models
{
    public class ScheduleViewModel : EditScheduleViewModel
    {
    }

    public class EditScheduleViewModel : CreateScheduleViewModel
    {
        public int Id { get; set; }
    }

    public class DeleteScheduleViewModel : IdModel
    {
      
    }

    public class CreateScheduleViewModel
    {
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
    }
}
