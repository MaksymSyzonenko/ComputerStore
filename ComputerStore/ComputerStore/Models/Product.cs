using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComputerStore.Models
{
    public class Product
    {
        public Guid ProductID { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [StringLength(20)]
        public string Producer { get; set; }
        [Required]
        [StringLength(50)]
        public string Category { get; set; }
        public string Image { get; set; }
        [Required]
        public string ShortDescription { get; set; }
        [Required]
        public string FullDescription { get; set; }
        [Required]
        public string Specifications { get; set; }
        [Required]
        public bool InStock { get; set; }
        [Required]
        public decimal Price { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<Cart> CartItems { get; set; }
    }

}
