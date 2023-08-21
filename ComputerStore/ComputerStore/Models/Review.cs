using System.ComponentModel.DataAnnotations;

namespace ComputerStore.Models
{
    public class Review
    {
        public Guid ReviewID { get; set; }
        [Required]
        public string UserID { get; set; }
        [Required]
        public Guid ProductID { get; set; }
        [Required]
        [Range(1, 5)]
        public double Rating { get; set; }
        public string Comment { get; set; }
        [Required]
        public DateTime ReviewDate { get; set; }
        public User User { get; set; }
        public Product Product { get; set; }
    }
}
