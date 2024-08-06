using Microsoft.AspNetCore.Mvc;

namespace ComputerStore.Models.DTO
{
    public sealed class ReviewAddDto
    {
        public Guid ProductId { get; set; }
        public string ReviewComment { get; set; } = default!;
        public string ReviewRating { get; set; } = default!;
        public string UserName { get; set; } = default!;
    }
}
