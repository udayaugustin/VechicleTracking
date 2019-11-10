using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using SQLite;
using VechcileTracking.Services;
using Xamarin.Forms;

namespace VechcileTracking.Views
{
    public partial class InitialValidation : ContentPage
    {
        private SQLiteAsyncConnection connection;
        private HttpClient httpClient;
        UserDetailResponse userResponse;
        private UserService userService;
        AccountValidation accountValidation;

        public InitialValidation()
        {
            InitializeComponent();

            accountValidation = new AccountValidation();
            httpClient = new HttpClient();
            connection = DependencyService.Get<ISQLiteDb>().GetConnection();
            userService = new UserService();            
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await TriggerInitialValidation();
        }

        private async Task TriggerInitialValidation()
        {
            await connection.CreateTableAsync<AppInfo>();

            if (!(await accountValidation.IsUserAlreadyLoggedIn()))
            {
                Application.Current.MainPage = new LoginPage();
                return;
            }
            else
            {
                if (ConnectivityHelper.IsInternetAvailable)
                    await OnlineValidation();
                else
                    await OfflineValidation();
            }           
        }

        private async Task OfflineValidation()
        {
            var appInfo = await connection.Table<AppInfo>().FirstOrDefaultAsync();
            if (appInfo.IsAppIdValidated)
                NavigateToMasterPage();
            else
            {
                Application.Current.MainPage = new LoginPage();
            }                
        }

        private async Task OnlineValidation()
        {
            var appInfo = await connection.Table<AppInfo>().FirstOrDefaultAsync();
            //Must call this method before validating the user response
            userResponse = await accountValidation.FetchUserInfo(appInfo.Username);

            if (!accountValidation.ValidateCredentials())
            {
                var errorMessage = "Invalid Login";
                Application.Current.MainPage = new LoginPage(errorMessage);
                return;
            }

            if (accountValidation.IsAccountExpired())
            {
                var errorMessage = "Your Account Expired. Please contact support.";
                Application.Current.MainPage = new LoginPage(errorMessage);
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

        private void NavigateToMasterPage()
        {
            var mainPage = Application.Current.MainPage;
            var detailPage = new NavigationPage(new Reports());

            mainPage = new MyMasterDetailPage();
            (mainPage as MasterDetailPage).Detail = detailPage;

            Application.Current.MainPage = mainPage;
        }
    }
}
