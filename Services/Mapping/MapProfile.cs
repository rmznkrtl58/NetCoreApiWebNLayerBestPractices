using App.Repositories.Entities;
using App.Services.DTOs.CategoryDTOs;
using App.Services.DTOs.ProductDTOs;
using AutoMapper;

namespace App.Services.Mapping
{
    public class MapProfile:Profile
    {
        public MapProfile()
        {   //Product
            CreateMap<Product, ProductDTO>().ReverseMap();

            //Ürün Ekleme ve Güncelleme işleminde maplama yaparken CreateProductRequest kaynağımdan gelen name değerini küçük harfe çevir ve Product Entitymdeki name ile maplerken küçük harfe çevirip maple
            CreateMap<CreateProductRequest, Product>().ForMember(dest=>dest.Name,opt=>opt.MapFrom(src=>src.Name.ToLowerInvariant()));

            CreateMap<UpdateCategoryDTO,Product>().ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.ToLowerInvariant()));

            //Category
            //Kategori Ekleme ve Güncelleme işleminde maplama yaparken CreateCategoryRequest kaynağımdan gelen name değerini büyük harfe çevir ve category Entitymdeki name ile maplerken küçük harfe çevirip maple
            CreateMap<CreateCategoryDTO,Category>().ForMember(c => c.Name, opt => opt.MapFrom(src => src.Name.ToUpperInvariant()));

            CreateMap<UpdateCategoryDTO,Category>().ForMember(x => x.Name, opt => opt.MapFrom(x => x.Name.ToUpperInvariant()));
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Category, CategoryWithProductsDTO>().ReverseMap();
        }
    }
}
