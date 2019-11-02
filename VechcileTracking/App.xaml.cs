using System;
using VechcileTracking.Models;
using VechcileTracking.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VechcileTracking
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Reports());
        }

        protected override void OnStart()
        {
            // Handle when your app starts

            var connection = DependencyService.Get<ISQLiteDb>().GetConnection();
            connection.CreateTableAsync<Customer>();
            connection.CreateTableAsync<Transaction>();
            connection.CreateTableAsync<PaymentInfo>();
            connection.CreateTableAsync<Vechicle>();
            connection.CreateTableAsync<PaymentHistory>();
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
