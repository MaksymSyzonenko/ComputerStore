using ComputerStore.Models.DTO;

namespace ComputerStore.Services.Review
{
    public interface IReviewService
    {
        Task<ReviewAddResultDto> AddReview(ReviewAddDto dto);
    }
}
