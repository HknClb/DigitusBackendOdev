using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace UserLoginFeature.Web.ApiControllers.Base
{
    public class BaseController : ControllerBase
    {
        private IMediator? _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>()!;
    }
}
