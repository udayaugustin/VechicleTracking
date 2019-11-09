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
                new MenuItem{ Title = "Add Site Work"},
                new MenuItem{ Title = "Received Cash"},
                new MenuItem{ Title = "Balance Cash List"},
                new MenuItem{ Title = "Reports"},
                new MenuItem{ Title = "Add Customer"},
                new MenuItem{ Title = "Add Vehicle"},
                new MenuItem{ Title = "Vehicles List"},
                new MenuItem{ Title = "Customers List"},
<<<<<<< HEAD
                new MenuItem{ Title = "Transaction List"},
                new MenuItem{ Title = "Sync Data"}
=======
                new MenuItem{ Title = "Site Work List"}
>>>>>>> e5f6913... Naviagtion style is updated
            };

            listView.ItemsSource = menuList;            
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            mainPage = Application.Current.MainPage as MasterDetailPage;
                        
            var menu = e.Item as MenuItem;
            switch (menu.Title)
            {
                case "Add Site Work":
                    mainPage.Detail = new NavigationPage(new TransactionEntry());                    
                    break;

                case "Received Cash":
                    mainPage.Detail = new NavigationPage(new UpdatePayment());                    
                    break;

                case "Balance Cash List":
                    mainPage.Detail = new NavigationPage(new PendingAmount());
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

                case "Site Work List":
                    mainPage.Detail = new NavigationPage(new TransactionList());
                    break;

                case "Sync Data":
                    mainPage.Detail = new NavigationPage(new SyncData());
                    break;
            }
            mainPage.IsPresented = false;
        }
    }
}
