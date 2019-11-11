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

        private async void Edit_Clicked(object sender, EventArgs e)
        {
            var editmenuItem = sender as Xamarin.Forms.MenuItem;

            if (editmenuItem != null)
            {
                var vechicle = editmenuItem.BindingContext as Vechicle;

                if (vechicle != null)
                {
                   await Navigation.PushAsync(new EditVehicle(vechicle));
                }
            }
        }

        private async void Delete_Clicked(object sender, EventArgs e)
        {
            var deletemenuItem = sender as Xamarin.Forms.MenuItem;

            if (deletemenuItem != null)
            {
                var vechicle = deletemenuItem.BindingContext as Vechicle;

                if (vechicle != null)
                {
                    await connection.DeleteAsync(vechicle);
                }
            }
        }

        private void add_vehicle_Clicked(object sender, EventArgs e)
        {
            var mainPage = Application.Current.MainPage as MasterDetailPage;
            mainPage.Detail = new NavigationPage(new AddVehicle());
        }
    }
}
