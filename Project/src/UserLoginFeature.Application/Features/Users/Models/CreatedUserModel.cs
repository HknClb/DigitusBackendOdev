using UserLoginFeature.Application.Features.Users.Dtos;

namespace UserLoginFeature.Application.Features.Users.Models
{
    public class CreatedUserModel
    {
        public CreatedUserModel()
        {
        }

        public CreatedUserModel(bool succeeded = true, string message = "") : this()
        {
            Succeeded = succeeded;
            Message = message;
        }

        public bool Succeeded { get; set; } = true;
        public string Message { get; set; } = "";
        public CreatedUserDto CreatedUserDto { get; set; } = null!;
    }
}
