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

        private void NavigateToPaymentInfo(object sender, EventArgs e)
        {
            Navigation.PushAsync(new UpdatePayment());
        }

        private void NavigateToReports(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Reports());
        }
    }
}
