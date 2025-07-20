
namespace App.Domain.Entities
{
    public class Product:BaseEntity<int>,IAuditEntity
    {
        public string Name { get; set; } = default!;//Default olarak atanan null değeri olmayacak anlamına gelir.Sql tablosunda ise null olmayacağını işaret eder.
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int CategoryId{ get; set; }
        public Category Category { get; set; } = default!; //null geçilemez.
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
