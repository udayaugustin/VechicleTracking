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
            connection.CreateTableAsync<Customer>();
            connection.CreateTableAsync<Transaction>();
            connection.CreateTableAsync<PaymentInfo>();
            connection.CreateTableAsync<Vechicle>();
            connection.CreateTableAsync<PaymentHistory>();
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
