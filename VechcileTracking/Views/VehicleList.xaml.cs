using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Forms;

namespace VechcileTracking.Views
{
    public partial class VehicleList : ContentPage
    {
        SQLiteAsyncConnection connection;

        List<Vechicle> vechicles;

        public VehicleList()
        {
            InitializeComponent();

            connection = DependencyService.Get<ISQLiteDb>().GetConnection();
            GetData();
        }

        private async Task GetData()
        {
            vechicles = new List<Vechicle>();
            vechicles = await connection.Table<Vechicle>().ToListAsync();
            listView.ItemsSource = vechicles;
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
