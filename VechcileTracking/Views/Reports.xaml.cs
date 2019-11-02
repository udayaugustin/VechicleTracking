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
	public partial class Reports : ContentPage
	{
        SQLiteAsyncConnection connection;
        List<Vechicle> vechicles;
        List<Customer> customerList;

        Customer _selectedCustomer;

        public Reports()
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

        private void GetReport_Clicked(object sender, EventArgs e)
        {

            if (_selectedCustomer == null) return;

            Navigation.PushAsync(new DetailedReport(_selectedCustomer.Id));
           
        }

        private void CustomerSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;

            _selectedCustomer = (Customer)picker.SelectedItem;

        }
    }
}