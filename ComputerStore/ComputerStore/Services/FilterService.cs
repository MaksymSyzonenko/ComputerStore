using ComputerStore.DataRepositories;
using ComputerStore.Models;

namespace ComputerStore.Services
{
    public static class FilterService
    {
        public static async Task<IEnumerable<Product>> ProductFilter(string searchQuery, decimal? minPrice, decimal? maxPrice, double? minRating, double? maxRating, string category, bool inStock, ProductRepository productRepository)
        {
            var filteredProducts = await productRepository.GetAllItemsAsync();

            if (!string.IsNullOrEmpty(searchQuery))
                filteredProducts = filteredProducts.Where(p => p!.Name.ToLower().Contains(searchQuery.ToLower()));

            if (minPrice.HasValue)
                filteredProducts = filteredProducts.Where(p => p!.Price >= minPrice.Value);

            if (maxPrice.HasValue)
                filteredProducts = filteredProducts.Where(p => p!.Price <= maxPrice.Value);

            if (minRating.HasValue)
                filteredProducts = filteredProducts.Where(p => p!.Reviews != null && p.Reviews.Any() && p.Reviews.Average(r => r.Rating) >= minRating.Value);

            if (maxRating.HasValue)
                filteredProducts = filteredProducts.Where(p => p!.Reviews != null && p.Reviews.Any() && p.Reviews.Average(r => r.Rating) <= maxRating.Value);

            if (!string.IsNullOrEmpty(category))
                filteredProducts = filteredProducts.Where(p => p!.Category.ToLower() == category.ToLower());

            if (inStock)
                filteredProducts = filteredProducts.Where(p => p!.InStock);

            return filteredProducts!;
        }
        public static IEnumerable<Product> ProductFilter(IEnumerable<Product> products, string searchQuery)
        {
            if (string.IsNullOrEmpty(searchQuery))
                return products;
            return products.Where(p =>
                    p.ProductID.ToString().Contains(searchQuery) ||
                    p.Name.Contains(searchQuery) ||
                    p.Producer.Contains(searchQuery) ||
                    p.Category.Contains(searchQuery) ||
                    p.InStock.ToString().Contains(searchQuery)
                ).ToList();
        }
        public static IEnumerable<Review> ReviewFilter(IEnumerable<Review> reviews, string searchQuery)
        {
            if (string.IsNullOrEmpty(searchQuery))
                return reviews;
            return reviews.Where(r =>
                    r.ReviewID.ToString().Contains(searchQuery) ||
                    r.UserID.ToString().Contains(searchQuery) ||
                    r.User.FirstName.Contains(searchQuery) ||
                    r.User.LastName.Contains(searchQuery) ||
                    r.ProductID.ToString().Contains(searchQuery) ||
                    r.Product.Name.Contains(searchQuery) ||
                    r.Rating.ToString().Contains(searchQuery) ||
                    r.Comment.Contains(searchQuery)
                ).ToList();
        }
        public static IEnumerable<Transaction> TransactionsFilter(IEnumerable<Transaction> transactions, string searchQuery)
        {
            if (string.IsNullOrEmpty(searchQuery))
                return transactions;
            return transactions.Where(t =>
                    t.TransactionID.ToString().Contains(searchQuery) ||
                    t.UserID.ToString().Contains(searchQuery) ||
                    t.User.FirstName.Contains(searchQuery) ||
                    t.User.LastName.Contains(searchQuery) ||
                    t.ProductID.ToString().Contains(searchQuery) ||
                    t.Product.Name.Contains(searchQuery) ||
                    t.TransactionID.ToString().Contains(searchQuery) ||
                    t.ShippingAddress.Contains(searchQuery) ||
                    t.ShippingCity.Contains(searchQuery) ||
                    t.ShippingPostalCode.Contains(searchQuery) ||
                    t.OrderID.ToString().Contains(searchQuery)
                ).ToList();
        }
        public static IEnumerable<User> UserFilter(IEnumerable<User> users, string searchQuery)
        {
            if (string.IsNullOrEmpty(searchQuery))
                return users;
            return users.Where(c =>
                    c.UserName.Contains(searchQuery) ||
                    c.Id.ToString().Contains(searchQuery) ||
                    c.FirstName.Contains(searchQuery) ||
                    c.LastName.Contains(searchQuery) ||
                    c.Email.Contains(searchQuery)
                ).ToList();
        }
    }
}
