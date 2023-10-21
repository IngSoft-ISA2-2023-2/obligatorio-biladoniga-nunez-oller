using Moq;
using PharmaGo.BusinessLogic;
using PharmaGo.Domain.Entities;
using PharmaGo.Exceptions;
using PharmaGo.IDataAccess;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace PharmaGo.Test.BusinessLogic.Test
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ProductManagerTest
    {
        private Mock<IRepository<Product>> _productRepository;
        private ProductManager _productManager;
        private Product product, nullProduct;
        private Pharmacy pharmacy;

        [TestInitialize]
        public void InitTest()
        {
            _productRepository = new Mock<IRepository<Product>>();
            _productManager = new ProductManager(_productRepository.Object );
            pharmacy = new Pharmacy() { Id = 1, Name = "pharmacy", Address = "address", Users = new List<User>() };
            product = new Product()
            {
                Id = 1,
                Code = 12345,
                Name = "Shampoo Sedal 200 ml",
                Description = "Dale vida a tu pelo con el nuevo shampoo Sedal",
                Price = 75,
                Deleted = false,
                Pharmacy = pharmacy
            };
            nullProduct = null;
        }

        [TestMethod]
        public void DeleteProductOk()
        {
            _productRepository.Setup(r => r.GetOneByExpression(It.IsAny<Expression<Func<Product, bool>>>())).Returns(product);
            _productRepository.Setup(x => x.UpdateOne(product));
            _productRepository.Setup(x => x.Save());
            _productManager.Delete(product.Id);
            _productRepository.VerifyAll();
        }
        
        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public void DeleteNullProduct()
        {
            _productRepository.Setup(r => r.GetOneByExpression(It.IsAny<Expression<Func<Product, bool>>>())).Returns(nullProduct);
            _productManager.Delete(product.Id);
            _productRepository.VerifyAll();
        }

    }
}
