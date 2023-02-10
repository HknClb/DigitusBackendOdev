namespace UserLoginFeature.Application.ViewModels.Base
{
    public class BaseViewModel
    {
        public string? ReturnUrl { get; set; }
        public IList<string> StatusMessages { get; } = new List<string>();
    }
}
