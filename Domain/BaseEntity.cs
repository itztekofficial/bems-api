namespace Core.DataModel
{
    public class BaseEntity
    {
        public int CompanyId { get; set; } = 1;
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public int CreatedById { get; set; }
        public DateTime? CreatedOn { get; set; } = DateTime.UtcNow;
        public int UpdatedById { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public string? ActiveStr { get; set; }
        public string? CreatedBy { get; set; }
        public string? CreateDateStr { get; set; }
        public string? UpdatedBy { get; set; }
        public string? ModifyDateStr { get; set; }
    }

    public class Country : BaseEntity
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
        public int Sequence { get; set; }
    }

    public class State : BaseEntity
    {
        public int CountryId { get; set; }
        public string? Country { get; set; } //For UI Only
        public string? Code { get; set; }
        public string? Name { get; set; }
        public int Sequence { get; set; }
    }

    public class City : BaseEntity
    {
        public int StateId { get; set; }
        public string? State { get; set; } //For UI Only
        //public int DistrictId { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public int Sequence { get; set; }
    }

    //public class District : BaseEntity
    //{
    //    public int CityId { get; set; }
    //    public string? Code { get; set; }
    //    public string? Name { get; set; }
    //    public int Sequence { get; set; }
    //}
}