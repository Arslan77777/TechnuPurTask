using TechnuPur.Model;

namespace TechnuPur.IRepository
{
    public interface IProductRepository
    {
        Task<bool> AddProduct(Product product);
        Task<bool> UpdateProduct(Product product);
        Task<bool> DeleteProduct(Guid Id);
        Task<List<Product>> GetAllProducts(int PageNo = 1, int PageSize = 10);
    }
}
