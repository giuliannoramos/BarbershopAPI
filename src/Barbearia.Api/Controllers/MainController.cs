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
        // Este método substitui o método padrão de tratamento de problemas de validação.
        // Ele é chamado quando ocorrem erros de validação em uma ação do controlador.
        public override ActionResult ValidationProblem(
            [ActionResultObjectValue] ModelStateDictionary modelStateDictionary
        )
        {
            // Obtém as opções de comportamento da API a partir do serviço de solicitação HTTP.
            var options = HttpContext.RequestServices
                .GetRequiredService<IOptions<ApiBehaviorOptions>>();

            // Retorna uma resposta personalizada com base nas opções de comportamento da API.
            return (ActionResult)options.Value
                .InvalidModelStateResponseFactory(ControllerContext);
        }

        // Este método é marcado como [NonAction], o que significa que não é uma ação do controlador
        // e não pode ser acessado diretamente como uma rota da Web.
        // Ele é usado para lidar com problemas de validação em respostas personalizadas.
        [NonAction]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        protected ActionResult RequestValidationProblem(
            BaseResponse requestResponse,
            [ActionResultObjectValue] ModelStateDictionary modelStateDictionary
        )
        {
            // Itera pelos erros na resposta personalizada e adiciona-os ao ModelStateDictionary
            foreach (var error in requestResponse.Errors)
            {
                string key = error.Key;
                string[] values = error.Value;

                foreach (var value in values)
                {
                    modelStateDictionary.AddModelError(key, value);
                }
            }

            // Chama o método ValidationProblem padrão, passando o ModelStateDictionary.
            return ValidationProblem(modelStateDictionary);
        }
    }
}