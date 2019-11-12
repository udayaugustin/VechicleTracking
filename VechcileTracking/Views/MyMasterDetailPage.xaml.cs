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
                new MenuItem{ Title = "Vehicles List"},
                new MenuItem{ Title = "Customers List"},                
                new MenuItem{ Title = "Site Work List"}
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

                case "Vehicles List":
                    mainPage.Detail = new NavigationPage(new VehicleList());                    
                    break;

                case "Customers List":
                    mainPage.Detail = new NavigationPage(new CustomerList());                    
                    break;

                case "Site Work List":
                    mainPage.Detail = new NavigationPage(new TransactionList());
                    break;


            }
            mainPage.IsPresented = false;
        }

        private void Synch(object sender, EventArgs e)
        {
            mainPage = Application.Current.MainPage as MasterDetailPage;
            mainPage.Detail = new NavigationPage(new SyncData());
        }
    }
}
