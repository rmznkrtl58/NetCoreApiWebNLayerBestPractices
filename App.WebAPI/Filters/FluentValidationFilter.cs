using App.Application.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
namespace App.WebAPI.Filters
{                                      //action metoda girmeden önce uygulanacak filtrem
    public class FluentValidationFilter : IAsyncActionFilter
    {   
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {   //benim validasyon işlemlerim başarısız ise
            if (!context.ModelState.IsValid)
            {
                //selectMany=>liste seçimi
                var errors = context.ModelState.Values.SelectMany(x => x.Errors).Select(y => y.ErrorMessage).ToList();
                //döneceğim serviceResultıma hataları ekle
                var resultModel = ServiceResult.Fail(errors);
                context.Result=new BadRequestObjectResult(resultModel);
                return;
            }
            await next();
        }
    }
}
