using BitrixService.Contracts.Models;
using CsvHelper.Configuration;

namespace BitrixService.ApiClients.Stripmag.Mappings
{
    public sealed class OfferAtoMap : ClassMap<OfferAto>
    {
        public OfferAtoMap()
        {
            Map(m => m.Id).Optional();
            Map(m => m.ProductID).Name("prodid");
            Map(m => m.XmlId).Name("sku");
            Map(m => m.Barcode).Name("barcode");
            Map(m => m.Name).Name("name");
            Map(m => m.Quantity).Name("qty");
            Map(m => m.ShippingDate).Name("shippingdate");
            Map(m=>m.Weight).Name("weight");
            Map(m => m.Color).Name("color");
            Map(m => m.Size).Name("size");
            Map(m => m.Currency).Name("currency");
            Map(m => m.Price).Name("price");
            Map(m => m.BaseWholePrice).Name("basewholeprice");
            Map(m => m.P5sStock).Name("p5s_stock");
            Map(m => m.SuperSale).Name("SuperSale");
            Map(m => m.StopPromo).Name("StopPromo");
        }
    }
}