using Azure;
using ComputerStore.Models;
using FluentAssertions.Primitives;
using Microsoft.EntityFrameworkCore;

namespace ComputerStore.Interfaces
{
    public interface IRepository<TypeEntity, TypeID>
    {
        Task AddItemAsync(TypeEntity item);
        Task RemoveItemAsync(TypeEntity item);
        Task UpdateItemAsync(TypeID itemID, TypeEntity updatedItem);
        Task<TypeEntity?> GetItemAsync(TypeID itemID);
        Task<IEnumerable<TypeEntity?>> GetAllItemsAsync();
    }
}
