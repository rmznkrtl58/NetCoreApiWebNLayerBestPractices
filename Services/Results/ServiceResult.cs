

using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json.Serialization;

namespace App.Services.Results
{   //benim serviceten döneceğim hataları ve durum kodlarını tutacak sınıfım.

    //Ekleme-Listelemede Kullanırken başarılıysa ben data bekliyordum etkilenen datayıda geriye döndürsün
    public class ServiceResult<T>
    {
        public T? Data  { get; set; }//eğer başarılı olursa datayı döndürsün
       
        public List<string>? Errors { get; set; }//eğer başarısız olursa bana hata mesajlarını döndürsün
        [JsonIgnore]//ben hatamı kullanıcıya açmak istemiyorum serialaze etmek istemiyorum.
        public HttpStatusCode StatusCode{ get; set; }//Hangi durum kodunu döneceğimizi belirlediğimiz propumdur
        [JsonIgnore] public string? UrlAsCreated { get; set; }//ekleme işleminde 201 dönerken benim url'imide tut ama dışarıya açma

        //benim hatalarım yoksa veya hatalarımın sayısı 0'sa şartıma göre metodum true veya false dönsün   "sadece geti olan bir proptur."
        [JsonIgnore]
        public bool IsSuccess => Errors == null || Errors.Count == 0;
        [JsonIgnore]
        public bool IsFail => !IsSuccess;

        //static olmasının sebebi direk metoda class isminden ulaşabiliyorum
        //protectedda ise miras aldığımız sınıflarda ulaşabiliyorum
        //Static Factory Metod
        public static ServiceResult<T> Success(T data,HttpStatusCode status=HttpStatusCode.OK)
        {
          return new ServiceResult<T>()
            {
              //Eğer başarılıysa bana Entitymi döndür
                Data = data,
                StatusCode= status//eğerki 200den başka bir success kodum gelirse onu ata
            };
        }
        //Ekleme İşlemi için ayrı bir dönüş tipi yapılandırıyorum.
        public static ServiceResult<T>SuccessAsCreated(T data,string urlAsCreated)
        {
            return new ServiceResult<T>
            {
                Data = data,
                UrlAsCreated = urlAsCreated,
                StatusCode = HttpStatusCode.Created
            };
        }
        //birden fazla hata varsa
        public static ServiceResult<T>Fail(List<string>errorMessages,HttpStatusCode status = HttpStatusCode.BadRequest)
        {
            return new ServiceResult<T>
            {
                Errors = errorMessages,
                StatusCode = status//eğerki code 404'den farklı olursa o kodun hatasını tanımla
            };
        }
        //Bir tane hata varsa
        public static ServiceResult<T>Fail(string errorMessage,HttpStatusCode status = HttpStatusCode.BadRequest)
        {
            return new ServiceResult<T>
            {
                Errors =[errorMessage],
                StatusCode = status
            };
        }
    }


    //Update-Deletede ise Normalde nocontent döneceğimden ötürü etkilenen bir veri var ama bana geriye dönmesine gerek yok oyuzden alttaki yazdıklarımda herhangi bir veri geriye dönmesini istemedim.
    public class ServiceResult
    {
        public List<string>? Errors { get; set; }
        [JsonIgnore]
        public HttpStatusCode StatusCode { get; set; }

        [JsonIgnore]
        public bool IsSuccess => Errors == null || Errors.Count == 0;
        [JsonIgnore]
        public bool IsFail => !IsSuccess;

        
        public static ServiceResult Success(HttpStatusCode status = HttpStatusCode.OK)
        {
            return new ServiceResult()
            {
                StatusCode = status
            };
        }
       
        public static ServiceResult Fail(List<string> errorMessages, HttpStatusCode status = HttpStatusCode.BadRequest)
        {
            return new ServiceResult
            {
                Errors = errorMessages,
                StatusCode = status
            };
        }
        //Bir tane hata varsa
        public static ServiceResult Fail(string errorMessage, HttpStatusCode status = HttpStatusCode.BadRequest)
        {
            return new ServiceResult
            {
                Errors = [errorMessage],
                StatusCode = status
            };
        }
    }
}
