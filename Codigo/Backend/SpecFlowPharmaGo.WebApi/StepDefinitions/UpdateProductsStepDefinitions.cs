using Newtonsoft.Json;
using PharmaGo.Domain.Entities;
using System.Net;
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

        [Given(@"The ""([^""]*)"" with ""([^""]*)"" of the product")]
        public void GivenTheWithValueOfTheProduct(string name, dynamic val)
        {
            decimal dec = 0;
            if (Decimal.TryParse(val, out dec))
            {
                typeof(Product).GetProperty(name).SetValue(_product, dec);
            } else
            {
                typeof(Product).GetProperty(name).SetValue(_product, val);
            }
        }

        [When(@"try to update the product")]
        public async Task WhenTryToUpdateAProductName()
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            var client = new HttpClient(clientHandler);
            client.DefaultRequestHeaders.Add("Authorization", "e9e0e1e9-3812-4eb5-949e-ae92ac931401");

            string requestBody = JsonConvert.SerializeObject(new { Name = _product.Name, Description = _product.Description, Price = _product.Price });

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
