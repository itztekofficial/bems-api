using Core.DataModel;

namespace Domain;

public class Company : BaseEntity
{
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? ContactPerson { get; set; }
    public string? EmailId { get; set; }
    public string? RegMobile { get; set; }
    public string? Address { get; set; }
    public string? GSTNNo { get; set; }
    public int CountryId { get; set; }
    public int StateId { get; set; }
    public int CityId { get; set; }
    public string? PinCode { get; set; }
}