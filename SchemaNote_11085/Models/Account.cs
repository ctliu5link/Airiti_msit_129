using System;
using System.Collections.Generic;

#nullable disable

namespace SchemaNote_A11085.Models
{
    public partial class Account
    {
        public string AccountId { get; set; }
        public string AccountPassword { get; set; }
        public string ContactEmail { get; set; }
        public string CustomerId { get; set; }
        public string CustomerFrom { get; set; }
        public string RegisterDate { get; set; }
        public string AccountType { get; set; }
        public string AccountStatus { get; set; }
        public string AccountSdate { get; set; }
        public string AccountEdate { get; set; }
        public string RegCustomerId { get; set; }
        public string VerifyId { get; set; }
        public string AppsyncStatus { get; set; }
        public int? GracePeriod { get; set; }
        public string Sort { get; set; }
    }
}
