namespace PharmaGo.WebApi.Models.Out
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class LoginModelResponse
    {
        public Guid token { get; set; }
        public string role { get; set; }
        public string userName { get; set; }
    }
}
