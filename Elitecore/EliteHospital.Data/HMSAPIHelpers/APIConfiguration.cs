using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliteHospital.Data.HMSAPIHelpers
{
    public class APIConfiguration
    {
        public bool IsFromWebAPI { get; set; }
        private string _baseUrl;
        public string BaseUrl
        {
            get
            {
                if (IsFromWebAPI)
                {
                    return _baseUrl;
                }
                else
                {
                    return GetAppSettingsValue("HMSApiBaseUrl");
                }
            }
            set { this._baseUrl = value; }
        }
        private string _username;
        public string UserName
        {
            get
            {
                if (IsFromWebAPI)
                {
                    return _username;
                }
                else
                {
                    return GetAppSettingsValue("HMSApiUserName");
                }
            }
            set { this._username = value; }
        }
        private string password;
        public string Password
        {
            get
            {
                if (IsFromWebAPI)
                {
                    return password;
                }
                else
                {
                    return GetAppSettingsValue("HMSApiPassword");
                }
            }
            set { this.password = value; }
        }


        private static string GetAppSettingsValue(string appKey)
        {
            string appValue = string.Empty;
            if (ConfigurationManager.AppSettings[appKey] != null)
            {
                appValue = Convert.ToString(ConfigurationManager.AppSettings[appKey]);
            }
            return appValue;
        }
    }
}
