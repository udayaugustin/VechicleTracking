using System;
namespace VechcileTracking
{
    public class UserUpdate
    {
        public string Name { get; set; }

        public string Mobile { get; set; }

        public string AppId { get; set; }

        public string UserStatus { get; set; }

        public DateTimeOffset ActivateDate { get; set; }

        public DateTimeOffset ExpireDate { get; set; }
    }
}
