namespace MediaShop.Common
{
    using AutoMapper;
    using MediaShop.Common.Models;

    /// <summary>
    /// Class for create MapperProfile object
    /// </summary>
    public class MapperProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MapperProfile"/> class.
        /// </summary>
        public MapperProfile()
        {
            this.CreateMap<ContentClassForUnitTest, ContentCart>()
                .ForMember(item => item.CreatorId, m => m.Ignore())
                .ReverseMap();
            this.CreateMap<ContentCart, ContentCartDto>()
                .ReverseMap();
        }
    }
}
