using System;
using SQLite;

namespace VechcileTracking
{
    public class Transaction 
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public string  CustomerName { get; set; }

        public int VechilceId { get; set; }

        public string  VehicleNo { get; set; }

        public string Workplace { get; set; }

        public int BucketHours { get; set; }

        public int BucketRate{ get; set; }

        public int BreakerHours { get; set; }

        public int BreakerRate { get; set; }

        public int BattaAmount { get; set; }

        public DateTime RentDate { get; set; }

        public string Date
        {
            get
            {
                return RentDate.ToShortDateString();
            }
        }

        public int Total
        {
            get
            {                
                return (BucketRate * BucketHours) + (BreakerHours * BreakerRate) + BattaAmount; ;
            }
        }

    }
}

