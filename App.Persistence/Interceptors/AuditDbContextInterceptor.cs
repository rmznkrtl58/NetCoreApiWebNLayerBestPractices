using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace App.Persistence.Interceptors
{
    public class AuditDbContextInterceptor:SaveChangesInterceptor
    {   
        //Delege kullanımı geriye değer döndürmeyen ve parametre içeren kullanım
        private static readonly Dictionary<EntityState, Action<DbContext, IAuditEntity>> _behaviors = new()
        {
            {EntityState.Added,AddBehavior },
            {EntityState.Modified,ModifiedBehavior}
        };
        private static void AddBehavior(DbContext context,IAuditEntity auditEntity)
        {
            //benim entitylerim IAuditEntityden miras almış ve nesne oluşturmuş ise
            auditEntity.CreatedDate = DateTime.Now;
            //Entityme eklenme olduğu sırada güncellenme tarihine dokunma hiç bir değişiklik yapma
            context.Entry(auditEntity).Property(x => x.UpdatedDate).IsModified = false;
        }

        private static void ModifiedBehavior(DbContext context, IAuditEntity auditEntity)
        {
            //Entityme güncelleme olduğu sırada eklenme tarihine dokunma hiç bir değişiklik yapma
            context.Entry(auditEntity).Property(x => x.CreatedDate).IsModified = false;
            auditEntity.UpdatedDate = DateTime.Now;
        }
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            //Contexte takip edilen tüm entitylerin haraketlerini listele bu listenin içinde entitynin stateleride mevcuttur
            var events = eventData.Context!.ChangeTracker.Entries().ToList();
            foreach (var entityEntry in events)
            {
                //IAuditEntityi almayan bir entitym gelirse devam et işine switche girmene gerek yok
                if (entityEntry.Entity is not IAuditEntity auditEntity)continue;
                //eğerki benim gelen entitylerimin durumları ekleme veya güncelleme ise devam et 
                if (entityEntry.State is not (EntityState.Added or EntityState.Modified)) continue;

                          //*2.yol*Switch case yerine kullanım
                _behaviors[entityEntry.State](eventData.Context, auditEntity);
                
                              //*1.yol*
                    //switch (entityEntry.State)
                    //{

                    //    case EntityState.Added:
                    //AddBehavior(eventData.Context,auditEntity);
                    //    break;

                    //    case EntityState.Modified:
                    //ModifiedBehavior(eventData.Context, auditEntity);
                    //    break;

                    //}
                }
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
    }
}
