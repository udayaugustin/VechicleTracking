using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using VechcileTracking.Droid.Persistance;
using VechcileTracking.Interface;
using Xamarin.Forms;

[assembly: Dependency(typeof(BackupDB))]

namespace VechcileTracking.Droid.Persistance
{
    public class BackupDB : IBackupDB
    {
        public string SyncDB()
        {
            string PureFileName = string.Format("vehicle_tracking_{0}_{1}_{2}_{3}.db3",DateTime.Now.Date.Day,DateTime.Now.Month,DateTime.Now.Year,DateTime.Now.Ticks);
            String uploadUrl = String.Format("{0}/{1}", "ftp://oywm4egm0p6k@43.255.154.9/var/www/codevmedia/vehicle-tracking", PureFileName);
            FtpWebRequest req = (FtpWebRequest)FtpWebRequest.Create(uploadUrl);
            req.Proxy = null;
            req.Method = WebRequestMethods.Ftp.UploadFile;
            req.Credentials = new NetworkCredential("oywm4egm0p6k", "Surandai@18");
            req.UseBinary = true;
            req.UsePassive = true;
            var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            var path = Path.Combine(documentsPath, "MySQLite.db3");
            byte[] data = File.ReadAllBytes(path);
            req.ContentLength = data.Length;
            Stream stream = req.GetRequestStream();
            stream.Write(data, 0, data.Length);
            stream.Close();
            FtpWebResponse res = (FtpWebResponse)req.GetResponse();
            return res.StatusDescription;

        }
    }
}