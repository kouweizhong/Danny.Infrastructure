namespace Danny.Infrastructure.WebAPI.DTO
{
    public class ResponseProtocol
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public dynamic Data { get; set; }

        public ResponseProtocol(int code,string message,dynamic data)
        {
            Code = code;
            Message = message;
            Data = data;
        }

        public ResponseProtocol()
        { }
    }
}