using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToBuyApı.Domain.Entities;
using ToBuyAPI.Application.DTOs.Category;
using ToBuyAPI.Application.DTOs.Product;

namespace ToBuyAPI.Application.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
			#region Category
			CreateMap<CreateCategory, Category>();
			CreateMap<DeleteCategory, Category>()
				.ForMember(x => x.Id, dest => dest.MapFrom(x => Guid.Parse(x.Id)));
			CreateMap<Category, ListItemCategory>()
				.ForMember(x => x.Id, dest => dest.MapFrom(x => x.Id.ToString()));
			#endregion

			#region Product
			CreateMap<CreateProduct, Product>().ForMember(x => x.ProductImageFiles, dest => dest.Ignore());
			CreateMap<DeleteProduct, Product>()
				.ForMember(x => x.Id, dest => dest.MapFrom(x => Guid.Parse(x.Id)));
			#endregion

		}
    }
}
