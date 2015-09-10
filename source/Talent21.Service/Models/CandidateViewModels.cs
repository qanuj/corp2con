using System;
using System.Collections.Generic;
using Talent21.Data.Core;

namespace Talent21.Service.Models
{
    public class DeleteProfileViewModel : IdModel
    {
        
    }

    public class MemberViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string AlternateNumber { get; set; }
        public string Location { get; set; }
        public string Address { get; set; }
        public string PinCode { get; set; }
    }

    public class GiftViewModel
    {
        public int Credit { get; set; }
        public string Email { get; set; }
    }
    

    public class InvoiceViewModel
    {
        public MemberViewModel Member { get; set; }
        public IEnumerable<Transaction> Transactions { get; set; }
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public float Total { get; set; }
        public int UnitPrice { get; set; }
        public double Tax { get; set; }
        public string TaxName { get; set; }
        public double TaxAmount { get; set; }
    }
}
