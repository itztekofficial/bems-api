namespace Core.DataModel
{
    public class Otp
    {
        public int Id { get; set; }
        public string? MobileNo { get; set; }
        public string? MobileOTP { get; set; }
        public bool IsActive { get; set; }
        public DateTime? LastUpdated { get; set; }
    }
}