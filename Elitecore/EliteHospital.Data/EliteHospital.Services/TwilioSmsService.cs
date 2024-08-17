using System.Configuration;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace EliteHospital.Services
{
    public class TwilioSmsService
    {
        public MessageResource SendMessage(string phoneNumber,string message)
        {
            string accountSid = ConfigurationManager.AppSettings["TwilioAccountSid"];
            string authToken = ConfigurationManager.AppSettings["TwilioAuthToken"];
            string twilioPhoneNumber = ConfigurationManager.AppSettings["TwilioPhoneNumber"];

            TwilioClient.Init(accountSid, authToken);

            var messageResponse = MessageResource.Create(
                body: message,
                from: new Twilio.Types.PhoneNumber(twilioPhoneNumber),
                to: new Twilio.Types.PhoneNumber(phoneNumber)
            );

            return messageResponse;
        }
    }
}
