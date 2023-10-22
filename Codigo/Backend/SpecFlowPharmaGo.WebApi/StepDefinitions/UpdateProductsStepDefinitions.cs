using System;
using Newtonsoft.Json;
using PharmaGo.Domain.Entities;
using TechTalk.SpecFlow;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using System.Net;
using Newtonsoft.Json.Linq;
using System.Text;

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

        [Given(@"The id (.*) of the product")]
        public void GivenTheIdOfTheProduct(string id)
        {
            context.Add("ID", id);
        }

        [Given(@"The name (.*) of the product")]
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
            client.DefaultRequestHeaders.Add("Authorization", "e9e0e1e9-3812-4eb5-949e-ae92ac931401");

            string requestBody = JsonConvert.SerializeObject(new { Name = _product.Name });

            var request = new HttpRequestMessage(HttpMethod.Put, $"https://localhost:7186/api/products/{context.Get<string>("ID")}");

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

        [Then(@"return the updated product")]
        public void ThenReturnTheUpdatedProduct()
        {
            Assert.Equal(200, (int)context.Get<HttpStatusCode>("ResponseStatusCode"));
        }
    }
}
