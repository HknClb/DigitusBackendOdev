using UserLoginFeature.Application.ViewModels.Base;

namespace UserLoginFeature.Application.ViewModels
{
    public class StatsViewModel : BaseViewModel
    {
        public WebsiteStatsModel? WebSiteStats { get; set; }

        public class WebsiteStatsModel
        {
            public int OnlineUsers { get; set; }
            public int SuccessfullyRegisteredUsersCount { get; set; }
            public int EmailUnconfirmedUsersCount { get; set; }
            public int AverageConfirmationSeconds { get; set; }
        }
    }
}
