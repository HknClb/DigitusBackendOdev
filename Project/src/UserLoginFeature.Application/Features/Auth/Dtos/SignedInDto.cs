namespace UserLoginFeature.Application.Features.Auth.Dtos
{
    public class SignedInDto
    {
        public string Id { get; set; } = null!;
        public bool EmailConfirmationRequired { get; set; } = false;
        public List<string> Errors { get; } = new List<string>();
    }
}