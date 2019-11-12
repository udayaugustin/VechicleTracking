using SQLite;
using System;
using VechcileTracking.Interface;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VechcileTracking.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SyncData : ContentPage
    {
        IBackupDB backupDB;
        SQLiteAsyncConnection connection;

        public SyncData()
        {
            InitializeComponent();
            connection = DependencyService.Get<ISQLiteDb>().GetConnection();
            backupDB = DependencyService.Get<IBackupDB>();
        }

        private async void Sync_Clicked(object sender, EventArgs e)
        {
            if (!ConnectivityHelper.IsInternetAvailable)
            {
                await DisplayAlert("Alert!", "Please enable internet to Sync your Data", "OK");
                return;
            }
            sync.IsEnabled = false;
            indicator.IsRunning = true;
            
            var appInfo = await connection.Table<AppInfo>().FirstOrDefaultAsync();
            var result = await backupDB.SyncDBAsync(appInfo.Username);
            
            if (result != true)
            {
                await DisplayAlert("Alert!", "Sync not completed", "Ok");
            }
            else
            {
                await DisplayAlert("Success!", "Sync the data successfully", "Ok");

            }

            sync.IsEnabled = true;
            indicator.IsRunning = false;
        }
    
    }
}