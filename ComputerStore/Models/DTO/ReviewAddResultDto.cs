namespace ComputerStore.Models.DTO
{
    public sealed class ReviewAddResultDto
    {
        public bool Succeeded { get; init; }
        public string Message { get; init; }
        public Guid ProductId { get; init; }
        public ReviewAddResultDto(bool succeeded, string message, Guid productId)
        {
            Succeeded = succeeded;
            Message = message;
            ProductId = productId;
        }
    }
}
