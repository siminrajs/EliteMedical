using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace EliteHospitalWebAPI.Models
{
    public class OoredooServiceAPI
    {
        string username;
        string password;
        string customerid;
        string originator;
        public OoredooServiceAPI(string _username, string _password, string _customerid, string _originator)
        {
            username = _username;
            password = _password;
            customerid = _customerid;
            originator = _originator;
        }

        public async Task<bool> SendMessageAsync(string mobileNo, string msg)
        {
            //int temp = '0';
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            ServicePointManager.ServerCertificateValidationCallback +=
(sender, cert, chain, sslPolicyErrors) => true;
            string url = "https://messaging.ooredoo.qa/bms/soap/Messenger.asmx/HTTP_SendSms?" +
                "customerID=" + customerid + "&userName=" + username + "&userPassword=" + password + "" +
                "&originator=" + originator + "&smsText=" + msg + "&recipientPhone=" + mobileNo + "&messageType=Latin&defDate=&blink=false&flash=false&Private=false";
            using (var client = new HttpClient())
            {
                var uri = new Uri(url);

                var response = await client.GetAsync(uri);

                string textResult = await response.Content.ReadAsStringAsync();
            }
            return true;
        }

        internal void SendMessageAsync()
        {
            throw new NotImplementedException();
        }
    }
}
