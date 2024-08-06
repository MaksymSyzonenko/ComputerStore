
namespace ComputerStore.ViewModels
{
    public class ProductViewModel
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; } = default!;
        public string Image { get; set; } = default!;
        public string ShortDescription { get; set; } = default!;
        public string Category { get; set; } = default!;
        public decimal Price { get; set; }
        public bool InStock { get; set; }
        public double AverageRating { get; set; }
    }
}
