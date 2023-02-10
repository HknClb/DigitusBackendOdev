namespace UserLoginFeature.Application.Abstractions.Services
{
    public interface IAccountVerificationStatsService
    {
        Task<int> GetSuccessfullyRegisteredUsersCountAsync();
        Task<int> GetEmailUnconfirmedUsersCountAsync();
        Task<int> GetAverageConfirmationSecondsAsync(DateTime date);
    }
}
