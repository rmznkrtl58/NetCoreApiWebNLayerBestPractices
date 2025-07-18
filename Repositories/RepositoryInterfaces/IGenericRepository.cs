using System.Linq.Expressions;


namespace App.Repositories.RepositoryInterfaces
{   //temel crudlarımı içeren imzalarımı tutan yapımdır
    public interface IGenericRepository<T,TId> where T : class where TId:struct
    {

       //Queryable kullanmamın sebebi olurda service katmanımda listelemek istediğim veriyi orderbydesc veya orderbyasc veya listelenen verilerin take metoduylada çağırma ihtimalim var direk listeleme olmaz yani ben burda sadece listele olarak verilerimi çağırıyorum service tarafında zaten gerekli durumları ekleyip öyle çağıracağım.  
       
        //Tümünü Listele
        IQueryable<T> GetListAll();
        //Şarta Göre Listele expression'ın diğer hali predicate
        IQueryable<T> GetByFilter(Expression<Func<T, bool>> filter);
        //Id'ye göre getir
        ValueTask<T?> GetValueByIdAsync(int id);
        //Ekleme işlemi ama ekleme işlemi olduğu sırada eklenen entitymide getir
        ValueTask CreateAsync(T t);
        //Güncelleme işlemi task istemiyoruz çünkü service tarafında commitasync kullanıcaz
        void Update(T t);
        //Silme işlemi task istemiyoruz çünkü service tarafında commitasync kullanıcaz
        void Delete(T t);
        Task<bool> AnyAsync(TId id);
    }
}
