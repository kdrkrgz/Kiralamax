using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using System.Text;
using CarRental.Core.Entities;
using CarRental.Core.Entities.Concrete;

namespace CarRental.Business.Constants
{
    public class Message
    {
        public const string CarRentAdded = "Araç başarıyla kiralandı.";
        public const string CarRentAddFailed = "Araç kiralanamadı.";
        public const string CarRentUpdated = "Kiralama başarıyla düzenlendi.";
        public const string CarRentUpdateFailed = "Kiralama başarıyla düzenlendi.";
        public const string CarRentDeleted = "Kiralama başarıyla silindi.";
        public const string CarRentDeleteFailed = "Kiralama silinemedi.";
        public const string RentalCountError = "Bir araç aynı anda bir defa kiralanabilir.";
        public const string CarRentComplateFailed ="Kiralama sonlandırılamadı.";
        public const string CarRentComplated = "Kiralama başarıyla sonlandırıldı.";


        public static string CarAdded = "Yeni araç başarıyla eklendi.";
        public static string CarAddFailed = "Araç eklenirken bir hata oluştu.";
        public static string CarUpdated = "Araç başarıyla güncellendi.";
        public static string CarUpdateFailed = "Araç güncellenirken bir hata oluştu.";
        public static string CarDeleted = "Araç başarıyla silindi.";
        public static string CarDeleteFailed = "Araç silinirken bir hata oluştu.";
               
        public static string CustomerAdded = "Yeni müşteri başarıyla eklendi.";
        public static string CustomerAddFailed = "Müşteri eklenirken bir hata oluştu.";
        public static string CustomerUpdated = "Müşteri başarıyla güncellendi.";
        public static string CustomerUpdateFailed = "Müşteri güncellenirken bir hata oluştu.";
        public static string CustomerDeleted = "Müşteri başarıyla silindi.";
        public static string CustomerDeleteFailed = "Müşteri silinirken bir hata oluştu.";

        public static string CategoryAdded = "Kategori eklendi.";
        public static string CategoryAddFailed = "Kategori eklenirken bir hata oluştu.";
        public static string CategoryUpdated = "Kategori güncellendi.";
        public static string CategoryUpdateFailed = "Kategori güncellenirken bir hata oluştu.";
        public static string CategoryDeleted = "Kategori silindi.";
        public static string CategoryDeleteFailed = "Kategori silinirken bir hata oluştu.";

        public static string CreditCardAdded = "Kredi kartı eklendi.";
        public static string CreditCardAddFiled = "Kredi kartı eklenemedi.";
        public static string CreditCardDeleted = "Kredi kartı silindi.";
        public static string CreditCardDeleteFailed = "Kredi kartı silinemedi.";

        public static string UserEmailExist = "Bu e-mail adresiyle daha önce kayıt olunmuş.";
        public static string AuthorizationFailed = "Bu işlem için gerekli yetkiye sahip değilsiniz.";
        public static string UserNotFound = "Kullanıcı adı veya parola yanlış.";
        public static string UserRegistered = "Kullanıcı oluşturuldu.";
        public static string PasswordError = "Parola hatalı";
        public static string UserAlreadyExists = "Bu kullanıcı zaten mevcut.";
        public static string AccessTokenCreated = "Bağlantı tokeni oluşturuldu.";
        public static string LoginSuccess = "Giriş yapıldı.";

        public static string GeneralSuccessfull = "İşlem başarılı.";
        public static string GeneralFailed = "İşlem başarısız.";

        public static string UserUpdated ="Kullanıcı güncellendi.";
        public static string UserUpdateFailed = "Kullanıcı güncellenemedi.";
        public static string UserRolesUpdateError = "Kullanıcı rolleri güncellenemedi.";
        public static string UserRolesUpdated = "Kullanıcı rolleri güncellendi.";
        public static string UserDeleted = "Kullanıcı silindi.";
        public static string UserDeleteFailed = "Kullanıcı silinemedi.";

        public static string PasswordResetMailSend ="Parola sıfırlama bağlantısı mail adresinize gönderildi.";
        public static string PasswordReseted ="Parola başarıyla değiştirildi.";
        public static string UserNotActivated = "Hesabınız aktif edilmemiş, E-mail adresinize gönderilen bağlantıdan aktifleştirebilirsiniz.";
        public static string UserActivated = "Hesap başarıyla aktif edildi.";
        public static string UserActivateFailed = "Hesap aktifleştirilemedi";

    }

}
