using App.Application.Features.ServiceInterfaces;


namespace App.Application.Features.Services
{
    public class GenericService<T>:IGenericService<T>where T : class
    {
    }
}
