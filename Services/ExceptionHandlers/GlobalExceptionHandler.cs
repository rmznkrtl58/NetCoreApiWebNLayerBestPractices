using App.Services.Results;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.ExceptionHandlers
{   //uygulamamda hata fırlatıldığı zaman yakalicak olan ve geriye uygun modelimizi dönecek olan yapımız
    public class GlobalExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var errorResponse = ServiceResult.Fail(exception.Message, System.Net.HttpStatusCode.InternalServerError);
            httpContext.Response.StatusCode = HttpStatusCode.InternalServerError.GetHashCode();
            httpContext.Response.ContentType= "application/json";
            await httpContext.Response.WriteAsJsonAsync(errorResponse, cancellationToken: cancellationToken);
            return true;//artık ben bunu kendim ele aldım responsenı felan belirledim bundan sonra kimse bunu ele almasın artık buradan geriye bizim ResponseDTOm ile beraber hata mesajı dönecek

        }
    }
}
