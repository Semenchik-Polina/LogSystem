using LogSystem.BLL.DTO.UserDTO;
using LogSystem.BLL.Interfaces;
using LogSystem.PL.Models.UserViewModels;
using LogSystem.PL.Utils;
using System.Threading.Tasks;
using System.Web.Http;

namespace LogSystem.PL.Controllers.api
{
    [AllowAnonymous]
    public class SignUpController : ApiController
    {
        private IUserService UserService { get; set; }
        private IValidationService ValidationService { get; set; }
        private AMapper AMapper { get; set; }

        public SignUpController(IUserService userService, IValidationService valService)
        {
            UserService = userService;
            ValidationService = valService;
            AMapper = new AMapper();
        }

        // POST: api/SignUp
        [HttpPost]
        public async Task<IHttpActionResult> PostAsync([FromBody]RegistrationViewModel regVM)
        {
            UserCreateDTO userCreateDTO = AMapper.Mapper.Map<RegistrationViewModel, UserCreateDTO>(regVM);
            int newUserId = await UserService.SignUp(userCreateDTO);

            if (newUserId < 0)
            {
                return BadRequest();
            }

            return Ok(newUserId);
        }
    }
}
