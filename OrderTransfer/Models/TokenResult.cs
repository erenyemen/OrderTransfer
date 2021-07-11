using System;

namespace OrderTransfer.Models
{
    public class TokenResult
    {
        public string access_token { get; set; }

        public int expires_in { get; set; }

        public string token_type { get; set; }

        public string scope { get; set; }

        public string error { get; set; }

        public DateTime AccessTokenCreatedDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsExpired 
        {
            get
            {
                return AccessTokenCreatedDate.AddMinutes(40) <= DateTime.Now ? true : false;
            }
        }
    }
}