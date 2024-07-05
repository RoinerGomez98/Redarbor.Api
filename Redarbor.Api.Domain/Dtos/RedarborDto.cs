using Newtonsoft.Json;
using System.Globalization;

namespace Redarbor.Api.Domain.Dtos
{
    public class RedarborDto
    {
        public int? Id { get; set; }
        public int? CompanyId { get; set; }
        public string? Email { get; set; }
        public string? Fax { get; set; }
        public string? Name { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Telephone { get; set; }


        [JsonProperty("Lastlogin")]
        public string? Lastlogin { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public DateTime? lastlogin
        {
            get { return DateTime.Parse(Lastlogin!, new CultureInfo("en-US")); }
        }


        [JsonProperty("CreatedOn")]
        public string? CreatedOn { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public DateTime? createdOn
        {
            get { return DateTime.Parse(CreatedOn!, new CultureInfo("en-US")); }
        }

        [JsonProperty("DeletedOn")]
        public string? DeletedOn { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public DateTime? deletedOn
        {
            get { return DateTime.Parse(DeletedOn!, new CultureInfo("en-US")); }
        }

        [JsonProperty("UpdatedOn")]
        public string? UpdatedOn { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public DateTime? updatedOn
        {
            get { return DateTime.Parse(UpdatedOn!, new CultureInfo("en-US")); }
        }

        public int? StatusId { get; set; }
        public int? PortalId { get; set; }
        public int? RoleId { get; set; }
    }
}
