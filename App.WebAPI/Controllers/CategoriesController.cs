using App.Application.Features.DTOs.CategoryDTOs;
using App.Application.Features.ServiceInterfaces;
using App.Domain.Entities;
using App.WebAPI.Filters;
using Microsoft.AspNetCore.Mvc;

namespace App.WebAPI.Controllers
{
    public class CategoriesController(ICategoryService _cService) : CustomBaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetListAll()
        {
            var result = await _cService.TGetListCategoryAsync();
            return CreateActionResult(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _cService.TGetCategoryByIdAsync(id);
            return CreateActionResult(result);
        }
        [HttpGet("products")]
        public async Task<IActionResult> GetCategoryWithProducts()
        {
            var result = await _cService.TGetCategoryWithProductsAsync();
            return CreateActionResult(result);
        }
        [HttpGet("{id}/products")]
        public async Task<IActionResult> GetCategoryWithProducts(int id)
        {
            var result = await _cService.TGetCategoryWithProductsAsync(id);
            return CreateActionResult(result);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryDTO c)
        {
            var result = await _cService.TCreateCategoryAsync(c);
            return CreateActionResult(result);
        }
        [ServiceFilter(typeof(NotFoundFilter<Category, int>))]
        [HttpPut]
        public async Task<IActionResult> Update(UpdateCategoryDTO c)
        {
            var result = await _cService.TUpdateCategoryAsync(c);
            return CreateActionResult(result);
        }
        [ServiceFilter(typeof(NotFoundFilter<Category, int>))]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _cService.TDeleteCategoryAsync(id);
            return CreateActionResult(result);
        }
    }
}
