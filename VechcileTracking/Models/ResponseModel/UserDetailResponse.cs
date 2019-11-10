using System;
namespace VechcileTracking
{
    public class UserDetailResponse
    {
        public string Status { get; set; }

        public UserDetails UserDetails { get; set; }

        public UserFileDetail[] UserFileDetails { get; set; }        
    }

    public class UserDetails
    {
        public long UserId { get; set; }

        public string Name { get; set; }

        public string Mobile { get; set; }

        public DateTimeOffset CreatedOn { get; set; }

        public DateTimeOffset ActivatedDate { get; set; }

        public DateTimeOffset ExpireDate { get; set; }

        public string AppId { get; set; }

        public string AuthToken { get; set; }

        public string UserStatus { get; set; }
    }

    public class UserFileDetail
    {
        public long FileId { get; set; }

        public DateTimeOffset CreatedOn { get; set; }

        public string FileUrl { get; set; }
    }
}


