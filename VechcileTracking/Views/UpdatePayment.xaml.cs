
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
    public partial class UpdatePayment : ContentPage
    {
        SQLiteAsyncConnection connection;
        List<Vechicle> vechicles;
        List<Customer> customerList;

        Customer _selectedCustomer;

        public UpdatePayment()
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
        }

        private async void UpdatePayment_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (_selectedCustomer == null) return;

                var paidInfo = connection.Table<PaymentInfo>().FirstOrDefaultAsync(o => o.CustomerId == _selectedCustomer.Id).Result;

                var payment = new PaymentInfo
                {
                    CustomerId = _selectedCustomer.Id,
                    Settled = (paidInfo != null ? paidInfo.Settled : 0) + Convert.ToInt32(amountEntry.Text),
                    TotalAmount = paidInfo.TotalAmount
                };

                var paymentHistory = new PaymentHistory
                {
                    CustomerId = _selectedCustomer.Id,
                    PaidAmount = Convert.ToInt32(amountEntry.Text),
                    PaidDate = DateTime.Now
                };


                await connection.UpdateAsync(payment);

                await connection.InsertAsync(paymentHistory);

                await Navigation.PushAsync(new DetailedReport(_selectedCustomer.Id));

                //CustomerSelector.SelectedIndex = -1;

                //amountEntry.Text = string.Empty;

                //balanceLabel.Text = "0";
            }
            catch(Exception ex)
            {
            }
        }

        private void CustomerSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;

            _selectedCustomer = (Customer)picker.SelectedItem;

            var paidInfo = connection.Table<PaymentInfo>().FirstOrDefaultAsync(o => o.CustomerId == _selectedCustomer.Id).Result;

            if(paidInfo!=null)
            {
                balanceLabel.Text = string.Format("{0}", paidInfo.TotalAmount - paidInfo.Settled);
            }
            else
            {
                balanceLabel.Text = "0";
            }
        }
    }
}