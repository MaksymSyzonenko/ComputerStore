using ComputerStore_MSSQL.Data.Entities;
using ComputerStore_MSSQL.Data.Repositories.Product;

namespace ComputerStore.Services.Filter.FilterServices
{
    public static class FilterService
    {
        public static IEnumerable<ProductEntity> ProductFilter(IEnumerable<ProductEntity> products, string searchQuery, decimal? minPrice, decimal? maxPrice, double? minRating, double? maxRating, string category, bool inStock)
        {
            if (!string.IsNullOrEmpty(searchQuery))
                products = products
                    .Where(p => p!.Name
                    .ToLower()
                    .Contains(searchQuery.ToLower()));

            if (minPrice.HasValue)
                products = products.Where(p => p!.Price >= minPrice.Value);

            if (maxPrice.HasValue)
                products = products.Where(p => p!.Price <= maxPrice.Value);

            if (minRating.HasValue)
                products = products
                    .Where(p => p!.Reviews != null && p.Reviews
                    .Any() && p.Reviews
                    .Average(r => r.Rating) >= minRating.Value);

            if (maxRating.HasValue)
                products = products
                    .Where(p => p!.Reviews != null && p.Reviews
                    .Any() && p.Reviews
                    .Average(r => r.Rating) <= maxRating.Value);

            if (!string.IsNullOrEmpty(category))
                products = products
                    .Where(p => p!.Category
                    .ToLower() == category
                    .ToLower());

            if (inStock)
                products = products.Where(p => p!.InStock);

            return products!;
        }
        public static IEnumerable<ProductEntity> ProductFilter(IEnumerable<ProductEntity> products, string searchQuery)
        {
            if (string.IsNullOrEmpty(searchQuery))
                return products;
            return products.Where(p =>
                    p.ProductId.ToString().Contains(searchQuery) ||
                    p.Name.Contains(searchQuery) ||
                    p.Producer.Contains(searchQuery) ||
                    p.Category.Contains(searchQuery) ||
                    p.InStock.ToString().Contains(searchQuery)
                ).ToList();
        }
        public static IEnumerable<ReviewEntity> ReviewFilter(IEnumerable<ReviewEntity> reviews, string searchQuery)
        {
            if (string.IsNullOrEmpty(searchQuery))
                return reviews;
            return reviews.Where(r =>
                    r.ReviewId.ToString().Contains(searchQuery) ||
                    r.UserId.ToString().Contains(searchQuery) ||
                    r.User.FirstName.Contains(searchQuery) ||
                    r.User.LastName.Contains(searchQuery) ||
                    r.ProductId.ToString().Contains(searchQuery) ||
                    r.Product.Name.Contains(searchQuery) ||
                    r.Rating.ToString().Contains(searchQuery) ||
                    r.Comment.Contains(searchQuery)
                ).ToList();
        }
        public static IEnumerable<TransactionEntity> TransactionsFilter(IEnumerable<TransactionEntity> transactions, string searchQuery)
        {
            if (string.IsNullOrEmpty(searchQuery))
                return transactions;
            return transactions.Where(t =>
                    t.TransactionId.ToString().Contains(searchQuery) ||
                    t.UserId.ToString().Contains(searchQuery) ||
                    t.User.FirstName.Contains(searchQuery) ||
                    t.User.LastName.Contains(searchQuery) ||
                    t.ProductId.ToString().Contains(searchQuery) ||
                    t.Product.Name.Contains(searchQuery) ||
                    t.TransactionId.ToString().Contains(searchQuery) ||
                    t.ShippingAddress.Contains(searchQuery) ||
                    t.ShippingCity.Contains(searchQuery) ||
                    t.ShippingPostalCode.Contains(searchQuery) ||
                    t.OrderId.ToString().Contains(searchQuery)
                ).ToList();
        }
        public static IEnumerable<UserEntity> UserFilter(IEnumerable<UserEntity> users, string searchQuery)
        {
            if (string.IsNullOrEmpty(searchQuery))
                return users;
            return users.Where(c =>
                    c.UserName!.Contains(searchQuery) ||
                    c.Id.ToString().Contains(searchQuery) ||
                    c.FirstName.Contains(searchQuery) ||
                    c.LastName.Contains(searchQuery) ||
                    c.Email!.Contains(searchQuery)
                ).ToList();
        }
    }
}
