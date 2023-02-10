using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserLoginFeature.Application.Abstractions.Services;
using UserLoginFeature.Application.ViewModels;
using UserLoginFeature.Web.Controllers.Base;

namespace UserLoginFeature.Web.Controllers
{
    [Route("[controller]")]
    [Authorize(Roles = "Administrator")]
    public class StatsController : BaseController
    {
        private readonly IAccountVerificationStatsService _accountVerificationStatsService;

        public StatsController(IAccountVerificationStatsService accountVerificationStatsService)
        {
            _accountVerificationStatsService = accountVerificationStatsService;
        }

        [HttpGet("WebSite")]
        public async Task<IActionResult> IndexAsync()
        {
            StatsViewModel model = new()
            {
                WebSiteStats = new()
                {
                    AverageConfirmationSeconds = await _accountVerificationStatsService.GetAverageConfirmationSecondsAsync(DateTime.UtcNow),
                    SuccessfullyRegisteredUsersCount = await _accountVerificationStatsService.GetSuccessfullyRegisteredUsersCountAsync(),
                    EmailUnconfirmedUsersCount = await _accountVerificationStatsService.GetEmailUnconfirmedUsersCountAsync(),
                    OnlineUsers = 0
                }
            };
            return View(model);
        }
    }
}
