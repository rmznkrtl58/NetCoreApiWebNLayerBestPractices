using App.Repositories.Entities;
using App.Services.DTOs.ProductDTOs;
using App.Services.Filters;
using App.Services.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers
{
    public class ProductsController(IProductService _pService) : CustomBaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetListAll()
        {
            var values = await _pService.TGetListProductAsync();
            return CreateActionResult(values);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var value=await _pService.TGetProductByIdAsync(id);
            return CreateActionResult(value);
        }
        [HttpGet("{pageNumber:int}/{pageSize:int}")]
        public async Task<IActionResult> GetPagedList(int pageNumber,int pageSize)
        {
            var values = await _pService.TGetPagedAllListAsync(pageNumber,pageSize);
            return CreateActionResult(values);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateProductRequest p)
        {
            var result = await _pService.TCreateProductAsync(p);
            return CreateActionResult(result);
        }
        [ServiceFilter(typeof(NotFoundFilter<Product, int>))]
        [HttpPut]
        public async Task<IActionResult> Update(UpdateProductRequest p)
        {
            var result = await _pService.TUpdateProductAsync(p);
            return CreateActionResult(result);
        }
        [HttpPatch("stock")]//Kısmi güncellemelerde kullanılır
        public async Task<IActionResult> UpdateStock(UpdateProductStockRequest p)
        {
            var result = await _pService.TUpdateStockAsync(p);
            return CreateActionResult(result);
        }
        [ServiceFilter(typeof(NotFoundFilter<Product,int>))]//service filter kullanma sebebim notfoundFilter clasımda parametre ve contructor geçtiğim için
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Remove(int id)
        {
            var result = await _pService.TDeleteProductAsync(id);
            return CreateActionResult(result);
        }
    }
}
