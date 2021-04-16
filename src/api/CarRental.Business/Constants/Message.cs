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

        public const string CarAdded = "Yeni araç başarıyla eklendi.";
        public const string CarAddFailed = "Araç eklenirken bir hata oluştu.";
        public const string CarUpdated = "Araç başarıyla güncellendi.";
        public const string CarUpdateFailed = "Araç güncellenirken bir hata oluştu.";
        public const string CarDeleted = "Araç başarıyla silindi.";
        public const string CarDeleteFailed = "Araç silinirken bir hata oluştu.";
        public const string CarPhotoDeleted = "Araç fotoğrafı başarıyla silindi.";
        public const string CarPhotoDeleteFailed = "Araç fotoğrafı başarıyla silindi.";        
               
        public const string CustomerAdded = "Yeni müşteri başarıyla eklendi.";
        public const string CustomerAddFailed = "Müşteri eklenirken bir hata oluştu.";
        public const string CustomerUpdated = "Müşteri başarıyla güncellendi.";
        public const string CustomerUpdateFailed = "Müşteri güncellenirken bir hata oluştu.";
        public const string CustomerDeleted = "Müşteri başarıyla silindi.";
        public const string CustomerDeleteFailed = "Müşteri silinirken bir hata oluştu.";

        public const string CategoryAdded = "Kategori eklendi.";
        public const string CategoryAddFailed = "Kategori eklenirken bir hata oluştu.";
        public const string CategoryUpdated = "Kategori güncellendi.";
        public const string CategoryUpdateFailed = "Kategori güncellenirken bir hata oluştu.";
        public const string CategoryDeleted = "Kategori silindi.";
        public const string CategoryDeleteFailed = "Kategori silinirken bir hata oluştu.";

        public const string CreditCardAdded = "Kredi kartı eklendi.";
        public const string CreditCardAddFiled = "Kredi kartı eklenemedi.";
        public const string CreditCardDeleted = "Kredi kartı silindi.";
        public const string CreditCardDeleteFailed = "Kredi kartı silinemedi.";

        public const string UserEmailExist = "Bu e-mail adresiyle daha önce kayıt olunmuş.";
        public const string AuthorizationFailed = "Bu işlem için gerekli yetkiye sahip değilsiniz.";
        public const string UserNotFound = "Kullanıcı adı veya parola yanlış.";
        public const string UserRegistered = "Kullanıcı oluşturuldu.";
        public const string PasswordError = "Parola hatalı";
        public const string UserAlreadyExists = "Bu kullanıcı zaten mevcut.";
        public const string AccessTokenCreated = "Bağlantı tokeni oluşturuldu.";
        public const string LoginSuccess = "Giriş yapıldı.";

        public const string GeneralSuccessfull = "İşlem başarılı.";
        public const string GeneralFailed = "İşlem başarısız.";

        public const string UserUpdated ="Kullanıcı güncellendi.";
        public const string UserUpdateFailed = "Kullanıcı güncellenemedi.";
        public const string UserRolesUpdateError = "Kullanıcı rolleri güncellenemedi.";
        public const string UserRolesUpdated = "Kullanıcı rolleri güncellendi.";
        public const string UserDeleted = "Kullanıcı silindi.";
        public const string UserDeleteFailed = "Kullanıcı silinemedi.";

        public const string PasswordResetMailSend ="Parola sıfırlama bağlantısı mail adresinize gönderildi.";
        public const string PasswordReseted ="Parola başarıyla değiştirildi.";
        public const string UserNotActivated = "Hesabınız aktif edilmemiş, E-mail adresinize gönderilen bağlantıdan aktifleştirebilirsiniz.";
        public const string UserActivated = "Hesap başarıyla aktif edildi.";
        public const string UserActivateFailed = "Hesap aktifleştirilemedi";

    }

}
