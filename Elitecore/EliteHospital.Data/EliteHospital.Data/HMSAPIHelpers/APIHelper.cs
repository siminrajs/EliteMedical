using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EliteHospital.Data.HMSAPIHelpers
{
    public class APIHelper : APIConfiguration
    {
        public T ExecuteRequest<T>(string method, string requestBodyJson = null)
        {
            var requestUrl = BaseUrl + method;
           
            var webRequest = WebRequest.Create(requestUrl);
            webRequest.Method = "POST";
            webRequest.ContentType = "application/json";
            if (requestBodyJson == null)
                webRequest.ContentLength = 0;

            var plainTextBytes = Encoding.UTF8.GetBytes($"{UserName}:{Password}");
            string val = Convert.ToBase64String(plainTextBytes);
            webRequest.Headers.Add(HttpRequestHeader.Authorization, "Basic " + val);

            if (requestBodyJson != null)
                using (var requestWriter = new StreamWriter(webRequest.GetRequestStream()))
                {
                    requestWriter.Write(requestBodyJson);
                    requestWriter.Flush();
                }

            try
            {
                using (var response = webRequest.GetResponse())
                {
                    using (var responseReader = new StreamReader(response.GetResponseStream()))
                    {
                        T resultObject = JsonConvert.DeserializeObject<T>(responseReader.ReadToEnd());
                        return resultObject;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
