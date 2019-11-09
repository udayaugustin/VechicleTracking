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
	public partial class TransactionList : ContentPage
	{
        SQLiteAsyncConnection connection;
        List<Customer> customerList;
        Customer _selectedCustomer;

        public TransactionList()
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

        private void CustomerSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;

            _selectedCustomer = (Customer)picker.SelectedItem;

            RefreshTransactionList();
        }

        private void RefreshTransactionList()
        {
            var trans = connection.Table<Transaction>().ToListAsync().Result;
            var transactions = new List<Transaction>();
            if (trans != null)
                transactions = trans.Where(p => p.CustomerId == _selectedCustomer.Id).OrderByDescending(o => o.RentDate).ToList();

            headerGrid.IsVisible = !(transactions.Count == 0);

            rentListView.ItemsSource = transactions;
        }

        private void Edit_Clicked(object sender, EventArgs e)
        {
            var editmenuItem = sender as Xamarin.Forms.MenuItem;

            if(editmenuItem!=null)
            {
                var transaction = editmenuItem.BindingContext as Transaction;

                if(transaction!=null)
                {
                    Navigation.PushAsync(new EditTransaction(transaction));
                }
            }
        }

        private void Delete_Clicked(object sender, EventArgs e)
        {
<<<<<<< HEAD
            var editmenuItem = sender as Xamarin.Forms.MenuItem;
=======
            var deletemenuItem = sender as Xamarin.Forms.MenuItem;            
>>>>>>> e5f6913... Naviagtion style is updated

            if (editmenuItem != null)
            {
                var transaction = editmenuItem.BindingContext as Transaction;

                if (transaction != null)
                {
<<<<<<< HEAD
                    connection.DeleteAsync(transaction);

                    var paidInfo = connection.Table<PaymentInfo>().FirstOrDefaultAsync(o => o.CustomerId == _selectedCustomer.Id).Result;

                    var amount = (transaction.BucketRate * transaction.BucketHours) + transaction.BattaAmount + (transaction.BreakerRate * transaction.BreakerHours);

                    if (paidInfo != null)
                    {
                        paidInfo.TotalAmount = paidInfo.TotalAmount - amount;

                        connection.UpdateAsync(paidInfo);
                    }

                    RefreshTransactionList();
                }
=======
                    await connection.DeleteAsync(transaction);

                    var mainPage = Application.Current.MainPage as MasterDetailPage;
                    mainPage.Detail = new NavigationPage(new TransactionList());
                }                
>>>>>>> e5f6913... Naviagtion style is updated
            }
        }
    }
}