using System;
using Newtonsoft.Json;
using PharmaGo.Domain.Entities;
using TechTalk.SpecFlow;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace SpecFlowPharmaGo.WebApi.StepDefinitions
{
    [Binding]
    public class UpdateProductsStepDefinitions
    {
        private readonly ScenarioContext context;
        private readonly Product _product = new Product();

        public UpdateProductsStepDefinitions(ScenarioContext context)
        {
            this.context = context;
        }

        [Given(@"The id ""([^""]*)"" of the product")]
        public void GivenTheIdOfTheProduct(string id)
        {
            context.Add("ID", id);
        }

        [Given(@"The name ""([^""]*)"" of the product")]
        public void GivenTheNameOfTheProduct(string name)
        {
            _product.Name = name;
        }

        [When(@"try to update a product name")]
        public async Task WhenTryToUpdateAProductName()
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            var client = new HttpClient(clientHandler);

            string requestBody = JsonConvert.SerializeObject(new { Name = _product.Name });

            var request = new HttpRequestMessage(HttpMethod.Put, $"http://localhost:5186/api/products/{context.Get<string>("ID")}")
            {
                Content = new StringContent(requestBody)
                {
                    Headers =
                        {
                          ContentType = new MediaTypeHeaderValue("application/json")
                        }
                }
            };
            var response = await client.SendAsync(request).ConfigureAwait(false);

            try
            {
                context.Set(response.StatusCode, "ResponseStatusCode");
            }
            finally
            {
            }
        }

        [Then(@"return the updated product")]
        public void ThenReturnTheUpdatedProduct()
        {
            Assert.Equal(200, (int)context.Get<HttpStatusCode>("ResponseStatusCode"));
        }
    }
}
