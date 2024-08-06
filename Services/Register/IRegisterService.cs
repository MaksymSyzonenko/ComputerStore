using ComputerStore.Models.DTO;

namespace ComputerStore.Services.Register
{
    public interface IRegisterService<in Dto, ResultDto>
    {
        Task<ResultDto> Register(Dto dto);
    }
}
