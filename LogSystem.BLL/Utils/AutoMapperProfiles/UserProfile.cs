using AutoMapper;
using LogSystem.BLL.DTO.UserDTO;
using LogSystem.DAL.Entities;
using System;

namespace LogSystem.BLL.Utils.AutoMapperProfiles
{
    public class UserProfile: Profile
    {
        // format of DateTime string in SQLite
        private const string dateTimeFormat = "YYYY-MM-DD HH:MM:SS.SSS"; 

        public UserProfile()
        {
            // store date in entities as string because SQLite doesn't have DateTime type
            CreateMap<User, UserGetDetailDTO>()
                .ForMember("RegistrationDate", opt => opt.MapFrom(c => DateTime.ParseExact(c.RegistrationDate, dateTimeFormat, null)));
            CreateMap<User, UserGetDetailDTO>().ReverseMap()
                .ForMember("RegistrationDate", opt => opt.MapFrom(c => c.RegistrationDate.ToString(dateTimeFormat)));

            CreateMap<UserUpdateDTO, User>();

            CreateMap<UserCreateDTO, User>();
        }
    }
}
