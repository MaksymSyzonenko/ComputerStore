using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace ComputerStore.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        //public string Role { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<Cart> CartItems { get; set; }
    }
}
