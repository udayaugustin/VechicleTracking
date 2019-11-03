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
    public partial class MyMasterDetailPage : MasterDetailPage
    {
        MasterDetailPage mainPage;
        public MyMasterDetailPage()
        {
            InitializeComponent();

            SetupMenu();
        }

        public void SetupMenu()
        {
            var menuList = new List<MenuItem>
            {
                new MenuItem{ Title = "Add Transaction"},
                new MenuItem{ Title = "Update Payment"},
                new MenuItem{ Title = "Reports"},
                new MenuItem{ Title = "Add Customer"},
                new MenuItem{ Title = "Add Vehicle"},
                new MenuItem{ Title = "Vehicles List"},
                new MenuItem{ Title = "Customers List"}
            };

            listView.ItemsSource = menuList;

            
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            mainPage = Application.Current.MainPage as MasterDetailPage;
                        
            var menu = e.Item as MenuItem;
            switch (menu.Title)
            {
                case "Add Transaction":
                    mainPage.Detail = new NavigationPage(new TransactionEntry());                    
                    break;

                case "Update Payment":
                    mainPage.Detail = new NavigationPage(new UpdatePayment());                    
                    break;

                case "Reports":
                    mainPage.Detail = new NavigationPage(new Reports());                    
                    break;

                case "Add Customer":
                    mainPage.Detail = new NavigationPage(new AddCustomer());                    
                    break;

                case "Add Vehicle":
                    mainPage.Detail = new NavigationPage(new AddVehicle());                    
                    break;

                case "Vehicles List":
                    mainPage.Detail = new NavigationPage(new VehicleList());                    
                    break;

                case "Customers List":
                    mainPage.Detail = new NavigationPage(new CustomerList());                    
                    break;
            }
            mainPage.IsPresented = false;
        }
    }
}
