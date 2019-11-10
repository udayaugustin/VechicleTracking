using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VechcileTracking.Interface;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VechcileTracking.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SyncData : ContentPage
    {
        IBackupDB backupDB;

        public SyncData()
        {
            InitializeComponent();

            backupDB = DependencyService.Get<IBackupDB>();
        }

        private async void Sync_Clicked(object sender, EventArgs e)
        {
            var result = await backupDB.SyncDBAsync();
        }
    }
}