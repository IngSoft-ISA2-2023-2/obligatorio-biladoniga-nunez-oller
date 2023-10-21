using System.Net;

namespace SpecFlowPharmaGo.WebApi.StepDefinitions
{
    [Binding]
    public sealed class GetProductsStepDefinitions
    {
        private readonly ScenarioContext context;

        public GetProductsStepDefinitions(ScenarioContext context)
        {
            this.context = context;
        }

        [When("I perform a simple products request")]
        public async Task WhenIPerformASimpleProductsRequest()
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            var client = new HttpClient(clientHandler);

            var request = new HttpRequestMessage(HttpMethod.Get, $"http://localhost:5186/api/products");
            var response = await client.SendAsync(request).ConfigureAwait(false);

            try
            {
                context.Set(response.StatusCode, "ResponseStatusCode");
            }
            finally
            {
            }
        }

        [Then("return a list of products")]
        public void ThenReturnAListOfProducts()
        {
            Assert.Equal(200, (int)context.Get<HttpStatusCode>("ResponseStatusCode"));
        }
    }
}
