namespace UserLoginFeature.Infrastructure.Security
{
    public static class TokenLifespanConfigurations
    {
        public static TimeSpan Email { get; set; } = TimeSpan.FromDays(1);
    }
}
