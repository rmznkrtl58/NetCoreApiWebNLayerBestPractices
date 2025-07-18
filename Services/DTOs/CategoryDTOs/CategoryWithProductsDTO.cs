using App.Services.DTOs.ProductDTOs;

namespace App.Services.DTOs.CategoryDTOs
{
    public record CategoryWithProductsDTO(int Id, string Name, List<ProductDTO> Products);
}
