using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using VechcileTracking.Models;
using Xamarin.Forms;

namespace VechcileTracking.Views
{
    public partial class TransactionEntry : ContentPage
    {
        SQLiteAsyncConnection connection;
        List<Vechicle> vechicles;
        List<Customer> customerList;
        Customer _selectedCustomer;
        Vechicle _selectedVehicle;
        public TransactionEntry()
        {
            InitializeComponent();

            connection = DependencyService.Get<ISQLiteDb>().GetConnection();
           
            GetData();
        }

        private async Task GetData()
        {
            customerList = new List<Customer>();
            customerList = await connection.Table<Customer>().ToListAsync();
            CustomerSelector.ItemsSource = customerList;
            CustomerSelector.ItemDisplayBinding = new Binding("Name");

            vechicles = new List<Vechicle>();
            vechicles = await connection.Table<Vechicle>().ToListAsync();            
            VechicleSelector.ItemsSource = vechicles;
            VechicleSelector.ItemDisplayBinding = new Binding("VechileNo");
        }

        private async void Save(object sender, EventArgs e)
        {

            if (_selectedVehicle == null || _selectedCustomer == null) return;

            var transaction = new Transaction
            {
                BattaAmount = Convert.ToInt32(BattaAmount.Text),
                BreakerHours = Convert.ToInt32(BreakerHours.Text),
                BreakerRate = Convert.ToInt32(BreakerRate.Text),
                BucketHours = Convert.ToInt32(BucketHours.Text),
                BucketRate = Convert.ToInt32(BucketRate.Text),
                CustomerId = _selectedCustomer.Id,
                CustomerName = _selectedCustomer.Name,
                VechilceId = _selectedVehicle.Id,
                VehicleNo = _selectedVehicle.VechileNo,
                Workplace = WorkPlace.Text,
                RentDate= rentDate.Date

            };

            if (_selectedCustomer == null) return;

            var paidInfo = connection.Table<PaymentInfo>().FirstOrDefaultAsync(o => o.CustomerId == _selectedCustomer.Id).Result;
            var amount = (transaction.BucketRate * transaction.BucketHours) + transaction.BattaAmount + (transaction.BreakerRate*transaction.BreakerHours);
            var payment = new PaymentInfo
            {
                CustomerId = _selectedCustomer.Id,
                Settled = paidInfo != null ? paidInfo.Settled : 0,
                TotalAmount = paidInfo != null ? paidInfo.TotalAmount + amount : amount
            };

            if (paidInfo == null)
                await connection.InsertAsync(payment);
            else
                await connection.UpdateAsync(payment);

            await connection.InsertAsync(transaction);

            var mainPage = Application.Current.MainPage as MasterDetailPage;
            mainPage.Detail = new NavigationPage(new DetailedReport(transaction.CustomerId));            
        }

        private void CustomerSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;

            _selectedCustomer = (Customer)picker.SelectedItem;
        }

        private void VechicleSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;

            _selectedVehicle = (Vechicle)picker.SelectedItem;

        }
    }
}
