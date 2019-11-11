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

        private void RefreshCustomerList()
        {
            var custom = connection.Table<Customer>().ToListAsync().Result;
            var customers = new List<Customer>();
            if (custom != null)
            listView.ItemsSource = customers;
        }


        private void Edit_clicked(object sender, EventArgs e)
        {
            var editmenuItem = sender as Xamarin.Forms.MenuItem;

            if (editmenuItem != null)
            {
                var customer = editmenuItem.BindingContext as Customer;

                if (customer != null)
                {
                    Navigation.PushAsync(new EditCustomer(customer));
                }
            }            
        }

        private async void Delete_clicked(object sender, EventArgs e)
        {
            var deletemenuItem = sender as Xamarin.Forms.MenuItem;

            if (deletemenuItem != null)
            {
                var customer = deletemenuItem.BindingContext as Customer;

                if (customer != null)
                {
                    await connection.DeleteAsync(customer);
                   
                }
                RefreshCustomerList();
            }

        }

        private void add_customer_Clicked(object sender, EventArgs e)
        {
            var mainPage = Application.Current.MainPage as MasterDetailPage;
            mainPage.Detail = new NavigationPage(new AddCustomer());
        }
    }
}
