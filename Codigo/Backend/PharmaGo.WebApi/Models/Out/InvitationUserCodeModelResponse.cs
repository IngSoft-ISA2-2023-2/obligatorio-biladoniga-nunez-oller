using System;
namespace PharmaGo.WebApi.Models.Out
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class InvitationUserCodeModelResponse
	{
		public string UserCode { get; set; }

		public InvitationUserCodeModelResponse(string userCode)
		{
			this.UserCode = userCode;
		}
	}
}

