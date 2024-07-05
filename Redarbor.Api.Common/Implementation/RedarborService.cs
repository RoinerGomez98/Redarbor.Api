using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using Redarbor.Api.Application.Entities;
using Redarbor.Api.Application.Repo;
using Redarbor.Api.Common.Interfaces;
using Redarbor.Api.Domain.Dtos;
using Redarbor.Api.Domain.Utils;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Redarbor.Api.Common.Implementation
{
    public class RedarborService : IRedarborService
    {
        private readonly Persistence _persistence;
        private readonly IMapper _mapper;
        public RedarborService(Persistence persistence, IMapper mapper)
        {
            _persistence = persistence;
            _mapper = mapper;
        }

        public async Task<GenericResponse<IEnumerable<RedarborDto>>> GetRedabors()
        {
            var result = await _persistence.GetRedabors();
            GenericResponse<IEnumerable<RedarborDto>> response = new()
            {
                Result = _mapper.Map<IEnumerable<RedarborDto>>(result),
                Status = HttpStatusCode.OK
            };
            return response;

        }

        public async Task<GenericResponse<RedarborDto>> GetRedaborById(int Id)
        {
            var result = await _persistence.GetRedaborById(Id);
            GenericResponse<RedarborDto> response = new()
            {
                Result = _mapper.Map<RedarborDto>(result),
                Status = HttpStatusCode.OK
            };
            return response;

        }

        public async Task<GenericResponse<RedarborResponseDto>> GetUser(string Email, string Password)
        {
            var result = await _persistence.GetUser(Email, Password);
            GenericResponse<RedarborResponseDto> response = new()
            {
                Result = _mapper.Map<RedarborResponseDto>(result),
                Status = HttpStatusCode.OK
            };
            return response;
        }

        public async Task<GenericResponse<int>> SaveOrUpdateRedarbors(RedarborDto redarborDto)
        {
            var cast = _mapper.Map<Redarbors>(redarborDto);
            if (!Security.Validate(cast, out ICollection<ValidationResult> errors))
            {
                return new GenericResponse<int>()
                {
                    Result = 0,
                    Status = HttpStatusCode.BadRequest,
                    Message = String.Join(", ", errors.Select(m => m.ErrorMessage))
                };
            }
            if (cast.Id != null)
            {
                var exist = await _persistence.GetRedaborById(cast.Id!.Value);
                if (exist != null)
                {
                    exist.Name = string.IsNullOrEmpty(cast.Name) ? exist.Name : cast.Name;
                    exist.Email = string.IsNullOrEmpty(cast.Email) ? exist.Email : cast.Email;
                    exist.Username = string.IsNullOrEmpty(cast.Username) ? exist.Username : cast.Username;
                    exist.Password = string.IsNullOrEmpty(cast.Password) ? exist.Password : cast.Password;
                    exist.Fax = string.IsNullOrEmpty(cast.Fax) ? exist.Fax : cast.Fax;
                    exist.Telephone = string.IsNullOrEmpty(cast.Telephone) ? exist.Telephone : cast.Telephone;
                    exist.StatusId = cast.StatusId == null ? exist.StatusId : cast.StatusId;
                }
                else
                {
                    return new()
                    {
                        Result = 0,
                        Message = "No se encontro Id digitado.",
                        Status = HttpStatusCode.BadRequest
                    };
                }

            }


            int result = await _persistence.SaveOrUpdateRedarbors(cast);
            return new()
            {
                Result = result,
                Message = result > 0 ? "Se guardó correctamente información" : "No se pudo guardar información",
                Status = result > 0 ? HttpStatusCode.OK : HttpStatusCode.BadRequest
            };
        }

        public async Task<GenericResponse<int>> DeleteRedarbor(int Id)
        {
            int result = await _persistence.DeleteRedarbor(Id);
            return new()
            {
                Result = result,
                Message = result > 0 ? "Se eliminó correctamente información" : "No se pudo eliminar información",
                Status = result > 0 ? HttpStatusCode.OK : HttpStatusCode.BadRequest
            };
        }
    }
}
