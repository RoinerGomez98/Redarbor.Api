using System.ComponentModel.DataAnnotations;

namespace Redarbor.Api.Application.Entities
{
    public class Redarbors
    {
        public int? Id { get; set; }
        public int? CompanyId { get; set; }
        public string? Email { get; set; }
        public string? Fax { get; set; }
        public string? Name { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Telephone { get; set; }
        public DateTime? Lastlogin { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? StatusId { get; set; }
        public int? PortalId { get; set; }
        public int? RoleId { get; set; }

        public Redarbors()
        {
            Id = 0;
        }
    }
}
