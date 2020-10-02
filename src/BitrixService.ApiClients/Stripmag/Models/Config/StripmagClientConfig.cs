namespace BitrixService.ApiClients.Stripmag.Models.Config
{
    public class StripmagClientConfig
    {
        public string BaseUri { get; }
        public string ProductPath { get; }
        public string OfferPath { get; }

        public StripmagClientConfig(
            string baseUri,
            string productPath,
            string offerPath
            )
        {
            BaseUri = baseUri;
            ProductPath = productPath;
            OfferPath = offerPath;
        }
    }
}