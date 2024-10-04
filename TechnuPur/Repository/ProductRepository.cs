using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using TechnuPur.DatabaseContext;
using TechnuPur.IRepository;
using TechnuPur.Model;

namespace TechnuPur.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDatabaseContext  _context;
        public ProductRepository(AppDatabaseContext context) {
        _context = context;
        }
        [Authorize]
        async Task<bool> IProductRepository.AddProduct(Product product)
        {
            try
            {
                product.ID= Guid.NewGuid();
                _context.products.Add(product);
                return await _context.SaveChangesAsync()>0;
            }
            catch(Exception ex) {

                return false;
            }
        }
        [Authorize]
        async Task<bool> IProductRepository.DeleteProduct(Guid Id)
        {
            try
            {
                var product = _context.products.Find(Id);
                if (product != null)
                {
                    _context.products.Remove(product);
                    return await _context.SaveChangesAsync() > 0;
                }
                return false;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        async Task<List<Product>> IProductRepository.GetAllProducts(int PageNo = 1, int PageSize = 10)
        {
            return await _context.products
                          .Skip((PageNo - 1) * PageSize)  
                          .Take(PageSize)                 
                          .ToListAsync();
        }
        [Authorize]
        async Task<bool> IProductRepository.UpdateProduct(Product product)
        {
            try
            {
                _context.products.Update(product);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
    }
}
