using StoreApp.Core.Entities;

namespace StoreApp.Application.Repository
{
    public interface ICartRepository : IBaseRepository<Cart>
    {
        Task<Cart?> GetByCustomerIdWithItems(Guid customerId);
        Task<Cart> GetOrCreateByCustomerId(Guid customerId);

        Task<CartItem?> GetCartItem(Guid cartId, Guid productId);
        void AddCartItem(CartItem item);
        void RemoveCartItem(CartItem item);
        void RemoveCartItems(IEnumerable<CartItem> items);
    }
}