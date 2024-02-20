using Core.DataModel;

namespace Core.Models.Response
{
    public class CompanyResponse : BaseEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string ContactPerson { get; set; }
        public string EmailId { get; set; }
        public string RegMobile { get; set; }
        public string Address { get; set; }
        public string GSTNNo { get; set; }
    }
}