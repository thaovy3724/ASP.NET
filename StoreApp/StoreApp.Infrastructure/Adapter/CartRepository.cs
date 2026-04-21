using Microsoft.EntityFrameworkCore;
using StoreApp.Application.Repository;
using StoreApp.Core.Entities;
using StoreApp.Infrastructure.Data;

namespace StoreApp.Infrastructure.Adapter
{
    public class CartRepository(StoreDbContext context) : BaseRepository<Cart>(context), ICartRepository
    {
        public Task<Cart?> GetByCustomerIdWithItems(Guid customerId)
        {
            return DbSet
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.CustomerId == customerId);
        }

        public async Task<Cart> GetOrCreateByCustomerId(Guid customerId)
        {
            var cart = await GetByCustomerIdWithItems(customerId);
            if (cart is not null)
            {
                return cart;
            }

            cart = new Cart(customerId);
            DbSet.Add(cart);
            await context.SaveChangesAsync();

            return await GetByCustomerIdWithItems(customerId) ?? cart;
        }

        public Task<CartItem?> GetCartItem(Guid cartId, Guid productId)
        {
            return context.Set<CartItem>()
                .FirstOrDefaultAsync(x => x.CartId == cartId && x.ProductId == productId);
        }

        public void AddCartItem(CartItem item)
        {
            context.Set<CartItem>().Add(item);
        }

        public void RemoveCartItem(CartItem item)
        {
            context.Set<CartItem>().Remove(item);
        }

        public void RemoveCartItems(IEnumerable<CartItem> items)
        {
            context.Set<CartItem>().RemoveRange(items.ToList());
        }
    }
}