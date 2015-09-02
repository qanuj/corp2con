using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace e10.Shared.Data.Abstraction
{
    [ComplexType]
    public class Duration
    {
        public int Years { get; set; }
        public int Months { get; set; }
    }

    public class Invite : Entity
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime? Completed { get; set; }
    }
}