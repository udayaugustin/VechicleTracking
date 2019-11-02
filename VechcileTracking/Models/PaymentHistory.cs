using SQLite;
using System;

namespace VechcileTracking.Models
{
    public class PaymentHistory
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public int PaidAmount { get; set; }

        public DateTime PaidDate { get; set; }

        public string Date
        {
            get
            {
                return PaidDate.ToShortDateString();
            }
        }
    }
}
