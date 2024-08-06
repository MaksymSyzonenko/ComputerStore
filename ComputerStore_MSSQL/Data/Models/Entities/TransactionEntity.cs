
namespace ComputerStore_MSSQL.Data.Entities
{
    public sealed class TransactionEntity
    {
        public Guid TransactionId { get; set; }
        public string UserId { get; set; } = default!;
        public Guid ProductId { get; set; }
        public DateTime TransactionDate { get; set; }
        public string ShippingAddress { get; set; } = default!;
        public string ShippingCity { get; set; } = default!;
        public string ShippingPostalCode { get; set; } = default!;
        public Guid OrderId { get; set; }
        public UserEntity User { get; set; } = default!;
        public ProductEntity Product { get; set; } = default!;
    }
}
