using PharmaGo.Domain.Entities;
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
            var products = _productRepository.GetAllByExpression(_ => true);

            return products.ToList();
        }
    }
}
