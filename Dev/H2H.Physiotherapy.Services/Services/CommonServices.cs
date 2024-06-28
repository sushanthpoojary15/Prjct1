using H2H.Physiotherapy.Services.Abstractions.IServices;
using H2H.Physiotherapy.Services.Configs;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2H.Physiotherapy.Services.Services
{
    public class CommonServices : ICommonServices
    {
        private static readonly Random _random = new Random();
        public CommonServices()
        {
           
        }
        public async Task<int> Generate8DigitReferenceNumber()
        {
            char[] referenceNumber = new char[8];
            for (int i = 0; i < 8; i++)
            {
                referenceNumber[i] = (char)('0' + _random.Next(0, 10));
            }
            string referenceNumberString = new string(referenceNumber);
            return int.Parse(referenceNumberString);
        }

        public async Task<int> Generate6DigitOtp()
        {
            const int otpLength = 6;
            const string digits = "0123456789";

            // Shuffle the digits to get a random order
            var shuffledDigits = digits.OrderBy(_ => _random.Next()).ToArray();

            // Take the first 6 digits from the shuffled list
            var otpDigits = shuffledDigits.Take(otpLength).ToArray();

            // Convert the array of characters to a string, then parse to an integer
            string otpString = new string(otpDigits);
            return int.Parse(otpString);
        }

    }
    
}
