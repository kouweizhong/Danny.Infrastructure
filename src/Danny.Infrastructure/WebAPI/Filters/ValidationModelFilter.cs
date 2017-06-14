using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Http.ModelBinding;
using Danny.Infrastructure.WebAPI.DTO;
using Newtonsoft.Json;

namespace Danny.Infrastructure.WebAPI.Filters
{
    public class ValidationModelFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            //get方法不进行模型校验
            if (actionContext.Request.Method.Method=="GET")
            {
                return;
            }

            if (!actionContext.ModelState.IsValid)
            {
                var error = JsonConvert.SerializeObject(new ResponseProtocol()
                {
                    Code =(int)ResponseResultEnum.ValidateError,
                    Message = actionContext.ModelState.ToErrorMessage(),
                    Data = string.Empty
                });

                var httpResponseMessage = new HttpResponseMessage
                {
                    Content = new StringContent(error)
                };

                httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                actionContext.Response = httpResponseMessage;
            }
        }

    }

    public static class ModelStateExtension
    {
        public static string ToErrorMessage(this ModelStateDictionary modelStateDictionary)
        {
            var stringBuilder = new StringBuilder();

            foreach (var value in modelStateDictionary.Values)
            {
                foreach (var error in value.Errors)
                {
                    stringBuilder.AppendLine(error.ErrorMessage);
                }
            }

            return stringBuilder.ToString();
        }
    }
}
