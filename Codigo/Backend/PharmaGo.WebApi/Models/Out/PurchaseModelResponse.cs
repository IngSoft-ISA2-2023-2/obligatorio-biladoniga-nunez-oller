using PharmaGo.Domain.Entities;
using static PharmaGo.WebApi.Models.In.PurchaseModelRequest;

namespace PharmaGo.WebApi.Models.Out
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class PurchaseModelResponse
    {
        public int Id { get; set; }
        public string BuyerEmail { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string TrackingCode { get; set; }
        public decimal TotalAmount { get; set; }
        public ICollection<PurchaseDetailModelResponse>? Details { get; set; }

        public class PurchaseDetailModelResponse
        {
            public int Id { get; set; }
            public string Code { get; set; }
            public int ProductCode { get; set; }
            public string Name { get; set; }
            public int Quantity { get; set; }
            public decimal Price { get; set; }
            public int PharmacyId { get; set; }
            public string PharmacyName { get; set; }
            public string Status { get; set; }
        }

        public PurchaseModelResponse(Purchase purchase)
        {
            Id = purchase.Id;
            BuyerEmail = purchase.BuyerEmail;
            TotalAmount = purchase.TotalAmount;
            PurchaseDate = purchase.PurchaseDate;
            TrackingCode = purchase.TrackingCode;
            Details = new List<PurchaseDetailModelResponse>();
            if (purchase.details != null) {
                foreach (var detail in purchase.details) {
                    Details.Add(new PurchaseDetailModelResponse {
                        Id = detail.Id,
                        Name = detail?.Drug?.Name ?? detail.Product.Name, 
                        Code = detail?.Drug?.Code ?? detail.Product.Code.ToString(),
                        Price = detail?.Drug?.Price ?? detail.Product.Price, 
                        Quantity = detail.Quantity,
                        PharmacyId = detail.Pharmacy.Id,
                        PharmacyName = detail.Pharmacy.Name,
                        Status = detail.Status
                });
                }
            }
        }
    }
}
