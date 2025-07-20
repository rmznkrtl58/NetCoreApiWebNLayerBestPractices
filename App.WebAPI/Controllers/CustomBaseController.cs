using App.Application.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //burdaki base controllerimın amacı apilerimi yazdığım controllerimda döneceğim değerleri ve ortak olan olayları yapılandırdığım yerdir
    public class CustomBaseController : ControllerBase
    {
        [NonAction]//yardımcı metod olarak yazdığım için action olarak görmesin
        public IActionResult CreateActionResult<T>(ServiceResult<T> result)
        {
            //apilerimde servicelerimi çağırırken serviceResult türünce responseDtolarla beraber apilerimde çağırıyorum benim service resultlarıma gelen status code eğerki nocontent dönerse içi boş bir durum kodu yani ozmn benim response bodym null olarak gelsin ve status codela birlikte gelsin
            if (result.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                return NoContent();
            }
            if (result.StatusCode == System.Net.HttpStatusCode.Created)
            {
                return Created(result.UrlAsCreated, result);
            }

            //olduda servicelerimden gelen durum codu nocontent değilse ozmn response bodye gelen değerim service ile gelen status code ve datayıda içerecektir.
            return new ObjectResult(result) { StatusCode = result.StatusCode.GetHashCode() };
        }
        [NonAction]
        //bu sefer datasız dönen değerler için geçerli
        public IActionResult CreateActionResult(ServiceResult result)
        {
            if (result.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                return NoContent();
            }
            return new ObjectResult(result) { StatusCode = result.StatusCode.GetHashCode() };
        }
    }
}
