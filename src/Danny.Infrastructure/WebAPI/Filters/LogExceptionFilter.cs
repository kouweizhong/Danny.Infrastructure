using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using Danny.Infrastructure.Helper.Log;
using Danny.Infrastructure.WebAPI.DTO;
using Newtonsoft.Json;

namespace Danny.Infrastructure.WebAPI.Filters
{
    public class LogExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
//            if (context.Request.IsLocal())
//            {
//                return;
//            }

            LogHelper.Current.Error(context.Exception);

            var jsonData = JsonConvert.SerializeObject(new ResponseProtocol()
            {
                Code =(int)ResponseResultEnum.Error,
                Message = "系统异常",
                Data = string.Empty
            });

            var httpResponseMessage = new HttpResponseMessage
            {
                Content = new StringContent(jsonData),
                StatusCode = HttpStatusCode.OK
            };

            context.Response= httpResponseMessage;
        }
    }
}