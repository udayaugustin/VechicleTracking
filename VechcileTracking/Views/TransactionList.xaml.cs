using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VechcileTracking.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TransactionList : ContentPage
	{
        SQLiteAsyncConnection connection;
        List<Customer> customerList;


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

            var selectedCustomer = (Customer)picker.SelectedItem;

            var trans = connection.Table<Transaction>().ToListAsync().Result;
            var transactions = new List<Transaction>();
            if (trans != null)
                transactions = trans.Where(p => p.CustomerId == selectedCustomer.Id).OrderByDescending(o => o.RentDate).ToList();
          
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

        private async void Delete_Clicked(object sender, EventArgs e)
        {
            var deletemenuItem = sender as Xamarin.Forms.MenuItem;

            if (deletemenuItem != null)
            {
                var transaction = deletemenuItem.BindingContext as Transaction;

                if (transaction != null)
                {
                    await connection.DeleteAsync(transaction);
                   
                }
            }
        }
    }
}