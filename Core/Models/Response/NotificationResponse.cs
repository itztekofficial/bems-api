namespace Core.Models.Response;

public class NotificationResponse
{
    public int InitiationId { get; set; }
    public string CustomerName { get; set; }
    public string EntityName { get; set; }
    public string EffectiveDate { get; set; }
    public string RenewalDueDate { get; set; }
}
