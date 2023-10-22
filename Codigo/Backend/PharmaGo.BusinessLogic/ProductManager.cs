﻿using PharmaGo.Domain.Entities;
using PharmaGo.Exceptions;
using PharmaGo.IBusinessLogic;
using PharmaGo.IDataAccess;

namespace PharmaGo.BusinessLogic
{
    public class ProductManager : IProductManager
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Session> _sessionRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Pharmacy> _pharmacyRepository;


        public ProductManager(IRepository<Product> productRepository, IRepository<Session> sessionRepository, IRepository<User> userRepository, IRepository<Pharmacy> pharmacyRepository)
        {
            _productRepository = productRepository;
            _sessionRepository = sessionRepository;
            _pharmacyRepository = pharmacyRepository;
            _userRepository = userRepository;
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

        public Product Create(Product product, string token)
        {
            if (product == null)
            {
                throw new ResourceNotFoundException("Please create a drug before inserting it.");
            }
            product.ValidOrFail();

            var guidToken = new Guid(token);
            Session session = _sessionRepository.GetOneByExpression(s => s.Token == guidToken);
            var userId = session.UserId;
            User user = _userRepository.GetOneDetailByExpression(u => u.Id == userId);

            Pharmacy pharmacyOfDrug = _pharmacyRepository.GetOneByExpression(p => p.Name == user.Pharmacy.Name);
            if (pharmacyOfDrug == null)
            {
                throw new ResourceNotFoundException("The pharmacy of the drug does not exist.");
            }

            if (_productRepository.Exists(d => d.Code == product.Code && d.Pharmacy.Name == pharmacyOfDrug.Name))
            {
                throw new InvalidResourceException("The drug already exists in that pharmacy.");
            }
           
            product.Pharmacy.Id = pharmacyOfDrug.Id;
            _productRepository.InsertOne(product);
            _productRepository.Save();
            return product;
        }

    }
}
