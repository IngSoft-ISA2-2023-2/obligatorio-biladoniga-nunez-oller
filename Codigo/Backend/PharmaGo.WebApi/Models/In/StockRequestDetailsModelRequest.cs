using System;
using PharmaGo.Domain.Entities;

namespace PharmaGo.WebApi.Models.In
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class StockRequestDetailModelRequest
    {
        public DrugModelRequest Drug { get; set; }
        public int Quantity { get; set; }
	}
}

