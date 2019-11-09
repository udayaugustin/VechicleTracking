using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VechcileTracking.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VechcileTracking.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditTransaction : ContentPage
	{
        SQLiteAsyncConnection connection;
        List<Vechicle> vechicles;
        List<Customer> customerList;
        Customer _selectedCustomer;
        Vechicle _selectedVehicle;
        Transaction _currenttransaction;
        public EditTransaction (Transaction transaction)
		{
			InitializeComponent ();
            connection = DependencyService.Get<ISQLiteDb>().GetConnection();
            Title = string.Format("Edit: {0} - {1}", transaction.CustomerName, transaction.VehicleNo);
            _currenttransaction = transaction;
            GetData(transaction);
        }

        private async Task GetData(Transaction transaction)
        {
            customerList = new List<Customer>();
            customerList = await connection.Table<Customer>().ToListAsync();
            CustomerSelector.ItemsSource = customerList;
            CustomerSelector.ItemDisplayBinding = new Binding("Name");
            var customer = customerList.Find(c => c.Id == transaction.CustomerId);
            CustomerSelector.SelectedIndex = customerList.IndexOf(customer);

            vechicles = new List<Vechicle>();
            vechicles = await connection.Table<Vechicle>().ToListAsync();
            VechicleSelector.ItemsSource = vechicles;
            VechicleSelector.ItemDisplayBinding = new Binding("VechileNo");
            var vehicle = vechicles.Find(c => c.VechileNo == transaction.VehicleNo);
            VechicleSelector.SelectedIndex = vechicles.IndexOf(vehicle);

            BattaAmount.Text = transaction.BattaAmount.ToString();
            BreakerHours.Text = transaction.BreakerHours.ToString();
            BreakerRate.Text = transaction.BreakerRate.ToString();
            BucketHours.Text = transaction.BucketHours.ToString();
            BucketRate.Text = transaction.BucketRate.ToString();
            WorkPlace.Text = transaction.Workplace.ToString();
            rentDate.Date = transaction.RentDate;
        }

        private async void Save(object sender, EventArgs e)
        {

            if (_selectedVehicle == null || _selectedCustomer == null) return;


            _currenttransaction.BattaAmount = Convert.ToInt32(BattaAmount.Text);
            _currenttransaction.BreakerHours = Convert.ToInt32(BreakerHours.Text);
            _currenttransaction.BreakerRate = Convert.ToInt32(BreakerRate.Text);
            _currenttransaction.BucketHours = Convert.ToInt32(BucketHours.Text);
            _currenttransaction.BucketRate = Convert.ToInt32(BucketRate.Text);
            _currenttransaction.CustomerId = _selectedCustomer.Id;
            _currenttransaction.CustomerName = _selectedCustomer.Name;
            _currenttransaction.VechilceId = _selectedVehicle.Id;
            _currenttransaction.VehicleNo = _selectedVehicle.VechileNo;
            _currenttransaction.Workplace = WorkPlace.Text;
            _currenttransaction.RentDate = rentDate.Date;                            

            if (_selectedCustomer == null) return;

            var paidInfo = connection.Table<PaymentInfo>().FirstOrDefaultAsync(o => o.CustomerId == _selectedCustomer.Id).Result;
            var amount = (_currenttransaction.BucketRate * _currenttransaction.BucketHours) + _currenttransaction.BattaAmount + (_currenttransaction.BreakerRate * _currenttransaction.BreakerHours);
            var payment = new PaymentInfo
            {
                CustomerId = _selectedCustomer.Id,
                Settled = paidInfo != null ? paidInfo.Settled : 0,
                TotalAmount = paidInfo != null ? paidInfo.TotalAmount + amount : amount
            };


            if (paidInfo == null)
                await connection.InsertAsync(payment);
            else
            {
                if (paidInfo.TotalAmount > amount)
                    payment.TotalAmount = payment.TotalAmount - (payment.TotalAmount - amount);
                else if(paidInfo.TotalAmount<amount)
                    payment.TotalAmount = payment.TotalAmount + (amount - payment.TotalAmount);
                await connection.UpdateAsync(payment);
            }

            await connection.UpdateAsync(_currenttransaction);

            var mainPage = Application.Current.MainPage as MasterDetailPage;
            mainPage.Detail = new NavigationPage(new TransactionList());
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