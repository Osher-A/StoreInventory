using System.Collections.Generic;

namespace StoreInventory.Interfaces
{
    public interface IProductRepository
    {
        void AddingProduct(IProduct newusersProduct);
        void DeletingProduct(int productId);
        void EditingProduct(IProduct productToEdit);
        List<IProduct> GetProducts();
    }
}