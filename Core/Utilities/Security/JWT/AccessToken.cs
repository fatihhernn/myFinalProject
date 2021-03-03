using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.JWT
{
    public class AccessToken//erişim anahtarı
    {
        public string Token { get; set; } //jwt-nin kendisi
        public DateTime Expiration { get; set; } //ne zaman sonlanacağını verir
    }
}
