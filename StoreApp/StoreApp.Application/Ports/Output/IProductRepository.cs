using StoreApp.Application.Repository;
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
        public Task<List<Product>> getProductsBysupplierIDAndCategoryIDAndPriceAndKeyword(Guid? supplier_id, Guid? category_id, string? order, string? keyword);
        public Task<bool> checkExistBarcode(string barcode);
        public Task<bool> checkExistID(Guid ID);
        public Task<bool> checkBarcodeExistForOtherProducts(Guid id, string barcode);
    }
}
