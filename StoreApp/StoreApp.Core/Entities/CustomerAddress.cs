namespace StoreApp.Core.Entities
{
    public class CustomerAddress(Guid customerId, string receiverName, string phone, string addressLine, bool isDefault) : BaseEntity
    {
        public Guid CustomerId { get; private set; } = customerId;
        public string ReceiverName { get; private set; } = receiverName;
        public string Phone { get; private set; } = phone;
        public string AddressLine { get; private set; } = addressLine;
        public bool IsDefault { get; private set; } = isDefault;
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow.AddHours(7);

        public void Update(string receiverName, string phone, string addressLine)
        {
            ReceiverName = receiverName;
            Phone = phone;
            AddressLine = addressLine;
        }

        public void SetDefault()
        {
            IsDefault = true;
        }

        public void UnsetDefault()
        {
            IsDefault = false;
        }

        public string ToSnapshot()
        {
            return $"{ReceiverName} - {Phone} - {AddressLine}";
        }
    }
}
