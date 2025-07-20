


namespace App.Domain.Entities
{
    public class Category:BaseEntity<int>,IAuditEntity
    {
        public string Name { get; set; } = default!;
        public List<Product>? Products{ get; set; }//Kategorinin hiç ürünüde olmayabilir.
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
