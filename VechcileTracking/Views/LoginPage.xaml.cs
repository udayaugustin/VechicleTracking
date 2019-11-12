using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Plugin.DeviceInfo;
using SQLite;
using VechcileTracking.Services;
using Xamarin.Forms;

namespace VechcileTracking.Views
{
    public partial class LoginPage : ContentPage
    {
        private SQLiteAsyncConnection connection;
        private HttpClient httpClient;
        UserDetailResponse userResponse;
        private UserService userService;
        AccountValidation accountValidation;

        public LoginPage(string errorMessge = null)
        {
            InitializeComponent();
            httpClient = new HttpClient();
            connection = DependencyService.Get<ISQLiteDb>().GetConnection();            
            userService = new UserService();
            ErrorLabel.Text = errorMessge;
            accountValidation = new AccountValidation();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();            
        }

        private async void Login(object sender, EventArgs e)
        {
            Loader.IsRunning = true;
            if (!ConnectivityHelper.IsInternetAvailable)
            {
                ErrorLabel.Text = "Please enable internet to login";
                StopLoader();
                return;
            }


            if (Username.Text != string.Empty)
            {
                userResponse = await accountValidation.FetchUserInfo(Username.Text);

                if (!accountValidation.ValidateCredentials())
                {
                    ErrorLabel.Text = "Login Failed";
                    StopLoader();
                    return;
                }

                if (accountValidation.IsAccountExpired())
                {
                    ErrorLabel.Text = "Your Account Expired. Please contact support.";
                    StopLoader();
                    return;
                }

                if (accountValidation.IsFirstTimeLogin())
                {
                    await userService.GenerateAndStoreAppId(userResponse);                    
                    NavigateToMasterPage();
                }
                else
                {
                    var isValidAppId = accountValidation.IsValidAppId();
                    if (isValidAppId)
                        NavigateToMasterPage();
                    else
                        Application.Current.MainPage = new LicenseViolation();
                }                
            }
        }        

        private void NavigateToMasterPage()
        {
            StopLoader();

            var mainPage = Application.Current.MainPage;
            var detailPage = new NavigationPage(new Reports());

            mainPage = new MyMasterDetailPage();
            (mainPage as MasterDetailPage).Detail = detailPage;

            Application.Current.MainPage = mainPage;
        }

        private void StopLoader()
        {
            Loader.IsRunning = false;
        }
    }
}
