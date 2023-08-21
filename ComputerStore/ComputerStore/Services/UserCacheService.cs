using ComputerStore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;

namespace ComputerStore.Services
{
    public class UserCacheService
    {
        private readonly IMemoryCache _cache;
        private readonly UserManager<User> _userManager;

        public UserCacheService(IMemoryCache cache, UserManager<User> userManager)
        {
            _cache = cache;
            _userManager = userManager;
        }

        public string GetUserId(string username)
        {
            if (_cache.TryGetValue(username, out string? userId))
            {
                return userId!;
            }
            var user = _userManager.FindByNameAsync(username).Result;
            if (user != null)
            {
                userId = user.Id;
                _cache.Set(username, userId, TimeSpan.FromMinutes(15));
            }
            return userId!;
        }
    }
}
