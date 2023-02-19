using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToBuyApı.Domain.Entities;
using ToBuyAPI.Application.DTOs.Category;

namespace ToBuyAPI.Application.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateCategory, Category>();
            CreateMap<DeleteCategory, Category>()
                .ForMember(x => x.Id, dest => dest.MapFrom(x => Guid.Parse(x.Id)));
            CreateMap<Category, ListItemCategory>()
                .ForMember(x => x.Id, dest => dest.MapFrom(x => x.Id.ToString()));
            CreateMap<Category, DetailCategory>()
                .ForMember(x => x.Id, dest => dest.MapFrom(x => x.Id.ToString()));
        }
    }
}
