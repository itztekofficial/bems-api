namespace Core.Models.Response;

public class AdminDashBoardResponse
{
    public AllMastersCountResponse AllMastersCount { get; set; }
}

public class AllMastersCountResponse
{
    public int TDepartment { get; set; }
    public int TDocument { get; set; }
    public int TEntity { get; set; }
    public int TEntityType { get; set; }
    public int TAgreement { get; set; }
    public int TSubAgreement { get; set; }
    public int TPaymentTerm { get; set; }
    public int TUser { get; set; }
}