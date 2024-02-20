using Core.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Admin.Api.Controllers
{
    /// <summary>
    /// StatusController
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class StatusController : ControllerBase
    {
        private readonly ILogger<StatusController> _logger;

        /// <summary>
        /// StatusController
        /// </summary>
        /// <param name="logger"></param>
        public StatusController(ILogger<StatusController> logger)
        {
            _logger = logger;
            _logger.LogInformation("StatusController Initialized");
        }

        /// <summary>
        /// Get
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = "Gt")]
        public string Get()
        {
            //MailConfiguration configuration = new()
            //{
            //    SMPTServer = "smtp.gmail.com",
            //    UserDisplayName = "AKUMS - CLM",
            //    UserEMail = "crm@softnect.com",
            //    UserEMailPassword = "qdjsugdsfynymzlj",
            //    Port = 587,
            //    IsSSL = true               
            //};

            //MessageData messageData = new()
            //{
            //    ToMail = new List<string> { "rupesh5681@gmail.com" },
            //    Subject = "Test mail",
            //    Message = "This is test mail...",

            //    IsHtmlMessage = true
            //};

            //List<MessageData> messages = new()
            //{
            //    messageData
            //};
            
            //var result = new Dictionary<object, Exception>();
            //Exception exception = null;
            ////test email
            //bool isMailSent = MailSMS.SendEMail( messages, configuration, out result, ref exception);

            ////test SMS
            //SMSCreatedModel smsCreatedEvent = new()
            //{
            //    MobileNumber = "9990973577",
            //    SmsTemplate = "Dear User, Your OTP is 123423 . Please do not share the OTP with anyone. Have a good day!  Thank you, Marg Books"
            //};
            //int data = SMSClient.sendSMSFromVduit(smsCreatedEvent);

            //QueueManager queueManager = new();
            //var message = Newtonsoft.Json.JsonConvert.SerializeObject(smsCreatedEvent);
            //queueManager.SendDataInQueue("sms.created.event", message, EnumBusConnection.Notification);

            return "OK";
        }

    }
}