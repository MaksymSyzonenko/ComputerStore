namespace ComputerStore.ViewModels
{
    public class ProductViewModel
    {
        public Guid ProductID { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string ShortDescription { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public bool InStock { get; set; }
        public double AverageRating { get; set; }
    }
}
