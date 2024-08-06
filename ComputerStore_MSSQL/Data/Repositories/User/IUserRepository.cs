using ComputerStore_MSSQL.Data.Entities;

namespace ComputerStore_MSSQL.Data.Repositories.User
{
    public interface IUserRepository : IRepository
    {
        Task AddUserAsync(UserEntity user);
        Task UpdateUserAsync(string userId, UserEntity updatedUser);
        Task<UserEntity?> GetUserAsync(string userId);
        Task<IEnumerable<UserEntity?>> GetAllUsersAsync();
    }
}
