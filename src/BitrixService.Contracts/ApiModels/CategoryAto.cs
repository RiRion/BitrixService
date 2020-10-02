namespace BitrixService.Contracts.ApiModels
{
    public class CategoryAto
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string Name { get; set; }
    }
}