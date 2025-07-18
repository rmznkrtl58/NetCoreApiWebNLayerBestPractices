using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.DTOs.ProductDTOs
{   //ürün listelerken kullanıcağım dtom

    //record yapma sebebim olduda iki ürünümü karşılaştırmam gerekti direk olarak propları karşılaştırsın diye record kullandım.bildiğin classtır sadece record olarak belirtiyoruz.
    public record ProductDTO(int Id, string Name, decimal Price, int Stock, int CategoryId);
}
