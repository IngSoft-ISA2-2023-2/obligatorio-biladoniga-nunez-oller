using Newtonsoft.Json;
using PharmaGo.Domain.Entities;
using System;
using System.Net;
using System.Text;
using TechTalk.SpecFlow;

namespace SpecFlowPharmaGo.WebApi.StepDefinitions
{
    [Binding]
    public class DeleteProductStepDefinitions
    {
        private readonly ScenarioContext context;

        public DeleteProductStepDefinitions(ScenarioContext _context)
        {
            context = _context;
        }

        [Given(@"The id 1 of a product")]
        public void GivenTheIdOfAProduct()
        {
            context.Add("ID", "1");
        }

        [When(@"try to delete the product")]
        public async Task WhenTryToDeleteTheProduct()
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            var client = new HttpClient(clientHandler);
            client.DefaultRequestHeaders.Add("Authorization", "e9e0e1e9-3812-4eb5-949e-ae92ac931401");

            var request = new HttpRequestMessage(HttpMethod.Delete, $"https://localhost:7186/api/products/{context.Get<string>("ID")}");

            var response = await client.SendAsync(request).ConfigureAwait(false);

            try
            {
                context.Set(response.StatusCode, "ResponseStatusCode");
            }
            finally
            {
            }
        }

        [Then(@"the product has been successfully  deleted")]
        public void ThenTheProductHasBeenSuccessfullyDeleted()
        {
            Assert.Equal(200, (int)context.Get<HttpStatusCode>("ResponseStatusCode"));
        }
    }
}
