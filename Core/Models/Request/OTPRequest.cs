namespace Core.Models.Request
{
    public class OTPRequest
    {
        public string mobileNo { get; set; }
    }

    public class ValidateOTPRequest
    {
        public string mobileNo { get; set; }
        public string otp { get; set; }
    }
}