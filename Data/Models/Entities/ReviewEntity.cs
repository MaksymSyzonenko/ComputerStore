using System.ComponentModel.DataAnnotations;

namespace ComputerStore_MSSQL.Data.Entities
{
    public sealed class ReviewEntity
    {
        public Guid ReviewId { get; set; }
        public string UserId { get; set; } = default!;
        public Guid ProductId { get; set; }
        [Range(1, 5)]
        public double Rating { get; set; }
        public string Comment { get; set; } = default!;
        public DateTime ReviewDate { get; set; }
        public UserEntity User { get; set; } = default!;
        public ProductEntity Product { get; set; } = default!;
    }
}
