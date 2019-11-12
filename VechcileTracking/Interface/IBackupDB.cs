using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace VechcileTracking.Interface
{
    public interface IBackupDB
    {
        Task<bool> SyncDBAsync(string username);
    }
}
