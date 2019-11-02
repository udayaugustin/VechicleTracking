using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Forms;

namespace VechcileTracking.Views
{
    public partial class CustomerList : ContentPage
    {
        SQLiteAsyncConnection connection;

        List<Customer> customerList;
        public CustomerList()
        {
            InitializeComponent();

            connection = DependencyService.Get<ISQLiteDb>().GetConnection();
            GetData();
        }

        private async Task GetData()
        {
            customerList = new List<Customer>();
            customerList = await connection.Table<Customer>().ToListAsync();
            listView.ItemsSource = customerList;
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
