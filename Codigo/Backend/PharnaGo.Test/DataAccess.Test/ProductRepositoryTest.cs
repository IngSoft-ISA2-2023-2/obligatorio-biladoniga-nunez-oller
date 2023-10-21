using Microsoft.EntityFrameworkCore;
using PharmaGo.DataAccess;
using PharmaGo.DataAccess.Repositories;
using PharmaGo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmaGo.Test.DataAccess.Test
{
   
        [TestClass]
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        public class ProductRepositoryTests
        {
            private PharmacyGoDbContext context;
            private List<Product> productsSaved;
            private ProductRepository _productRepository;
            private const int invalidId = 500;
            private Pharmacy pharmacy;
            private Product newProduct;
            private const int invalidProductCode = 9999999;

            [TestInitialize]
            public void InitTest()
            {
                productsSaved = new List<Product>();
                pharmacy = new Pharmacy() { Id = 1, Name = "pharmacy", Address = "address", Users = new List<User>() };
                newProduct = new Product()
                {
                    Id = 1,
                    Code = 1,
                    Name = "newdrugName",
                    Description = "newproductdescription",
                    Price = 50,
                    Pharmacy = new Pharmacy()
                    {
                        Id = pharmacy.Id
                    }
                };
            }

            [TestCleanup]
            public void CleanUp()
            {
                context.Database.EnsureDeleted();
            }

            private void CreateDataBase(string name)
            {
                productsSaved = CreateDummyProducts();
                var options = new DbContextOptionsBuilder<PharmacyGoDbContext>().UseInMemoryDatabase(databaseName: name).Options;
                context = new PharmacyGoDbContext(options);
                productsSaved.ForEach(p => context.Set<Product>().Add(p));
                context.Set<Product>().Include("Pharmacy");
                context.SaveChanges();
                _productRepository = new ProductRepository(context);
            }

            [TestMethod]
            public void GetAllProductsOfPharmacyOk()
            {
                CreateDataBase("getProductsTestDb");
                var retrievedProducts = _productRepository.GetAllByExpression(p => true);
                Assert.AreEqual(10, retrievedProducts.Count());
            }

           

            [TestMethod]
            public void GetDrugsByIdOk()
            {
                CreateDataBase("getProductsByIdTestDb");
                Product product = productsSaved[0];
                var retrievedProduct = _productRepository.GetOneByExpression(p => p.Id == product.Id);
                Assert.AreEqual(product.Code, retrievedProduct.Code);
            }

            [TestMethod]
            public void GetProductsByIdNotExists()
            {
                CreateDataBase("getProductsByIdNotExistTestDb");
                var retrievedProduct = _productRepository.GetOneByExpression(p => p.Id == invalidId);
                Assert.AreEqual(null, retrievedProduct);
            }

            [TestMethod]
            public void InsertProductOk()
            {
                CreateDataBase("insertProductTestDb");
                 newProduct.Id = 9999;
                 newProduct.Pharmacy.Id = 9999;
                _productRepository.DeleteOne(newProduct);
                _productRepository.InsertOne(newProduct);
                _productRepository.Save();
                var retrievedProduct = _productRepository.GetOneByExpression(p => p.Id == newProduct.Id);
                Assert.AreEqual(retrievedProduct.Code, newProduct.Code);
            }

            [TestMethod]
            public void ExistProductOk()
            {
                CreateDataBase("ExistProductTestDb");
                bool exists = _productRepository.Exists(p => p.Code == productsSaved[0].Code);
                Assert.IsTrue(exists);
            }

            [TestMethod]
            public void ExistProductFalse()
            {
                CreateDataBase("ExistFalseDrugTestDb");
                bool exists = _productRepository.Exists(p => p.Code == invalidProductCode);
                Assert.IsFalse(exists);
            }

            [TestMethod]
            public void DeleteProductOk()
            {
                CreateDataBase("deleteProductTestDb");
                Product toDelete = productsSaved[0];
                _productRepository.DeleteOne(toDelete);
                _productRepository.Save();
                var productsReturned = _productRepository.GetAllByExpression(d => d.Code == toDelete.Code && d.Pharmacy.Name == toDelete.Pharmacy.Name);
                Assert.IsTrue(productsReturned.Count() == 0);
            }

            [TestMethod]
            public void UpdateProductOk()
            {
                CreateDataBase("updateDrugTestDb");
                var productFromDb = _productRepository.GetOneByExpression(d => d.Code == productsSaved[0].Code);
                var dbName = productFromDb.Name;
                productFromDb.Name = "updatedName";
                _productRepository.UpdateOne(productFromDb);
                _productRepository.Save();
                var productUpdated = _productRepository.GetOneByExpression(d => d.Code == productsSaved[0].Code);
                Assert.AreNotEqual(dbName, productUpdated.Name);
            }

            private List<Product> CreateDummyProducts()
            {
                Pharmacy pharmacy = new Pharmacy() { Id = 1, Name = "pharmacy", Address = "address", Users = new List<User>() };
                var productList = new List<Product>();
                for (int i = 1; i < 11; i++)
                {
                    productList.Add(new Product()
                    {
                        Id = i,
                        Code = i,
                        Name = $"productName{i}",
                        Description = $"testProduct{i}",
                        Price = 100,


                        Pharmacy = pharmacy
                    });
                }
                return productList;
            }
        }
    }

