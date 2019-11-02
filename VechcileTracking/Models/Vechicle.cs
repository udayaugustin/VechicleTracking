using System;
using SQLite;

namespace VechcileTracking
{
    public class Vechicle
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string VechileNo { get; set; }

        public string OwnerName { get; set; }
    }
}

