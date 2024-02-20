using System.Collections.Generic;

namespace Core.Models.Response;

public class CompanyDashBoardResponse
{
    public dynamic ContractCountResponse { get; set; }
    public MyRequestGraphRepsonse MyRequestGraphRepsonse { get; set; }
    public IEnumerable<MyContractResponse> MyContractResponses { get; set; }
}

//public class ContractCountResponse
//{
//    public int PendingAction { get; set; }
//    public int Requested { get; set; }
//    public int Drafts { get; set; }
//    public int Negotiation { get; set; }
//    public int Execution { get; set; }
//    public int Executed { get; set; }
//    public int SharedWithCustomer { get; set; }
//}

public class MyRequestGraphRepsonse
{
    public string JAN { get; set; }
    public string FEB { get; set; }
    public string MAR { get; set; }
    public string APR { get; set; }
    public string MAY { get; set; }
    public string JUN { get; set; }
    public string JUL { get; set; }
    public string AUG { get; set; }
    public string SEP { get; set; }
    public string OCT { get; set; }
    public string NOV { get; set; }
    public string DEC { get; set; }
}

public class MyContractResponse
{
    public int Id { get; set; }
    public string RefId { get; set; }
    public string CustomerName { get; set; }
    public string Entity { get; set; }
    public string EntityType { get; set; }
    public string CustomerType { get; set; }
    public string Agreement { get; set; }
    public string PaymentTerm { get; set; }
    public int RequestStatusId { get; set; }  
    public int SubStatusId { get; set; }
    public string RequestStatus { get; set; }
    public int PendingReply { get; set; }
    public string RequestBy { get; set; }
    public string RequestOn { get; set; }
}

public class RequestHistoryResponse
{
    public string InitiationId { get; set; }
    public string RequestStatus { get; set; }
    public string Comment { get; set; }
    public string Remarks { get; set; }
    public string CreatedBy { get; set; }
    public string CreateDate { get; set; }
}