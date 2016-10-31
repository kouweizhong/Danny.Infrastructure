using System.Text;
using System.Web.ModelBinding;

namespace Danny.Infrastructure.Extensions
{
    public static class  ModelStateExtension
    {
        /// <summary>
        /// ModeState校验失败时讲转换失败的字符串转换为字符串
        /// </summary>
        public static string ToErrorMessage(this ModelStateDictionary modelStateDictionary)
        {
            var stringBuilder=new StringBuilder();

            foreach (var value in modelStateDictionary.Values)
            {
                foreach (var error in value.Errors)
                {
                    stringBuilder.Append(error.ErrorMessage);
                }
            }

            return stringBuilder.ToString();
        }
    }
}
