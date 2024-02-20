namespace Core.DataModel
{
    public class Entity : BaseEntity
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
        public int Sequence { get; set; }
    }
}