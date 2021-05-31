using System;
using System.Collections.Generic;

#nullable disable

namespace SchemaNote_A11085.Models
{
    public partial class AccountSchInfoRefuse
    {
        public DateTime RefuseTime { get; set; }
        public string RefuseDesc { get; set; }
        public string Id { get; set; }
        public string SchId { get; set; }
        public string ColId { get; set; }
        public string DepId { get; set; }
        public string StudentShip { get; set; }
    }
}
