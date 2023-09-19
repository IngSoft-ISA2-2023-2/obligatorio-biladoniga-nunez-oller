using System;
using PharmaGo.Domain.Entities;

namespace PharmaGo.WebApi.Models.In
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class StockRequestModelRequest
	{
        public List<StockRequestDetailModelRequest> Details { get; set; }

        public StockRequest ToEntity()
        {
            var stockRequest = new StockRequest();
            foreach (StockRequestDetailModelRequest item in this.Details)
            {
                stockRequest.Details.Add(new StockRequestDetail() { Drug = new Drug() { Code = item.Drug.Code }, Quantity = item.Quantity });
            }
            return stockRequest;
        }
    }
}

