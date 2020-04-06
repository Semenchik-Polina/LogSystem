using AutoMapper;
using LogSystem.BLL.Utils.AutoMapperProfiles;

namespace LogSystem.BLL.Utils
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
