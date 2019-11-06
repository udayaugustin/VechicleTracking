using System;
using System.Collections.Generic;
using SQLite;
using VechcileTracking.Models;
using Xamarin.Forms;

namespace VechcileTracking.Views
{
    public partial class AddCustomer : ContentPage
    {
        SQLiteAsyncConnection connection;

        public AddCustomer()
        {
            InitializeComponent();

            connection = DependencyService.Get<ISQLiteDb>().GetConnection();            
        }

        private async void Add(object sender, EventArgs e)
        {
            var name = Name.Text;
            var phoneNo = PhoneNo.Text;

            var customer = new Customer
            {
                Name = name,
                PhoneNo = phoneNo
            };

            await connection.InsertAsync(customer);

            var mainPage = Application.Current.MainPage as MasterDetailPage;
            mainPage.Detail = new NavigationPage(new CustomerList());            
        }        
    }
}
