
namespace ComputerStore_MSSQL.Data.Entities
{
    public sealed class ProductEntity
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; } = default!;
        public string Producer { get; set; } = default!;
        public string Category { get; set; } = default!;
        public string Image { get; set; } = default!;
        public string ShortDescription { get; set; } = default!;
        public string FullDescription { get; set; } = default!;
        public string Specifications { get; set; } = default!;
        public bool InStock { get; set; }
        public decimal Price { get; set; }
        public ICollection<TransactionEntity> Transactions { get; set; } = default!;
        public ICollection<ReviewEntity> Reviews { get; set; } = default!;
        public ICollection<CartEntity> CartItems { get; set; } = default!;
    }

}
