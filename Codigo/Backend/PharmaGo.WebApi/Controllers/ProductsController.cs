using Microsoft.AspNetCore.Mvc;
using PharmaGo.IBusinessLogic;
using PharmaGo.WebApi.Models.In;
using PharmaGo.WebApi.Models.Out;

namespace PharmaGo.WebApi.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsManager _productsManager;

        public ProductsController(IProductsManager manager)
        {
            _productsManager = manager;
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            throw new NotImplementedException();
        }
    }
}
