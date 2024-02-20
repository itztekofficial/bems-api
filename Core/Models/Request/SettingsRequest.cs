namespace Core.Models.Request;

public class PWDChangeRequest
{
    public int Id { get; set; }
    public string UserPwd { get; set; }
    public int ModifiedById { get; set; } //Modified BY Id
}