﻿using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using PharmaGo.BusinessLogic;
using PharmaGo.Domain.Entities;
using PharmaGo.IBusinessLogic;
using PharmaGo.WebApi.Enums;
using PharmaGo.WebApi.Filters;
using PharmaGo.WebApi.Models.In;
using PharmaGo.WebApi.Models.Out;

namespace PharmaGo.WebApi.Controllers
{
    [Route("api/products")]
    [ApiController]
    [TypeFilter(typeof(ExceptionFilter))]
    public class ProductsController : ControllerBase
    {
        private readonly IProductManager _productsManager;

        public ProductsController(IProductManager manager)
        {
            _productsManager = manager;
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            var products = _productsManager.GetProducts();

            return Ok(products.Select(p => new ProductModelOut(p)));
        }

        [HttpPut("{id}")]
        [AuthorizationFilter(new string[] { nameof(RoleType.Employee) })]
        public IActionResult UpdateProduct([FromRoute] int id, [FromBody] UpdateProductModelIn modelIn)
        {
            var updatedProduct = _productsManager.UpdateProduct(id, modelIn.ToEntity());

            return Ok(new UpdateProductModelOut(updatedProduct));
        }

        [HttpDelete("{id}")]
        [AuthorizationFilter(new string[] { nameof(RoleType.Employee) })]
        public IActionResult DeleteProduct([FromRoute] int id)
        {
            _productsManager.Delete(id);

            return Ok();
        }

        [HttpPost]
        [AuthorizationFilter(new string[] { nameof(RoleType.Employee) })]
        public IActionResult Create([FromBody] ProductModel productModel)
        {
            string token = HttpContext.Request.Headers["Authorization"];
            Product productCreated = _productsManager.Create(productModel.ToEntity(), token);
            ProductModelOut productResponse = new ProductModelOut(productCreated);
            return Ok(productResponse);
        }
    }
}
