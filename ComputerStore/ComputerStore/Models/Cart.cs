using System.ComponentModel.DataAnnotations;

namespace ComputerStore.Models
{
    public class Cart
    {
        public Guid CartID { get; set; }
        [Required]
        public string UserID { get; set; }
        [Required]
        public Guid ProductID { get; set; }
        public User User { get; set; }
        public Product Product { get; set; }
    }
}
