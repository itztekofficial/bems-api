namespace Core.DataModel
{
    public class Product : BaseEntity
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
        public int Sequence { get; set; }
    }
}