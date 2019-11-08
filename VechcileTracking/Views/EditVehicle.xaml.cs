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
	public partial class EditVehicle : ContentPage
	{
        SQLiteAsyncConnection connection;
        List<Vechicle> customerList;
        Vechicle _selectedVechicle;

        public EditVehicle ( Vechicle vechicle)
		{
			InitializeComponent ();
            connection = DependencyService.Get<ISQLiteDb>().GetConnection();
            _selectedVechicle = vechicle;
            GetData(vechicle);
		}

        private async void GetData(Vechicle vechicle)
        {
            VehcileNo.Text = vechicle.VechileNo.ToString();
            OwnerName.Text = vechicle.OwnerName.ToString();
        }

        private async void Update(object sender, EventArgs e)
        {
            _selectedVechicle.VechileNo = VehcileNo.Text;
            _selectedVechicle.OwnerName = OwnerName.Text;
            await connection.UpdateAsync(_selectedVechicle);
            var mainPage = Application.Current.MainPage as MasterDetailPage;
            mainPage.Detail = new NavigationPage(new VehicleList());
        }
    }
}