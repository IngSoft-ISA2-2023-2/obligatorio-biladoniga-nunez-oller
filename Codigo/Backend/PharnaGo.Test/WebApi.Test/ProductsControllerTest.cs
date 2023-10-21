using Microsoft.AspNetCore.Mvc;
using Moq;
using PharmaGo.Domain.Entities;
using PharmaGo.Exceptions;
using PharmaGo.IBusinessLogic;
using PharmaGo.WebApi.Controllers;
using PharmaGo.WebApi.Models.In;
using PharmaGo.WebApi.Models.Out;

namespace PharmaGo.Test.WebApi.Test
{
    [TestClass]
    public class ProductsControllerTest
    {
        private ProductsController _productsController;
        private Mock<IProductsManager> _productsManagerMock;

        [TestInitialize]
        public void SetUp()
        {
            _productsManagerMock = new Mock<IProductsManager>(MockBehavior.Strict);
            _productsController = new ProductsController(_productsManagerMock.Object);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _productsManagerMock.VerifyAll();
        }

        [TestMethod]
        public void GetProductsOk()
        {
            //Arrange
            _productsManagerMock
                .Setup(service => service.GetProducts())
                .Returns(new List<Product>());

            //Act
            var result = _productsController.GetProducts();

            //Assert
            var objectResult = result as OkObjectResult;
            var statusCode = objectResult.StatusCode;
            var value = objectResult.Value;

            //Assert
            Assert.IsInstanceOfType(value, typeof(IEnumerable<ProductModelOut>));
        }
    }
}
