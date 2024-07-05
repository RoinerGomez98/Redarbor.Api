using System.ComponentModel.DataAnnotations;

namespace Redarbor.Api.Application.Entities
{
    public class Redarbors
    {
        public int? Id { get; set; }
        [Required(ErrorMessage = "La propiedad CompanyId es requerido")]
        public int? CompanyId { get; set; }
        [Required(ErrorMessage = "La propiedad Email es requerido")]
        [StringLength(150, ErrorMessage = "El valor para la propiedad Email sobrepasa la longitud permitida")]
        public string? Email { get; set; }
        public string? Fax { get; set; }
        public string? Name { get; set; }
        public string? Username { get; set; }

        [Required(ErrorMessage = "La propiedad Password es requerido")]
        [StringLength(100, ErrorMessage = "El valor para la propiedad Password sobrepasa la longitud permitida")]
        public string? Password { get; set; }
        public string? Telephone { get; set; }
        public DateTime? Lastlogin { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? StatusId { get; set; }
        [Required(ErrorMessage = "La propiedad PortalId es requerido")]
        public int? PortalId { get; set; }
        [Required(ErrorMessage = "La propiedad RoleId es requerido")]
        public int? RoleId { get; set; }

        public Redarbors()
        {
            Id = 0;
        }
    }
}
