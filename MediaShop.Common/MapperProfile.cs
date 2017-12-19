using AutoMapper;

namespace MediaShop.Common
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            Mapper.Initialize(config => config.CreateMap<Models.Entity, Dto.ProductDto>().ForMember(x => x.ProductId, opt => opt.MapFrom(source => source.Id)).ReverseMap());

            Mapper.Initialize(config => config.CreateMap<Models.Content.Product, Dto.ProductDto>().ReverseMap());

            //to do: output result, add source references (objects)
        }
    }
}
