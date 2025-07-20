

namespace App.Application.Features.DTOs.ProductDTOs;


public record CreateProductRequest(string Name, decimal Price, int Stock,int CategoryId);

