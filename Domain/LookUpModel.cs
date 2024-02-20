namespace Core.DataModel
{
    public class LookUpModel : BaseEntity
    {
        public int LookTypeId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Value { get; set; }
        public int Sequence { get; set; }
    }
}