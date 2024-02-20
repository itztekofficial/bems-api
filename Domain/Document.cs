namespace Core.DataModel
{
    public class Document : BaseEntity
    {
        public int EntityTypeId { get; set; }
        public int CustomerTypeId { get; set; }
        public string? EntityTypeName { get; set; } //For UI Use Only
        public string? CustomerType { get; set; } //For UI Use Only
        public string? Name { get; set; }
        public int Sequence { get; set; }
    }
}