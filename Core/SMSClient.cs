using Core.Models.QueueModel;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;

namespace Core
{
    public class SMSClient
    {
        public static string VduitURL = "http://mysms.streaminbox.in/vb/apikey.php?";
        public static string VduitApiKey = "5pnLVwSYhl131uoE";
        public static string SMSFrom = "ITZTEK";

        public static bool sendSMSFromVduit(SMSCreatedModel smsRequest)
        {
            bool isSuccess = false;
            string str = $" {VduitURL}apikey={VduitApiKey}&senderid={SMSFrom}&number={smsRequest.MobileNumber.Trim()}&message={smsRequest.SmsTemplate}";

            HttpClient client = new();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

            client.BaseAddress = new Uri(str);
            client.DefaultRequestHeaders.Accept.Clear();
            using (var message = new HttpRequestMessage(HttpMethod.Get, str))
            {
                using var result = client.SendAsync(message).Result;
                using var responseStream = result.Content.ReadAsStringAsync();
                var nresult = JsonConvert.DeserializeObject<VduitSMSResponse>(responseStream.Result);
                isSuccess = (nresult.status == "Success") ? true : false;
            }
            return isSuccess;
        }
    }

    public class VduitSMSResponse
    {
        public string status { get; set; }
        public string code { get; set; }
        public string description { get; set; }
        public Data data { get; set; }
    }

    public class Data
    {
        public string messageid { get; set; }
        public int totnumber { get; set; }
        public int totalcredit { get; set; }
    }
}