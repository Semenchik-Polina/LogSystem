using AutoMapper;
using LogSystem.BLL.DTO.UserActionDTO;
using LogSystem.PL.Models;

namespace LogSystem.PL.Utils.AutoMapperProfiles
{
    public class UserActionProfile: Profile
    {
        public UserActionProfile()
        {
            CreateMap<UserActionGetDetailDTO, UserActionViewModel>()
                .ForMember("UserName", opt => opt.MapFrom(c => c.User.UserName));
        }
    }
}