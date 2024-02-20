namespace Core.Models.Response;

public class ApprovalResponse : ResponseBase
{
    public string RefId { get; set; }
    public string CustomerName { get; set; }
    public string Entity { get; set; }
    public string EntityType { get; set; }
    public string CustomerType { get; set; }
    public string Agreement { get; set; }
    public string PaymentTerm { get; set; }
    public string RequestStatus { get; set; }
    public int RequestStatusId { get; set; }
    public int PendingReply { get; set; }
    public string Remarks { get; set; }
    public string RequestBy { get; set; }
    public string RequestOn { get; set; }
}