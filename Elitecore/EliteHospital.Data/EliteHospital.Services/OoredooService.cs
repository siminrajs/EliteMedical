using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EliteHospital.Services
{
    public class OoredooService
    {
        public async Task<bool> SendMessageAsync(string mobileNo, string msg)
        {
            //int temp = '0';

            string username = ConfigurationManager.AppSettings["ooredooUsername"];
            string password = ConfigurationManager.AppSettings["ooredooPassword"];
            string customerId = ConfigurationManager.AppSettings["ooredooCustomerId"];
            string Originator = ConfigurationManager.AppSettings["ooredooOriginator"];

            string url = "https://messaging.ooredoo.qa/bms/soap/Messenger.asmx/HTTP_SendSms?" +
                "customerID=" + customerId + "&userName=" + username + "&userPassword=" + password + "" +
                "&originator=" + Originator + "&smsText=" + msg + "&recipientPhone=" + mobileNo + "&messageType=Latin&defDate=&blink=false&flash=false&Private=false";
            using (var client = new HttpClient())
            {
                var uri = new Uri(url);

                var response = await client.GetAsync(uri);

                string textResult = await response.Content.ReadAsStringAsync();
            }
            return true;

            //HttpWebRequest req = (HttpWebRequest)
            //WebRequest.Create("http://doo.ae/api/msgSend.php");
            //req.Method = "POST";
            //req.ContentType = "application/x-www-form-urlencoded";
            //string postData = "mobile=" + username + "&password=" + password + "&numbers=" + numbers + "&sender=" + sender + "&msg=" + msg + "&applicationType=24";
            //req.ContentLength = postData.Length;

            //StreamWriter stOut = new
            //StreamWriter(req.GetRequestStream(),
            //System.Text.Encoding.ASCII);
            //stOut.Write(postData);
            //stOut.Close();
            //// Do the request to get the response
            //string strResponse;
            //StreamReader stIn = new StreamReader(req.GetResponse().GetResponseStream());
            //strResponse = stIn.ReadToEnd();
            //stIn.Close();
            //return strResponse;
        }

        //public bool SendMessage(string message)
        //{
        //    try
        //    {
        //        HttpClient client = new HttpClient();

        //        HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, "https://fcm.googleapis.com/fcm/send");
        //        requestMessage.Headers.Add("Authorization", "key=AAAAG...:APA91bH7U...");
        //        requestMessage.Content = new StringContent(jsonAsStringContent, Encoding.UTF8, "application/json");

        //        HttpResponseMessage response = client.SendAsync(requestMessage).GetAwaiter().GetResult();

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
    }
}
