using App.Repositories.Entities;
using App.Repositories.RepositoryInterfaces;
using App.Repositories.UnitOfWorkPattern;
using App.Services.DTOs.ProductDTOs;
using App.Services.ExceptionHandlers;
using App.Services.Results;
using App.Services.ServiceInterfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace App.Services.Services
{
    public class ProductService(IProductRepository _pRepository,IUnitOfWork _unitOfWork,IMapper _mapper) : GenericService<Product>, IProductService
    {
        public async Task<ServiceResult<CreateProductResponse>> TCreateProductAsync(CreateProductRequest p)
        {       
                 //Hata yakalama
            //throw new CriticalException("Kritik Seviye Bir Hata Meydana Geldi!");
            var anyProduct =await _pRepository.GetByFilter(x=>x.Name==p.Name).AnyAsync();
            if(anyProduct)
            { 
                return ServiceResult<CreateProductResponse>.Fail("Ürün İsmi Veri Tabanında Bulunmaktadır.",System.Net.HttpStatusCode.BadRequest);
            }
            //var mapValue = new Product()
            //{
            //    Name = p.Name,
            //    Price = p.Price,
            //    Stock = p.Stock,
            //};
            var mapValue = _mapper.Map<Product>(p);
            await _pRepository.CreateAsync(mapValue);
            await _unitOfWork.CommitAsync();
            var insertedValue = new CreateProductResponse(mapValue.Id);
            return ServiceResult<CreateProductResponse>.SuccessAsCreated(insertedValue,$"api/products/{mapValue.Id}");
        }

        public async Task<ServiceResult> TDeleteProductAsync(int id)
        {
            var findProduct = await _pRepository.GetValueByIdAsync(id);
            _pRepository.Delete(findProduct!);//notFoundFilterimda kontrol sağlandığı için artık findProductumun için dolu olacak
            await _unitOfWork.CommitAsync();
            return ServiceResult.Success(System.Net.HttpStatusCode.NoContent);
        }

        public async Task<ServiceResult<List<ProductDTO>>> TGetListProductAsync()
        {
            var products =await _pRepository.GetListAll().ToListAsync();
            //var mapProducts = products.Select(z => new ProductDTO(z.Id, z.Name, z.Price, z.Stock)).ToList();
            var mapProducts = _mapper.Map<List<ProductDTO>>(products);
            return ServiceResult<List<ProductDTO>>.Success(mapProducts, System.Net.HttpStatusCode.OK);
        }

        public async Task<ServiceResult<List<ProductDTO>>> TGetPagedAllListAsync(int pageNumber,int pageSize)
        {
            //1.sayfada 10 veri listeleme=>(1-0)*10
            //2.sayfada 10 veri listeleme=>(2-1)*10
            var calculate = (pageNumber - 1) * pageSize;
            var pagination = await _pRepository.GetListAll().Skip(calculate).Take(pageSize).ToListAsync();
            //var mapProducts = pagination.Select(x => new ProductDTO(x.Id, x.Name, x.Price, x.Stock)).ToList();
            var mapProducts = _mapper.Map<List<ProductDTO>>(pagination);
            return ServiceResult<List<ProductDTO>>.Success(mapProducts, System.Net.HttpStatusCode.OK);
        }

        public async Task<ServiceResult<ProductDTO?>> TGetProductByIdAsync(int id)
        {
            var findProduct = await _pRepository.GetValueByIdAsync(id);
            if(findProduct is null)
            {
                return ServiceResult<ProductDTO>.Fail($"{id}'li ürün Bulunamadı!", System.Net.HttpStatusCode.NotFound);
            }
            //var mapProducts = new ProductDTO(findProduct.Id, findProduct.Name, findProduct.Price, findProduct.Stock);
            var mapProducts = _mapper.Map<ProductDTO>(findProduct);
            return ServiceResult<ProductDTO>.Success(mapProducts)!;//"!"null olmayacak
        }

        public async Task<ServiceResult<List<ProductDTO>>> TGetTopPriceProductsAsync()
        {
            var products =await _pRepository.GetTopPriceProductsAsync();
            if (products is null)
            {
                return ServiceResult<List<ProductDTO>>.Fail("Yüksek Fiyatlı Ürünler Bulunamadı!", System.Net.HttpStatusCode.NotFound);
            }
            //var mapProducts = products.Select(x => new ProductDTO(x.Id, x.Name, x.Price, x.Stock)).ToList();
            var mapProducts = _mapper.Map<List<ProductDTO>>(products);
            return ServiceResult<List<ProductDTO>>.Success(mapProducts);
        }

        public async Task<ServiceResult> TUpdateProductAsync(UpdateProductRequest p)
        {
            var findProduct = await _pRepository.GetValueByIdAsync(p.Id);
            //Güncellenecek Ürünün ismi veri tabanımdan biriyle eşleşiyorsa ve Ürünün Id değeri veri tabanımdan birine eşleşmiyorsa true dön olay şu aslında adam ürünün ismini değiştirmez sadece price veya stoğu güncellerse ozmn iki şarttada bak demek istiyor.
            var isProductNameExist = await _pRepository.GetByFilter(x => x.Name == findProduct!.Name && x.Id != findProduct.Id).AnyAsync();
            if (isProductNameExist)
            {
                return ServiceResult.Fail("Aynı Ürün İsmi Veri Tabanında Mevcuttur.", System.Net.HttpStatusCode.BadRequest);
            }
            //findProduct.Stock = p.Stock;
            //findProduct.Price = p.Price;
            //findProduct.Name = p.Name;
            //Ekleme işlemindeki gibi maplamadan farklı olarak yeni bir product sınıfını source olarak kabul etmeyip zaten bulduğum mevcut olan findproduct fieldimi source olarak kabul ederim.hedef olarakta p parametremden gelen requestimi kabul ederim.
            var mapProduct = _mapper.Map(p,findProduct);
            _pRepository.Update(mapProduct!);
            await _unitOfWork.CommitAsync();
            return ServiceResult.Success(System.Net.HttpStatusCode.NoContent);
        }

        public async Task<ServiceResult> TUpdateStockAsync(UpdateProductStockRequest p)
        {
            var findProduct=await _pRepository.GetValueByIdAsync(p.productId);
            if(findProduct is null)
            {
                return ServiceResult.Fail($"{p.productId}'li Ürün Bulunamadı!", System.Net.HttpStatusCode.NotFound);
            }
            findProduct.Stock = p.quantity;
            _pRepository.Update(findProduct);
            await _unitOfWork.CommitAsync();
            return ServiceResult.Success(System.Net.HttpStatusCode.NoContent);
        }
    }
}
