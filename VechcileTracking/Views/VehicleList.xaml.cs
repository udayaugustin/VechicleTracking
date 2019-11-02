using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Forms;

namespace VechcileTracking.Views
{
    public partial class VehicleList : ContentPage
    {
        SQLiteAsyncConnection connection;

        List<Vechicle> vechicles;

        public VehicleList()
        {
            InitializeComponent();

            connection = DependencyService.Get<ISQLiteDb>().GetConnection();
            GetData();
        }

        private async Task GetData()
        {
            vechicles = new List<Vechicle>();
            vechicles = await connection.Table<Vechicle>().ToListAsync();
            listView.ItemsSource = vechicles;
        }
    }
}
