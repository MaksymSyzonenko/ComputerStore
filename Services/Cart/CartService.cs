using ComputerStore.ViewModels;
using ComputerStore_MSSQL.Data.Entities;
using ComputerStore_MSSQL.Data.Repositories.Cart;
using ComputerStore_MSSQL.Data.Repositories.Product;
using ComputerStore_MSSQL.Data.Repositories.Transaction;
using ComputerStore_MSSQL.Data.UnitOfWork;

namespace ComputerStore.Services.Cart
{
    public sealed class CartService : ICartService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CartService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task CreateOrder(string userId, OrderViewModel model)
        {
            var orderId = Guid.NewGuid();
            var orderProducts = (await ((ICartRepository)_unitOfWork.Repository<CartEntity>()).GetCartForUserAsync(userId))!;
            foreach (var p in orderProducts)
            {
                await((ITransactionRepository)_unitOfWork.Repository<TransactionEntity>()).AddTransactionAsync(new TransactionEntity
                {
                    UserId = userId.ToString(),
                    ProductId = p!.ProductId,
                    ShippingAddress = model.ShippingAddress,
                    ShippingCity = model.ShippingCity,
                    ShippingPostalCode = model.ShippingPostalCode,
                    TransactionDate = DateTime.Now,
                    OrderId = orderId
                });
                p.InStock = false;
                await((IProductRepository)_unitOfWork.Repository<ProductEntity>()).UpdateProductAsync(p.ProductId, p);
            }
            await((ICartRepository)_unitOfWork.Repository<CartEntity>()).ClearCartAsync(userId);
        }
    }
}
