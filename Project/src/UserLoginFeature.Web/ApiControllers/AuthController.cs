using Microsoft.AspNetCore.Mvc;
using UserLoginFeature.Application.Features.Auth.Commands.SendEmailConfirmation;
using UserLoginFeature.Application.Features.Auth.Commands.SendResetPasswordEmail;
using UserLoginFeature.Web.ApiControllers.Base;

namespace UserLoginFeature.Web.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        [HttpPut("[action]")]
        public async Task<IActionResult> SendEmailConfirmationAsync([FromBody] SendEmailConfirmationCommand command)
        {
            await Mediator.Send(command);
            return Ok();
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> SendResetPasswordEmailAsync([FromBody] SendResetPasswordEmailCommand command)
        {
            await Mediator.Send(command);
            return Ok();
        }
    }
}
