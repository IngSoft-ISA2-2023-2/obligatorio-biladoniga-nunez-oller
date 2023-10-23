using Newtonsoft.Json;
using PharmaGo.DataAccess.Repositories;
using PharmaGo.Domain.Entities;
using PharmaGo.WebApi.Models.In;
using System;
using System.Net;
using System.Text;
using TechTalk.SpecFlow;

namespace SpecFlowPharmaGo.WebApi.StepDefinitions
{
    [Binding]
    public class InsertProductStepDefinitions
    {

        private readonly ScenarioContext context;
        private readonly Product _product = new Product();
        private readonly ProductModel _productModel = new ProductModel();
        public InsertProductStepDefinitions(ScenarioContext context)
        {
            this.context = context;
        }

        [Given(@"the name product(.*) of the product")]
        public void GivenTheNameProductOfTheProduct(string name)
        {
            _product.Name = name;
            _productModel.Name = name;
        }

        [Given(@"the description(.*) of the product")]
        public void GivenTheDescriptionNewExcerciseBallOfTheProduct(string description)
        {
            _product.Description = description;
            _productModel.Description = description;
        }


        [Given(@"the code (.*) of the product")]
        public void GivenTheCodeOfTheProduct(int code)
        {
            _product.Code = code;
            _productModel.Code = code;
        }

        [Given(@"the price (.*) of the product")]
        public void GivenThePriceOfTheProduct(decimal price)
        {
            _product.Price = price;
            _productModel.Price = price;
        }

        [When(@"a user wants to add it to the system")]
        public async Task WhenAUserWantsToAddItToTheSystemAsync()
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            var client = new HttpClient(clientHandler);
            client.DefaultRequestHeaders.Add("Authorization", "e9e0e1e9-3812-4eb5-949e-ae92ac931401");

            string requestBody = JsonConvert.SerializeObject(_productModel);

            var request = new HttpRequestMessage(HttpMethod.Post, $"https://localhost:7186/api/products");

            request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");

            var response = await client.SendAsync(request).ConfigureAwait(false);

            try
            {
                context.Set(response.StatusCode, "ResponseStatusCode");
            }
            finally
            {
            }
        }

        [Then(@"add the product to the user´s pharmacy and return the  product model")]
        public void ThenAddTheProductToTheUserSPharmacyAndReturnTheProductModel()
        {
            Assert.Equal(200, (int)context.Get<HttpStatusCode>("ResponseStatusCode"));
        }
    }
}
