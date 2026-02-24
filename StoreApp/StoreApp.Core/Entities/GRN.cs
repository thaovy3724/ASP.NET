using StoreApp.Core.ValueObject;

namespace StoreApp.Core.Entities
{
    public class GRN(Guid supplierId) : BaseEntity
    {
        public Guid SupplierId { get; private set; } = supplierId;
        public GRNStatus Status { get; private set; } = GRNStatus.Pending;

        public DateTime UpdatedAt { get; private set; } = DateTime.Now;
        public List<GRNDetail> Items { get; private set; } = [];

        public void UpdateInventoryStatus(GRNStatus grnStatus)
        {
            Status = grnStatus;
            UpdatedAt = DateTime.Now;
        }
    }
}
