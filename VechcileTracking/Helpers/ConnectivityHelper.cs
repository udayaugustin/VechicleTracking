using System;
using Xamarin.Essentials;

namespace VechcileTracking
{
    public class ConnectivityHelper
    {
        public static bool IsInternetAvailable
        {
            get
            {
                var current = Connectivity.NetworkAccess;

                if (current == NetworkAccess.Internet)
                {
                    return true;
                }

                return false;
            }
        }

    }
}
