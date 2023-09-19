using System;
using PharmaGo.Domain.Entities;

namespace PharmaGo.WebApi.Models.Out
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class StockRequestSearchCriteriaDetailsModelResponse
    {
		public DrugModelResponse Drug { get; set; }
		public int Quantity { get; set; }

		public StockRequestSearchCriteriaDetailsModelResponse(StockRequestDetail stockRequestDetail)
		{
			this.Drug = new DrugModelResponse(stockRequestDetail.Drug);
			this.Quantity = stockRequestDetail.Quantity;
		}
	}
}

