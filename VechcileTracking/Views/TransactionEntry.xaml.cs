using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Forms;

namespace VechcileTracking.Views
{
    public partial class TransactionEntry : ContentPage
    {
        SQLiteAsyncConnection connection;
        List<Vechicle> vechicles;
        List<Customer> customerList;

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

        private void Save(object sender, EventArgs e)
        {
           /* var selectedCustomer = ;
            var ownerName = OwnerName.Text;

            var vehicle = new Vechicle
            {
                VechileNo = vehicleNo,
                OwnerName = ownerName
            };

            connection.InsertAsync(vehicle);

            Navigation.PushAsync(new VehicleList());*/
        }
    }
}
