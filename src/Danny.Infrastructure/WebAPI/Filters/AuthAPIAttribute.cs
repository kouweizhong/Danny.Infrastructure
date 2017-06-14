using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Danny.Infrastructure.Session;
using Danny.Infrastructure.WebAPI.DTO;

namespace Danny.Infrastructure.WebAPI.Filters
{
    public class AuthAPIAttribute:  ActionFilterAttribute
    {
        public ISessionManager SessionManager { get; set; }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            base.OnActionExecuting(actionContext);

//            if (actionContext.Request.IsLocal())
//            {
//                return;
//            }

            var token = actionContext.Request.GetQueryNameValuePairs().FirstOrDefault(f => f.Key == "token").Value;
            if (string.IsNullOrEmpty(token))
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.OK,
                    new ResponseProtocol((int)ResponseResultEnum.ValidateError, "token参数找不到", token));
            }

            var customer = SessionManager.Get<CacheCustomerModel>(token);
            if (customer==null)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.OK,
                    new ResponseProtocol((int)ResponseResultEnum.TokenDad, "token已过期请重新登录", token));
            }
        }
    }
}