using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Business.Constants
{
    public static class Messages
    {
        public static string ProductAdded = "Urun eklendi";

        public static string ProductNameInvalid = "Urun ismi gecersiz";

        public static string ProductListed = "Urunler listelendi";

        public static string MaintenanceTime = "Sistem Bakımda";

        public static string ProductCountError = "Kategori sınırı aşıldı";

        public static string ProductNameAlreadyExist = "Bu isimde zaten başka ürün var";

        public static string CategoryLimitExceed = "Kategori limiti aşıldığı için yeni ürün eklenemiyor";
        internal static string AuthorizationDenied = "yetkiniz yok";

        public static string UserRegistered = "kayıt oldu";

        public static string UserNotFound = "kullanıcı bulunamadı";
        public static string PasswordError = "şifre hatalı";
        public static string SuccessfulLogin = "başarılı giriş";

        public static string UserAlreadyExists = "kullanıcı mevcut";
        public static string AccessTokenCreated = "token oluşturuldu";
    }
}
