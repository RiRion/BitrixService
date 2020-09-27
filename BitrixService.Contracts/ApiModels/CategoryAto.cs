namespace BitrixService.Contracts.Models
{
    public class CategoryAto
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string Name { get; set; }
    }
}