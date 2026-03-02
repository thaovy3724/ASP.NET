using StoreApp.Core.Exceptions;
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

        public void AddItem(Guid productId, int quantity, decimal price)
        {
            var item = new GRNDetail(Id, productId, quantity, price);
            Items.Add(item);
        }

        public void CancelGRN()
        {
            if (Status != GRNStatus.Pending)
            {
                throw new GRNCannotBeCanceledException("Chỉ có thể hủy GRN ở trạng thái chờ duyệt.");
            }
            Status = GRNStatus.Canceled;
            UpdatedAt = DateTime.Now;
        }

        public void MarkAsCompleted()
        {
            if (Status != GRNStatus.Pending)
            {
                throw new GRNCannotBeCompletedException("Chỉ có thể hoàn thành GRN ở trạng thái chờ duyệt.");
            }
            Status = GRNStatus.Completed;
            UpdatedAt = DateTime.Now;
        }
    }
}
