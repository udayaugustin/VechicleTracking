using SQLite;
using System;

namespace VechcileTracking.Models
{
    public class PaymentInfo
    {
        [PrimaryKey, Unique]
        public int CustomerId { get; set; }

        public int TotalAmount { get; set; }

        public int Settled { get; set; }
    }
}
