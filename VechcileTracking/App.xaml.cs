using System;
using System.Threading.Tasks;
using Plugin.DeviceInfo;
using SQLite;
using VechcileTracking.Models;
using VechcileTracking.Services;
using VechcileTracking.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VechcileTracking
{
    public partial class App : Application
    {
        SQLiteAsyncConnection connection;

        public App()
        {
            InitializeComponent();            

            MainPage = new InitialValidation();            
        }

        protected override async void OnStart()
        {
            // Handle when your app starts
            connection = DependencyService.Get<ISQLiteDb>().GetConnection();
            await connection.CreateTableAsync<AppInfo>();
            await connection.CreateTableAsync<Customer>();
            await connection.CreateTableAsync<Transaction>();
            await connection.CreateTableAsync<PaymentInfo>();
            await connection.CreateTableAsync<Vechicle>();
            await connection.CreateTableAsync<PaymentHistory>();

            //var appId = CrossDeviceInfo.Current.GenerateAppId(false, null, null);
        }
              
        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
