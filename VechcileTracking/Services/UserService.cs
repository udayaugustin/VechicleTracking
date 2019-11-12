using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Plugin.DeviceInfo;
using SQLite;
using Xamarin.Forms;

namespace VechcileTracking.Services
{
    public class UserService
    {
        private HttpClient httpClient;
        UserDetailResponse userResponse;

        public UserService()
        {
            httpClient = new HttpClient();
        }

        public async Task<UserDetailResponse> GetUserDetail(string userName)
        {
            var url = Constants.FetchUserDetailUrl + "?Mobile=" + userName;
            var content = await httpClient.GetStringAsync(url);
            return userResponse = JsonConvert.DeserializeObject<UserDetailResponse>(content);
        }

        public async Task<bool> UpdateUserDetail(UserUpdate userUpdate)
        {
            var url = Constants.UpdateUserDetailUrl;
            var content = JsonConvert.SerializeObject(userUpdate);
            var response = await httpClient.PostAsync(url, new StringContent(content));

            if (response.IsSuccessStatusCode)
                return true;

            return false;
        }

        public async Task<bool> GenerateAndStoreAppId(UserDetailResponse userResponse)
        {
            var connection = DependencyService.Get<ISQLiteDb>().GetConnection();
            var appId = CrossDeviceInfo.Current.GenerateAppId(false, null, null);

            var userUpdate = new UserUpdate
            {
                Name = userResponse.UserDetails.Name,
                Mobile = userResponse.UserDetails.Mobile,
                AppId = appId,
                UserStatus = "active",
                ActivateDate = userResponse.UserDetails.ActivatedDate,
                ExpireDate = userResponse.UserDetails.ExpireDate,
            };

            //Add the App Id in the server
            var status = await UpdateUserDetail(userUpdate);

            if (status)
            {
                var appInfo = new AppInfo
                {
                    Username = userResponse.UserDetails.Mobile,
                    AppID = appId,
                    IsAppIdValidated = true
                };
                //Store the app id in the sqlite
                await connection.InsertOrReplaceAsync(appInfo);

                return true;
            }

            return false;
        }
    }
}
