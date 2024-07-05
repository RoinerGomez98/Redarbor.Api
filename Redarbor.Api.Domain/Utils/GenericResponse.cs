using System.Net;

namespace Redarbor.Api.Domain.Utils
{
    public class GenericResponse<T>
    {
        public string? Message { get; set; }
        public HttpStatusCode Status { get; set; }
        public T? Result { get; set; } = default;
    }
}
