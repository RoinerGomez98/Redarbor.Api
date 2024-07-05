using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Redarbor.Api.Common.Interfaces;
using Redarbor.Api.Domain.Dtos;
using Redarbor.Api.Domain.Utils;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace Redarbor.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RedarborController : ControllerBase
    {
        private readonly IRedarborService _redarborService;
        private readonly Security securityC;
        private readonly IConfiguration _config;

        public RedarborController(IRedarborService redarborService, IConfiguration config)
        {
            _config = config;
            _redarborService = redarborService;
            securityC = new(_config);
        }
        [HttpGet("{email},{password}"), AllowAnonymous]
        public async Task<GenericResponse<RedarborResponseDto>> Auth(string email, string password)
        {
            if (string.IsNullOrEmpty(email))
            {
                HttpContext.Response.StatusCode = 400;
                return new GenericResponse<RedarborResponseDto> { Message = "Digite Email.", Status = HttpStatusCode.BadRequest };
            }
            if (string.IsNullOrEmpty(password))
            {
                HttpContext.Response.StatusCode = 400;
                return new GenericResponse<RedarborResponseDto> { Message = "Digite Contraseña.", Status = HttpStatusCode.BadRequest };
            }

            var res = await _redarborService.GetUser(email, securityC.EncryptP(password!));
            RedarborResponseDto response = res.Result!;
            GenericResponse<RedarborResponseDto> tk;
            if (response != null)
            {
                tk = GenerateToken(new UserClaims { Email = response.Email, Name = response!.Name, Id = response.Id.ToString() }, response);
            }
            else
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return new GenericResponse<RedarborResponseDto> { Message = "Usuario no encontrado", Status = HttpStatusCode.BadRequest };
            }

            HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;
            return new GenericResponse<RedarborResponseDto> { Result = tk.Result, Status = HttpStatusCode.OK };
        }


        [HttpGet, AllowAnonymous]
        public async Task<GenericResponse<IEnumerable<RedarborDto>>> GetRedabors()
        {
            return await _redarborService.GetRedabors();
        }

        [HttpGet("{id}")]
        public async Task<GenericResponse<RedarborDto>> GetRedabors(int id)
        {
            return await _redarborService.GetRedaborById(id);
        }

        [HttpPost]
        public async Task<GenericResponse<int>> SaveOrUpdateRedarbors(RedarborDto redarborDto)
        {
            if (!string.IsNullOrEmpty(redarborDto.Password) && redarborDto.Id == null)
            {
                redarborDto.Password = securityC.EncryptP(redarborDto.Password!);
            }
            var result = await _redarborService.SaveOrUpdateRedarbors(redarborDto);
            HttpContext.Response.StatusCode = (int)result.Status;
            return result;
        }


        [HttpDelete("{id}")]
        public async Task<GenericResponse<int>> DeleteRedarbor(int id)
        {
            return await _redarborService.DeleteRedarbor(id);
        }
        private GenericResponse<RedarborResponseDto> GenerateToken(UserClaims userClaims, RedarborResponseDto userResponse)
        {
            if (userClaims == null)
                return new GenericResponse<RedarborResponseDto>();

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Authentication, userClaims.Id!),
                new Claim(ClaimTypes.NameIdentifier, userClaims.Name!),
                new Claim(ClaimTypes.Email, userClaims.Email!),
            };

            DateTime expira = DateTime.Now.AddHours(1);
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Audience"],
              claims,
              expires: expira,
              signingCredentials: credentials);

            userResponse.Token = new JwtSecurityTokenHandler().WriteToken(token);
            userResponse.Expires = expira;

            return new GenericResponse<RedarborResponseDto> { Result = userResponse };
        }
        private class UserClaims
        {
            public string? Name { get; set; }
            public string? Email { get; set; }
            public string? Id { get; set; }
        }
    }
}
