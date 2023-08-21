using System.ComponentModel.DataAnnotations;

namespace ComputerStore.Models
{
    public class Transaction
    {
        public Guid TransactionID { get; set; }
        [Required]
        public string UserID { get; set; }
        [Required]
        public Guid ProductID { get; set; }
        [Required]
        public DateTime TransactionDate { get; set; }
        [Required]
        public string ShippingAddress { get; set; }
        [Required]
        public string ShippingCity { get; set; }
        [Required]
        public string ShippingPostalCode { get; set; }
        [Required]
        public Guid OrderID { get; set; }
        public User User { get; set; }
        public Product Product { get; set; }
    }
}
