using AutoMapper;
using LogSystem.BLL.DTO.UserDTO;
using LogSystem.PL.Models.UserViewModels;

namespace LogSystem.PL.Utils.AutoMapperProfiles
{
    public class UserProfile: Profile
    {
        public UserProfile()
        {
            CreateMap<UserGetDetailDTO, UserEditorViewModel>();
            
            CreateMap<AuthorizationViewModel, UserLogInDTO>();

            CreateMap<RegistrationViewModel, UserCreateDTO>();

            CreateMap<UserEditorViewModel, UserEditDTO>();
        }
    }
}