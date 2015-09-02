using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace e10.Shared.Data.Abstraction
{
    public class AdvertisementTransaction : Transaction
    {
        public IList<Advertisement> Advertisements { get; set; }
    }

    public class Payment : Transaction
    {
        public string Gateway { get; set; }
        public string Capture { get; set; }
    }

    public class Transaction : Entity
    {
        public string Reason { get; set; }
        public bool IsSuccess { get; set; }
        public int Credit { get; set; }
        public string Code { get; set; }
        public string Notes { get; set; }

        [DataType(DataType.MultilineText)]
        public string PaymentCapture { get; set; }

        public User User { get; set; }
        public string UserId { get; set; }

        [DataType(DataType.Currency)]
        public float Amount { get; set; }


        public static string GenerateHash512(string text)
        {
            byte[] message = Encoding.UTF8.GetBytes(text);
            var hashString = new SHA512Managed();
            byte[] hashValue = hashString.ComputeHash(message);
            return hashValue.Aggregate("", (current, x) => current + String.Format((string) "{0:x2}", (object) x));
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