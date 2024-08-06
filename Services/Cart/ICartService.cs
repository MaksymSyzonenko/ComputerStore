using ComputerStore.ViewModels;
using ComputerStore_MSSQL.Data.Entities;

namespace ComputerStore.Services.Cart
{
    public interface ICartService
    {
        Task CreateOrder(string userId, OrderViewModel model);
    }
}
