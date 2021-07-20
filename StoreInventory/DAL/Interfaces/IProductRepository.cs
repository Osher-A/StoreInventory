using System.Collections.Generic;

namespace StoreInventory.DAL.Interfaces
{
    public interface IProductRepository
    {
        void AddingProduct(DTO.Product newDtoProduct);
        void DeletingProduct(int productId);
        void EditingProduct(DTO.Product productToEdit);
        List<Model.Product> GetProducts();
    }
}