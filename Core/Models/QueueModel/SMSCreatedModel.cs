namespace Core.Models.QueueModel
{
    /// <summary>
    /// The sms created event.
    /// </summary>
    public class SMSCreatedModel
    {
        /// <summary>
        /// Gets or sets the mobile number.
        /// </summary>
        public string MobileNumber { get; set; }

        /// <summary>
        /// Gets or sets the sms template.
        /// </summary>
        public string SmsTemplate { get; set; }
    }
}