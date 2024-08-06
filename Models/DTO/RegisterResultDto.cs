namespace ComputerStore.Models.DTO
{
    public sealed class RegisterResultDto
    {
        public bool Succeeded { get; init; }
        public string Message { get; init; }
        public RegisterResultDto(bool succeeded, string message)
        {
            Succeeded = succeeded;
            Message = message;
        }
    }
}
