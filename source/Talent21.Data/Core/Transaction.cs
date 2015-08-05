using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using e10.Shared.Data.Abstraction;

namespace Talent21.Data.Core
{
    public class Transaction : Entity
    {
        public string Reason { get; set; }
        public bool IsSuccess { get; set; }
        public int Credit { get; set; }
        public string Code { get; set; }

        [DataType(DataType.MultilineText)]
        public string PaymentCapture { get; set; }

        public User User { get; set; }
        public string UserId { get; set; }

        [DataType(DataType.Currency)]
        public float Amount { get; set; }


        public static string GenerateHash512(string text)
        {
            var message = Encoding.UTF8.GetBytes(text);
            var hashString = new SHA512Managed();
            var hashValue = hashString.ComputeHash(message);
            return hashValue.Aggregate("", (current, x) => current + string.Format("{0:x2}",x));
        }

        public static string GenerateTransactionId()
        {
            var rnd = new Random();
            var strHash = GenerateHash512(rnd.ToString() + DateTime.Now);
            var txnid1 = strHash.Substring(0, 20);
            return txnid1;
        }

    }
}