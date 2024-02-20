namespace Core.DataModel
{
    public class PaymentTerm : BaseEntity
    {
        public string? Name { get; set; }
        public int Sequence { get; set; }
    }
}