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

        void NaviagteToAddCustomer(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddCustomer());
        }

        void NaviagteToAddVechilce(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddVehicle());
        }

        void NaviagteToTransaction(object sender, EventArgs e)
        {
            Navigation.PushAsync(new TransactionEntry());
        }

        void NaviagteToCustomerList(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CustomerList());
        }

        void NaviagteToVechicleList(object sender, EventArgs e)
        {
            Navigation.PushAsync(new VehicleList());
        }
    }
}
