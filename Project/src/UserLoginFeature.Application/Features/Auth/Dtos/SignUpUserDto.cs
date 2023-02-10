namespace UserLoginFeature.Application.Features.Auth.Dtos
{
    public class SignUpUserDto
    {
        public SignUpUserDto()
        {
        }

        public SignUpUserDto(string name, string surname, string userName, string email, string password)
        {
            Name = name;
            Surname = surname;
            UserName = userName;
            Email = email;
            Password = password;
        }

        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
