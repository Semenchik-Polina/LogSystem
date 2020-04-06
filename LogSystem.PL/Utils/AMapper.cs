using AutoMapper;
using LogSystem.PL.Utils.AutoMapperProfiles;

namespace LogSystem.PL.Utils
{
    public class AMapper
    {
        public AMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<UserProfile>();
                cfg.AddProfile<UserActionProfile>();
            });
            Mapper = config.CreateMapper();
        }

        public IMapper Mapper { get; }
    }
}