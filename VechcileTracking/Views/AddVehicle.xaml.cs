using System;
using System.Collections.Generic;
using SQLite;
using Xamarin.Forms;

namespace VechcileTracking.Views
{
    public partial class AddVehicle : ContentPage
    {
        SQLiteAsyncConnection connection;

        public AddVehicle()
        {
            InitializeComponent();

            connection = DependencyService.Get<ISQLiteDb>().GetConnection();

        }

        private async void Add(object sender, EventArgs e)
        {
            var vehicleNo = VehcileNo.Text;
            var ownerName = OwnerName.Text;

            var vehicle = new Vechicle
            {
                VechileNo = vehicleNo,
                OwnerName = ownerName
            };

            await connection.InsertAsync(vehicle);

            var mainPage = Application.Current.MainPage as MasterDetailPage;
            mainPage.Detail = new NavigationPage(new VehicleList());            
        }
    }
}
