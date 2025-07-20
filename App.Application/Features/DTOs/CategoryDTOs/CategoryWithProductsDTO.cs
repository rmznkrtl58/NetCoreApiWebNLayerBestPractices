using App.Application.Features.DTOs.ProductDTOs;

namespace App.Application.Features.DTOs.CategoryDTOs
{
    public record CategoryWithProductsDTO(int Id, string Name, List<ProductDTO> Products);
}
