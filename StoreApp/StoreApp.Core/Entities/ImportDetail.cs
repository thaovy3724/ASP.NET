namespace StoreApp.Core.Entities
{
    public class ImportDetail : BaseEntity
    {
        public Guid ImportId { get; private set; }
        public Guid ProductId { get; private set; }
        public int Quantity { get; private set; }
        public decimal Price { get; private set; }
        public decimal Subtotal => Quantity * Price;
        //public Import Import { get; set; }

    }
}
