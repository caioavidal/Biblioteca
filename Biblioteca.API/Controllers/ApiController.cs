using Biblioteca.API.Application;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.API.Controllers
{
    public class ApiController : ControllerBase
    {
        protected ActionResult HandleResponse(OperationResult result, ActionResult sucessResult)
        {
            if (result.Succedeed) return sucessResult;

            return result.StatusCode switch
            {
                ErrorCode.BadRequest => BadRequest(result.ErrorMessage),
                ErrorCode.NotFound => NotFound(result.ErrorMessage),
                ErrorCode.InternalServerError => StatusCode(500, result.ErrorMessage),
                _ => StatusCode(500, result.ErrorMessage)
            };
        }
    }
}
