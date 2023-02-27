using AutoMapper;
using ToBuyApı.Domain.Entities;
using ToBuyApı.Domain.Entities.Identity;
using ToBuyAPI.Application.DTOs.AppUser;
using ToBuyAPI.Application.DTOs.Category;
using ToBuyAPI.Application.DTOs.Product;
using ToBuyAPI.Application.DTOs.ToBuyList;

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
			CreateMap<Product, ListItemProduct>()
				.ForMember(x => x.Id, dest => dest.MapFrom(x => x.Id.ToString()))
				.ForMember(x => x.Categories, dest => dest.Ignore());
			#endregion

			#region ToBuyList
			CreateMap<CreateToBuyList, ToBuyList>();
			CreateMap<DeleteToBuyList, ToBuyList>().ForMember(x => x.Id, dest => dest.MapFrom(x => Guid.Parse(x.Id)));
			CreateMap<ToBuyList, ListItemToBuyList>()
				.ForMember(x => x.Id, dest => dest.MapFrom(x => x.Id.ToString()))
				.ForMember(x => x.Categories, dest => dest.Ignore());
			#endregion

			#region AppUser
			CreateMap<CreateAppUser, AppUser>();
			#endregion

		}
	}
}
