namespace Danny.Infrastructure.WebAPI.DTO
{
    public class CacheCustomerModel
    {
        public long CustomerId { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>
  
        public string Name { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { get; set; }
    }
}