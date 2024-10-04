using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TechnuPur.IRepository;
using TechnuPur.Model;

namespace TechnuPur.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository productRepository;
        public ProductController(IProductRepository productRepository) { 
        this.productRepository = productRepository;
        }
        [Authorize(Policy = "RequireAdminRole")]
        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProduct([FromBody] Product product)
        {
          
            return Ok(new {Result= await productRepository.AddProduct(product) });
        }
        [Authorize(Policy = "RequireAdminRole")]
        [HttpPut("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct([FromBody] Product product)
        {

            return Ok(new { Result = await productRepository.UpdateProduct(product) });
        }
        [Authorize(Roles = "Admin , User")]
        [HttpGet("GetAllProducts/{PageNo}/{PageSize}")]
        public async Task<IActionResult> GetAllProducts(int PageNo = 1,int PageSize=10)
        {

            return Ok(new { Result = await productRepository.GetAllProducts(PageNo,PageSize) });
        }
        [Authorize(Policy = "RequireAdminRole")]
        [HttpGet("DeleteProduct/{ID}")]
        public async Task<IActionResult> DeleteProduct(Guid ID)
        {

            return Ok(new { Result = await productRepository.DeleteProduct(ID) });
        }
    }
}
