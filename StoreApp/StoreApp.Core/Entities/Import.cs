namespace StoreApp.Core.Entities
{
    public class Import : BaseEntity
    {
        public Guid SupplierId { get; private set; }
        public Guid UserId { get; private set; }
        public decimal TotalAmount => ImportDetails?.Sum(d => d.Subtotal) ?? 0;
        public DateTime ImportDate { get; private set; } = DateTime.Now;
        public List<ImportDetail> ImportDetails { get; private set; } = [];
    }
}
