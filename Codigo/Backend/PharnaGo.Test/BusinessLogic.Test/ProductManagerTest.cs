using Moq;
using NuGet.Common;
using PharmaGo.BusinessLogic;
using PharmaGo.DataAccess.Repositories;
using PharmaGo.Domain.Entities;
using PharmaGo.Exceptions;
using PharmaGo.IBusinessLogic;
using PharmaGo.IDataAccess;
using PharmaGo.WebApi.Models.In;
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
        private string token = "c80da9ed-1b41-4768-8e34-b728cae25d2f";
        private const Pharmacy nullPharmacy = null;

        private Mock<IRepository<Pharmacy>> _pharmacyRepository;
        private Mock<IRepository<User>> _userRepository;
        private Mock<IRepository<Session>> _sessionRepository;
        private Session session = null;
        private User user = null;
        private ProductModel productModel;

        [TestInitialize]
        public void InitTest()
        {
            _productRepository = new Mock<IRepository<Product>>();
            _userRepository = new Mock<IRepository<User>>();
            _sessionRepository = new Mock<IRepository<Session>>();
            _pharmacyRepository = new Mock<IRepository<Pharmacy>>();
            _productManager = new ProductManager(_productRepository.Object, _sessionRepository.Object, _userRepository.Object, _pharmacyRepository.Object );
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
            session = new Session { Id = 1, Token = new Guid(token), UserId = 1 };
            user = new User() { Id = 1, UserName = "test", Email = "test@gmail.com", Address = "test" };
            nullProduct = null;
            pharmacy = new Pharmacy() { Id = 1, Name = "pharmacy", Address = "address", Users = new List<User>() };
            productModel = new ProductModel()
            {
                Code = 6514,
                Name = "name",
                Price = 200,
                PharmacyName = "pharmacy",
                Description = "test"
            };
            session = new Session { Id = 1, Token = new Guid(token), UserId = 1 };
            user = new User() { Id = 1, UserName = "test", Email = "test@gmail.com", Address = "test" };
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

        [TestMethod]
        public void GetProducts()
        {
            _productRepository.Setup(r => r.GetAllByExpression(It.IsAny<Expression<Func<Product, bool>>>())).Returns(new List<Product>());

            var products = _productManager.GetProducts();

            Assert.IsInstanceOfType(products, typeof(List<Product>));
        }

        [TestMethod]
        public void UpdateProduct()
        {
            _productRepository.Setup(r => r.GetOneByExpression(It.IsAny<Expression<Func<Product, bool>>>())).Returns(new Product());
            _productRepository.Setup(r => r.UpdateOne(It.IsAny<Product>()));

            var product = _productManager.UpdateProduct(0, new Product()
            {
                Description = "Test",
                Price = 10,
                Name = "Test"
            });

            Assert.IsInstanceOfType(product, typeof(Product));
        }

        [TestMethod]
        public void CreateProductOk()
        {
            _productRepository.Setup(r => r.Exists(It.IsAny<Expression<Func<Product, bool>>>())).Returns(false);
            _pharmacyRepository.Setup(r => r.GetOneByExpression(It.IsAny<Expression<Func<Pharmacy, bool>>>())).Returns(pharmacy);
            
            _sessionRepository.Setup(r => r.GetOneByExpression(It.IsAny<Expression<Func<Session, bool>>>())).Returns(session);
            _userRepository.Setup(r => r.GetOneDetailByExpression(It.IsAny<Expression<Func<User, bool>>>())).Returns(user);
            _productRepository.Setup(x => x.InsertOne(It.IsAny<Product>()));
            _productRepository.Setup(x => x.Save());

            var productReturned = _productManager.Create(productModel.ToEntity(), token);

            // Assert
            _productRepository.VerifyAll();
            Assert.AreNotEqual(productReturned.Id, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public void CreateProductWithExistentCode()
        {
            _sessionRepository.Setup(r => r.GetOneByExpression(It.IsAny<Expression<Func<Session, bool>>>())).Returns(session);
            _userRepository.Setup(r => r.GetOneDetailByExpression(It.IsAny<Expression<Func<User, bool>>>())).Returns(user);
            var drugReturned = _productManager.Create(product, token);
        }

        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public void CreateNullProduct()
        {
            var drugReturned = _productManager.Create(null, token);
        }

        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public void CreateDrugNullProduct()
        {
            _sessionRepository.Setup(r => r.GetOneByExpression(It.IsAny<Expression<Func<Session, bool>>>())).Returns(session);
            _userRepository.Setup(r => r.GetOneDetailByExpression(It.IsAny<Expression<Func<User, bool>>>())).Returns(user);
            _pharmacyRepository.Setup(r => r.GetOneByExpression(It.IsAny<Expression<Func<Pharmacy, bool>>>())).Returns(nullPharmacy);
            var drugReturned = _productManager.Create(product, token);
        }
    }
}
