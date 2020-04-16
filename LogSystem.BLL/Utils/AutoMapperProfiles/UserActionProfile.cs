using AutoMapper;
using LogSystem.BLL.DTO.UserActionDTO;
using LogSystem.DAL.Entities;
using System;

namespace LogSystem.BLL.Utils.AutoMapperProfiles
{
    public class UserActionProfile: Profile
    {
        // format of DateTime string in SQLite
        private const string dateTimeFormat = "yyyy-MM-dd HH:mm:ss.fff";

        public UserActionProfile()
        {
            // store date in entities as string because SQLite doesn't have DateTime type
            CreateMap<UserAction, UserActionGetDetailDTO>()
                .ForMember("Date", opt => opt.MapFrom(c => DateTime.ParseExact(c.Date, dateTimeFormat, null)));

            CreateMap<UserActionCreateDTO, UserAction>()
                .ForMember("Date", opt => opt.MapFrom(c => c.Date.ToString(dateTimeFormat)));

            CreateMap<UserActionGetDetailDTO, UserActionReportDTO>()
                .ForMember("UserName", opt => opt.MapFrom(c => c.User.UserName));
        }
    }
}
