using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VechcileTracking.Models;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VechcileTracking.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailedReport : TabbedPage
    {
        SQLiteAsyncConnection connection;
        List<PaymentHistory> paymentHistories;
        List<Transaction> transactions;
        int customerId;
        public DetailedReport(int customerId)
        {
            InitializeComponent();

            connection = DependencyService.Get<ISQLiteDb>().GetConnection();
            this.customerId = customerId;
            GetData(customerId);            
        }

        private async void Call(object sender, EventArgs e)
        {
            var customer = await connection.Table<Customer>().Where(c => c.Id == customerId).FirstOrDefaultAsync();
            PhoneDialer.Open(customer.PhoneNo);
        }

        private void GetData(int customerId)
        {
            try
            {
                var paidInfo = connection.Table<PaymentInfo>().FirstOrDefaultAsync(o => o.CustomerId == customerId).Result;

                if (paidInfo != null)
                {
                    balanceLabel.Text = "Balance to be paid is Rs " + (paidInfo.TotalAmount - paidInfo.Settled);
                }

                var history = connection.Table<PaymentHistory>().ToListAsync().Result;

                if (history != null)
                    paymentHistories = history.Where(p => p.CustomerId == customerId).OrderByDescending(o => o.PaidDate).ToList();

                paymentListView.ItemsSource = paymentHistories;

                var trans = connection.Table<Transaction>().ToListAsync().Result;

                if (trans != null)
                    transactions = trans.Where(p => p.CustomerId == customerId).OrderByDescending(o => o.RentDate).ToList();
                rentListView.ItemsSource = transactions;
            }
            catch(Exception ex)
            {

            }
        }
    }
}