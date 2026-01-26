using StoreApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Application.Ports.Output
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        public Task<List<Product>> searchByKeyword(string keyword);
        public Task<List<Product>> getBySupplierID(Guid? supplier_id);
        public Task<List<Product>> getByCategoryID(Guid? category_id);
        public Task<List<Product>> getProductsSortByPrice(string? order);
        public Task<List<Product>> getProductsBySupplierIDAndCategoryID(Guid? supplier_id, Guid? category_id);
        public Task<List<Product>> getProductsBySupplierIDAndPrice(Guid? supplier_id, string? order);
        public Task<List<Product>> getProductsByCategoryIDAndPrice(Guid? category_id, string? order);
        public Task<List<Product>> getProductsBysupplierIDAndCategoryIDAndPriceAndKeyword(Guid? supplier_id, Guid? category_id, string? order, string? keyword);
        public Task<bool> checkExistBarcode(string barcode);
        public Task<bool> checkExistID(Guid ID);
        public Task<bool> checkBarcodeExistForOtherProducts(Guid id, string barcode);
    }
}
