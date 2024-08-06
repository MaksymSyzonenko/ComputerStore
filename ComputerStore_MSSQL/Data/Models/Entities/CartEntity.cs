
namespace ComputerStore_MSSQL.Data.Entities
{
    public sealed class CartEntity
    {
        public Guid CartId { get; set; }
        public string UserId { get; set; } = default!;
        public Guid ProductId { get; set; }
        public UserEntity User { get; set; } = default!;
        public ProductEntity Product { get; set; } = default!;
    }
}
