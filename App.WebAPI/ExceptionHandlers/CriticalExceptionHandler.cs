using App.Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;


namespace App.WebAPI.ExceptionHandlers
{   //Kendi Fırlatacağım hataları içerir
    public class CriticalExceptionHandler(ILogger<CriticalExceptionHandler>_logger) : IExceptionHandler
    {
        public ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            //hatam criticalExceptin ise 
            if(exception is CriticalException)
            {
                Console.WriteLine("Hata Sms İle İletilmiştir.");
            }
            return ValueTask.FromResult(false);
            //hata false ise bir sonraki handlera yönlendirir.(GlobalExceptionHandler)->yoksa Exception handler middleware gider 
        }
    }
}
