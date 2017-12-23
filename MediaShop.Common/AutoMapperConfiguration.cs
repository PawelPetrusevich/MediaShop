namespace MediaShop.Common
{
    using AutoMapper;

    /// <summary>
    /// Class AutoMapperConfiguration
    /// </summary>
    public class AutoMapperConfiguration
    {
        /// <summary>
        /// Method for configure Mapper
        /// </summary>
        public static void Configure()
        {
            Mapper.Initialize(x =>
            {
                x.AddProfile<MapperProfile>();
            });
        }
    }
}
