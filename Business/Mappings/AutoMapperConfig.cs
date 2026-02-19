using AutoMapper;

namespace PruebaTecnica.Business.Mappings
{
    public static class AutoMapperConfig
    {
        public static IMapper Mapper { get; private set; }
        
        public static void Configure()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<UserProfile>();
            });

            Mapper = config.CreateMapper();
        }
    }
}