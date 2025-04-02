using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace BackendAPI.Services
{
    public class SmsService
    {
        private readonly string _accountSid;
        private readonly string _authToken;
        private readonly string _fromNumber;

        public SmsService(IConfiguration configuration)
        {
            _accountSid = configuration["Twilio:AccountSid"] ?? throw new ArgumentNullException("Twilio AccountSid is missing.");
            _authToken = configuration["Twilio:AuthToken"] ?? throw new ArgumentNullException("Twilio AuthToken is missing.");
            _fromNumber = configuration["Twilio:FromNumber"] ?? throw new ArgumentNullException("Twilio FromNumber is missing.");

            TwilioClient.Init(_accountSid, _authToken);
        }

        public async Task<bool> SendSmsAsync(string to, string message)
        {
            try
            {
                Console.WriteLine($"Attempting to send SMS to: {to}");

                var result = await MessageResource.CreateAsync(
                    to: new PhoneNumber(to),
                    from: new PhoneNumber(_fromNumber),
                    body: message
                );

                Console.WriteLine($"Twilio Response: SID={result.Sid}, Status={result.Status}, Error={result.ErrorMessage}");

                return result.ErrorCode == null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending SMS: {ex.Message}");
                return false;
            }
        }

    }
}
