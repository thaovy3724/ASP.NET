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

        public void UpdateItem(List<GRNDetail> newItems)
        {
            // Kiểm tra trạng thái của phiếu nhập
            if(Status != GRNStatus.Pending)
            {
                throw new GRNCannotBeUpdatedException("Chỉ có thể chỉnh sửa phiếu nhập ở trạng thái chờ duyệt");
            }

            // Xóa những item không còn trong danh sách mới
            Items.RemoveAll(old => !newItems.Any(n => n.ProductId == old.ProductId));

            // Cập nhật item
            foreach(var item in newItems)
            {
                // tìm theo ProductId nhé 
                var existedItem = Items.FirstOrDefault(x => x.ProductId == item.ProductId);
                if (existedItem != null)
                    existedItem.Update(item.Quantity, item.Price);
                else Items.Add(item);
            }

            UpdatedAt = DateTime.UtcNow;
        }
    }
}
