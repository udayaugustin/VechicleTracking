using System;
using SQLite;

namespace VechcileTracking
{
    public class AppInfo
    {
        [PrimaryKey]
        public string Username { get; set; }

        public string AppID { get; set; }

        public DateTime LastSynchDate { get; set; }

        public bool IsAppIdValidated { get; set; }
    }
}
