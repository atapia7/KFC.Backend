using System.Net;

namespace KFC.UseCases.DTOs.Interface;

public interface IHttpStatus
{
    public HttpStatusCode HttpStatusCode { get; set; }
}