using StoreApp.Application.DTOs;
using StoreApp.Core.Entities;

namespace StoreApp.Application.Mapper
{
    public static class CartMappingExtension
    {
        public static CartDTO ToDTO(this Cart cart, IEnumerable<Product> products)
        {
            var productMap = products.ToDictionary(x => x.Id, x => x);

            var items = cart.Items.Select(item =>
            {
                productMap.TryGetValue(item.ProductId, out var product);

                var price = product?.Price ?? 0m;
                var quantity = item.Quantity;

                return new CartItemDTO(
                    CartItemId: item.Id,
                    ProductId: item.ProductId,
                    ProductName: product?.ProductName ?? "Sản phẩm không tồn tại",
                    ImageUrl: product?.ImageUrl,
                    Price: price,
                    Quantity: quantity,
                    StockQuantity: product?.Quantity ?? 0,
                    TotalPrice: quantity * price
                );
            }).ToList();

            return new CartDTO(
                Id: cart.Id,
                CustomerId: cart.CustomerId,
                Items: items,
                TotalQuantity: items.Sum(x => x.Quantity),
                TotalAmount: items.Sum(x => x.TotalPrice)
            );
        }
    }
}