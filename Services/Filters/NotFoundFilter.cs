using App.Repositories.RepositoryInterfaces;
using App.Services.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

//Parametre ve ctor içerdiği için DI containerda kullanmak gerek
namespace App.Services.Filters
{                                //attribute gibi kullanmak için attributemdende miras alıyorum
    public class NotFoundFilter<T,TId>(IGenericRepository<T,TId> _genericRepository) : Attribute, IAsyncActionFilter where T : class where TId:struct

    {   //Action Filter endpoint çalışmadan veya çalıştıktan sonra devreye giren mekanizmadır.
      
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {   
            //Action Metod çalışmadan önce


            //idValue=>benim metodumdaki ilk parametreyi alacak değerim
            var idValue = context.ActionArguments.Values.FirstOrDefault();
            if (idValue is null)
            {
                //ilgili endpointe gir çalıştır sonra alta girme
                await next();
                return;
            }

            //yani benim actionumdan gelen id değerim TId türünde değil ise yani(int) değil ise
            if(idValue is not TId id)
            {
                //ilgili endpointe gir çalıştır sonra alta girme
                await next();
                return;
            }
            bool hasEntity = await _genericRepository.AnyAsync(id);
            if (!hasEntity)
            {
                var entityName=typeof(T).Name;
                //silmemi güncellememi veya id'ye göre getirme işlemimi ona göre bakalım.
                var actionName = context.ActionDescriptor.RouteValues["action"];
                var result = ServiceResult.Fail($"Veri Bulunamadı! ({entityName})/({actionName})", System.Net.HttpStatusCode.NotFound);
                context.Result=new NotFoundObjectResult(result);
                return;
            }
            await next();

            //Action Metod çalıştıktan sonra
        }
    }
}
