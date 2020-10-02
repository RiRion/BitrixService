using BitrixService.Contracts.ApiModels;
using CsvHelper.Configuration;

namespace BitrixService.ApiClients.Stripmag.Mappings
{
    public sealed class CategoriesMap : ClassMap<CategoriesAto>
    {
        public CategoriesMap()
        {
            Map(m => m.Category1.Name).Name("categories_1");
            Map(m => m.Category2.Name).Name("categories_2");
            Map(m => m.Category3.Name).Name("categories_3");
        }
    }
}