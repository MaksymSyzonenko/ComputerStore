using Microsoft.AspNetCore.Identity;

namespace ComputerStore_MSSQL.Data.Entities
{
    public sealed class UserEntity : IdentityUser
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public ICollection<TransactionEntity> Transactions { get; set; } = default!;
        public ICollection<ReviewEntity> Reviews { get; set; } = default!;
        public ICollection<CartEntity> CartItems { get; set; } = default!;
    }
}
