using System.Diagnostics;
using Barbearia.Application.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;

namespace Barbearia.Api.Controllers
{
    [ApiController]
    public abstract class MainController : ControllerBase
    {
        // Método protegido para retornar um problema de validação com base em um objeto BaseResponse e um ModelState.
        [NonAction]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        protected ActionResult RequestValidationProblem(
            BaseResponse requestResponse,
            [ActionResultObjectValue] ModelStateDictionary modelStateDictionary
        )
        {
            foreach (var error in requestResponse.Errors)
            {
                string key = error.Key;
                string[] values = error.Value;

                foreach (var value in values)
                {
                    modelStateDictionary.AddModelError(key, value);
                }
            }

            return ValidationProblem(modelStateDictionary);
        }
    }
}