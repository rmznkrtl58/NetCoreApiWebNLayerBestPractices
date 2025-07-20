using App.Application.Features.DTOs.ProductDTOs;
using App.Application.Results;
using App.Domain.Entities;

namespace App.Application.Features.ServiceInterfaces
{
    public interface IProductService:IGenericService<Product>
    {
        Task<ServiceResult<List<ProductDTO>>>TGetListProductAsync();
        Task<ServiceResult<List<ProductDTO>>> TGetTopPriceProductsAsync();
        Task<ServiceResult<ProductDTO?>> TGetProductByIdAsync(int id);
        Task<ServiceResult<CreateProductResponse>> TCreateProductAsync(CreateProductRequest p);
        Task<ServiceResult> TUpdateProductAsync(UpdateProductRequest p);
        Task<ServiceResult> TDeleteProductAsync(int id);
        Task<ServiceResult<List<ProductDTO>>> TGetPagedAllListAsync(int pageNumber, int pageSize);
        Task<ServiceResult> TUpdateStockAsync(UpdateProductStockRequest p);
    }
}
