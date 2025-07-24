using System.Net;

using KFC.UseCases.OutputPort;


namespace KFC.UseCases.Interactor;

public class HandleSuccess<T> : IHandleSuccess<T>
{
    public T Data { get; set; }
    public HttpStatusCode HttpStatusCode { get; set; }
    private HandleSuccess(T data, HttpStatusCode statusCode)
    {
        Data = data;
        HttpStatusCode = statusCode;
    }   
    
    public static HandleSuccess<T> Ok(T data)     {
        return new HandleSuccess<T>(data: data, statusCode: HttpStatusCode.OK);
    }
    public static HandleSuccess<T> Created(T data)
    {
        return new HandleSuccess<T>(data: data, statusCode: HttpStatusCode.Created);
    }

}


