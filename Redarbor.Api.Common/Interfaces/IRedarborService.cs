using Redarbor.Api.Domain.Dtos;
using Redarbor.Api.Domain.Utils;

namespace Redarbor.Api.Common.Interfaces
{
    public interface IRedarborService
    {
        Task<GenericResponse<IEnumerable<RedarborDto>>> GetRedabors();
        Task<GenericResponse<RedarborDto>> GetRedaborById(int Id);
        Task<GenericResponse<RedarborResponseDto>> GetUser(string Email, string Password);
        Task<GenericResponse<int>> SaveOrUpdateRedarbors(RedarborDto redarborDto);
        Task<GenericResponse<int>> DeleteRedarbor(int Id);
    }
}
