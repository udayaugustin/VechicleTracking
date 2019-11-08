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
	public partial class EditCustomer : ContentPage
	{
        SQLiteAsyncConnection connection;
        List<Customer> customerList;
        Customer _selectedCustomer;

        public EditCustomer (Customer customer)
		{
            InitializeComponent();
            connection = DependencyService.Get<ISQLiteDb>().GetConnection();
            _selectedCustomer = customer;
            GetData(customer);
        }

        private async Task GetData (Customer customer)
        {

            Name.Text = customer.Name.ToString();
            PhoneNo.Text = customer.PhoneNo.ToString();
        }
        private async void Save(object sender, EventArgs e)
        {
            _selectedCustomer.Name = Name.Text;
            _selectedCustomer.PhoneNo = PhoneNo.Text;
            await connection.UpdateAsync(_selectedCustomer);
            var mainPage = Application.Current.MainPage as MasterDetailPage;
            mainPage.Detail = new NavigationPage(new CustomerList());
        }
    }
}