using System;
using System.Collections.Generic;
using SQLite;
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

            connection.CreateTableAsync<Customer>();
        }

        private void Add(object sender, EventArgs e)
        {
            var name = Name.Text;
            var phoneNo = PhoneNo.Text;

            var customer = new Customer
            {
                Name = name,
                PhoneNo = phoneNo
            };

            connection.InsertAsync(customer);

            Navigation.PushAsync(new CustomerList());
        }
    }
}
