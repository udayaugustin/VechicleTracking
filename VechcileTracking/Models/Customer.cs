using System;
using SQLite;

namespace VechcileTracking
{
    public class Customer
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }

        public string PhoneNo { get; set; }
    }
}
