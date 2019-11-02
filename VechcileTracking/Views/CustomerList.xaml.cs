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
    }
}
