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

            connection.CreateTableAsync<Vechicle>();
        }

        private void Add(object sender, EventArgs e)
        {
            var vehicleNo = VehcileNo.Text;
            var ownerName = OwnerName.Text;

            var vehicle = new Vechicle
            {
                VechileNo = vehicleNo,
                OwnerName = ownerName
            };

            connection.InsertAsync(vehicle);

            Navigation.PushAsync(new VehicleList());
        }
    }
}
