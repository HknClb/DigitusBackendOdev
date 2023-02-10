using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UserLoginFeature.Domain.Entities.Identity;
using UserLoginFeature.WebAPI.Controllers.Base;

namespace WebAPI.Controllers
{
    public class TestModel
    {
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly UserManager<User> _userManager;

        public AuthController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        //[HttpPost]
        //[HttpPost("[action]")]
        //public async Task<IActionResult> SignUpAsync([FromBody] CreateUserCommand command)
        //{
        //    CreatedUserModel createdUserModel = await Mediator.Send(command);
        //    if (!createdUserModel.Succeeded)
        //        return BadRequest(createdUserModel.Message);
        //    return Created("", createdUserModel);
        //}

        //[HttpPut]
        //[HttpPut("[action]")]
        //public async Task<IActionResult> SignInAsync([FromBody] SignInCommand command)
        //{
        //    SignedInDto result = await Mediator.Send(command);
        //    return Ok(result);
        //}

        //[HttpPut("[action]")]
        //public async Task<IActionResult> GoogleSignInAsync([FromBody] GoogleSignInCommand command)
        //{
        //    SignedInDto result = await Mediator.Send(command);
        //    return Ok(result);
        //}

        //[HttpPut("[action]")]
        //public async Task<IActionResult> RefreshTokenSignInAsync([FromBody] RefreshTokenSignInCommand command)
        //{
        //    SignedInDto result = await Mediator.Send(command);
        //    return Ok(result);
        //}
    }
}
