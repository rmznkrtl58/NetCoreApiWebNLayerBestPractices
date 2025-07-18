using App.Repositories.Context;


namespace App.Repositories.UnitOfWorkPattern
{
    public class UnitOfWork(AppDbContext context) : IUnitOfWork
    {   //UnitOfWork kullanmamızın amacı işlemleri sağlama almak atıyorum bir banka işlemi yapıyorsunuz para gönderiyorsunuz bir anda elektirik kesildi noldu para karşı tarafa geçmedi sadece para gönderme işlemi yapıldı fakat kaydetme işlemi gerçekleşmedi! amaç,ilk önce veri tabanı işlemlerini yaptır sonra kaydetme işlemini çağırki iptal olunan işlemler sıkıntıya girmesin!

       
        
        //int türünde tutma sebebim kayıtın etkilenme durumunu "1" olarak gösterecek
        public Task<int> CommitAsync() => context.SaveChangesAsync();
        
    }
}
