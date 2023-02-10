using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using UserLoginFeature.Application.ViewModels.Base;

namespace UserLoginFeature.Application.ViewModels
{
    public class AuthViewModel : BaseViewModel
    {
        [BindProperty] public SignInModel? SignIn { get; set; }
        [BindProperty] public SignUpModel? SignUp { get; set; }
        [TempData] public ConfirmEmailModel? ConfirmEmail { get; set; }
        [BindProperty] public ResetPasswordModel? ResetPassword { get; set; }
        [BindProperty] public ConfirmResetPasswordModel? ConfirmResetPassword { get; set; }

        public class SignInModel
        {
            [Display(Name = "Email", Prompt = "Enter your Email", Description = "Enter your Email here for Sign In..")]
            [Required(AllowEmptyStrings = false, ErrorMessage = "* You must enter your Email for Sign In!")]
            [DataType(DataType.EmailAddress)]
            public string Email { get; set; } = null!;

            [Display(Name = "Password", Prompt = "Enter your Password", Description = "Enter your Password here for Sign In..")]
            [Required(AllowEmptyStrings = false, ErrorMessage = "* You must enter your Password for Sign In!")]
            [DataType(DataType.Password)]
            public string Password { get; set; } = null!;

            [Display(Name = "Remember Me", Description = "Keep Logged In..")]
            public bool RememberMe { get; set; } = true;
        }

        public class SignUpModel
        {
            [Display(Name = "Name", Prompt = "Enter your Name", Description = "Enter your Name here for Sign Up..")]
            [Required(AllowEmptyStrings = false, ErrorMessage = "* You must enter your Name for Sign Up!")]
            public string Name { get; set; } = null!;

            [Display(Name = "Surname", Prompt = "Enter your Surname", Description = "Enter your Surname here for Sign Up..")]
            [Required(AllowEmptyStrings = false, ErrorMessage = "* You must enter your Surname for Sign Up!")]
            public string Surname { get; set; } = null!;

            [Display(Name = "Email", Prompt = "Enter your Email", Description = "Enter your Email here for Sign Up..")]
            [Required(AllowEmptyStrings = false, ErrorMessage = "* You must enter your Email for Sign Up!")]
            [DataType(DataType.EmailAddress)]
            public string Email { get; set; } = null!;

            [Display(Name = "Password", Prompt = "Enter your Password", Description = "Enter your Password here for Sign Up..")]
            [Required(AllowEmptyStrings = false, ErrorMessage = "* You must enter your Password for Sign Up!")]
            [DataType(DataType.Password)]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 8)]
            public string Password { get; set; } = null!;

            [Display(Name = "Confirm Password", Prompt = "Confirm Your Password", Description = "Enter your Password again for Confirm..")]
            [Required(AllowEmptyStrings = false, ErrorMessage = "* Enter your Password again for Confirm!")]
            [DataType(DataType.Password)]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 8)]
            [Compare(nameof(Password), ErrorMessage = "Passwords do not match")]
            public string ConfirmPassword { get; set; } = null!;
        }

        public class ConfirmEmailModel
        {
            [HiddenInput] public string Id { get; set; } = null!;
            [HiddenInput] public string Code { get; set; } = null!;
        }

        public class ResetPasswordModel
        {
            [Display(Name = "Email", Prompt = "Enter your Email", Description = "Enter your Email here for Reset Password..")]
            [Required(AllowEmptyStrings = false, ErrorMessage = "* You must enter your Email for Reset Password!")]
            [DataType(DataType.EmailAddress)]
            public string Email { get; set; } = null!;
        }

        public class ConfirmResetPasswordModel
        {
            public ConfirmResetPasswordModel()
            {
            }

            public ConfirmResetPasswordModel(string email, string code)
            {
                Email = email;
                Code = code;
            }

            [HiddenInput] public string Email { get; set; } = null!;
            [HiddenInput] public string Code { get; set; } = null!;

            [Display(Name = "Password", Prompt = "Enter New Password", Description = "Enter your New Password here for Reset Password..")]
            [Required(AllowEmptyStrings = false, ErrorMessage = "* You must enter your New Password for Reset Password!")]
            [DataType(DataType.Password)]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 8)]
            public string Password { get; set; } = null!;

            [Display(Name = "Confirm Password", Prompt = "Confirm New Password", Description = "Enter Confirm Password here for Reset Password..")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            [DataType(DataType.Password)]
            public string ConfirmPassword { get; set; } = null!;
        }
    }
}
