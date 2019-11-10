using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http;
using SQLite;
using VechcileTracking.Services;
using Xamarin.Forms;
using VechcileTracking.Views;

namespace VechcileTracking.Services
{
    public class AccountValidation 
    {
        private SQLiteAsyncConnection connection;
        UserDetailResponse userResponse;
        private UserService userService;
        AppInfo appInfo;

        public AccountValidation()
        {
            Initialize();
        }

        private async void Initialize()
        {
            connection = DependencyService.Get<ISQLiteDb>().GetConnection();
            await connection.CreateTableAsync<AppInfo>();
            appInfo = await connection.Table<AppInfo>().FirstOrDefaultAsync();
            userService = new UserService();            
        }

        public async Task<UserDetailResponse> FetchUserInfo(string username)
        {            
            return userResponse = await userService.GetUserDetail(username);
        }       

        public async Task<bool> IsUserAlreadyLoggedIn()
        {
            appInfo = await connection.Table<AppInfo>().FirstOrDefaultAsync();
            if (appInfo == null || appInfo?.Username == string.Empty)
            {
                return false;
            }

            return true;
        }

        public bool ValidateCredentials()
        {            
            if (userResponse == null || userResponse?.Status == "fail")
            {
                if(appInfo != null)
                {
                    appInfo.IsAppIdValidated = false;
                    connection.UpdateAsync(appInfo);
                }
                    
                return false;
            }            

            return true;
        }

        public bool IsAccountExpired()
        {            
            if (userResponse.UserDetails.ExpireDate.Date >= DateTime.Now.Date)
            {
                return false;
            }

            if (appInfo != null)
            {
                appInfo.IsAppIdValidated = false;
                connection.UpdateAsync(appInfo);
            }

            return true;
        }

        public bool IsFirstTimeLogin()
        {            
            if (userResponse.UserDetails.AppId == "" || userResponse.UserDetails.AppId == string.Empty || userResponse.UserDetails.AppId == null)
            {
                return true;
            }

            return false;
        }

        public bool IsValidAppId()
        {
            if (appInfo == null)
                return false;

            if (userResponse.UserDetails.AppId != appInfo?.AppID)
            {                
                appInfo.IsAppIdValidated = false;
                connection.UpdateAsync(appInfo);

                return false;
            }

            return true;
        }
             
    }
}

