﻿using PharmaGo.Domain.Entities;
using PharmaGo.Exceptions;
using PharmaGo.IBusinessLogic;
using PharmaGo.IDataAccess;

namespace PharmaGo.BusinessLogic
{
    public class ProductManager : IProductManager
    {
        private readonly IRepository<Product> _productRepository;

        public ProductManager(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public void Delete(int id)
        {
            var productSaved = _productRepository.GetOneByExpression(d => d.Id == id);
            if (productSaved == null)
            {
                throw new ResourceNotFoundException("The product to delete does not exist.");
            }
            productSaved.Deleted = true;
            _productRepository.UpdateOne(productSaved);
            _productRepository.Save();
        }

        public List<Product> GetProducts()
        {
            var products = _productRepository.GetAllByExpression(p => !p.Deleted);

            return products.ToList();
        }

        public Product UpdateProduct(int id, Product product)
        {
            var productSaved = _productRepository.GetOneByExpression(d => d.Id == id);
            if (productSaved == null)
            {
                throw new ResourceNotFoundException("The product to update does not exist.");
            }
            
            if (product.Name != null)
            {
                productSaved.Name = product.Name;
            }

            if (product.Price != 0)
            {
                productSaved.Price = product.Price;
            }

            if (product.Description != null)
            {
                productSaved.Description = product.Description;
            }

            _productRepository.UpdateOne(productSaved);
            _productRepository.Save();

            return productSaved;
        }
    }
}
