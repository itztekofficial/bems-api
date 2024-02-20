namespace Core.DataModel
{
    public class FieldMapping : BaseEntity
    {
        public string? Name { get; set; }
        public bool IsVisible { get; set; }
        public bool IsRequired { get; set; }
        public int Sequence { get; set; }
    }
}