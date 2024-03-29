﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Contents
{
    public class Messages
    {
        //Contact Messages:
        public static string ContactAddSuccess = "Kişi rehbere başarıyla eklendi.";
        public static string ContactAddFail = "Rehbere kişi eklenme işlemi başarısız oldu.";
        public static string ContactDeleteSuccess = "Kişi rehberden başarıyla silindi.";
        public static string ContactsDeleteSuccess = "Kişiler rehberden başarıyla silindi.";
        public static string ContactDeleteFail = "Rehberden kişi silme işlemi başarısız oldu.";
        public static string ContactUpdateSuccess = "Kişi rehberde başarıyla güncellendi.";
        public static string ContactUpdateFail = "Rehberde kişi güncelleme işlemi başarısız oldu.";
        public static string ContactGetFail = "Kişi bulunamadı.";
        public static string ContactGetListFail = "Bu kullanıcıya ait kişiler bulunamadı.";
        public static string ContactAlreadyExist = "Eklemeye çalıştığınız numara kayıtlı bir kullanıcıya ait.";
        //User Messages:
        public static string UserAddSuccess = "Kullanıcı başarıyla eklendi.";
        public static string UserAddFail = "Kullanıcı ekleme işlemi başarısız oldu.";
        public static string UserDeleteSuccess = "Kullanıcı başarıyla silindi.";
        public static string UsersDeleteSuccess = "Kullanıcılar başarıyla silindi";
        public static string UserDeleteFail = "Kullanıcı silme işlemi başarısız oldu.";
        public static string UserUpdateSuccess = "Kullanıcı başarıyla güncellendi.";
        public static string UserUpdateFail = "Kullanıcı güncelleme işlemi başarısız oldu.";
        public static string UserGetFail = "Kullanıcı bulunamadı.";
        public static string UserTelAlreadyExist = "Bu numaraya sahip kullanıcı bulunmaktadır.";
        //Group Messages:
        public static string GroupAddSuccess = "Grup başarıyla eklendi.";
        public static string GroupAddFail = "Grup ekleme işlemi başarısız oldu.";
        public static string GroupDeleteSuccess = "Grup başarıyla silindi.";
        public static string GroupsDeleteSuccess = "Gruplar başarıyla silindi.";
        public static string GroupDeleteFail = "Grup silme işlemi başarısız oldu.";
        public static string GroupUpdateSuccess = "Grup başarıyla güncellendi.";
        public static string GroupUpdateFail = "Grup güncelleme işlemi başarısız oldu.";
        public static string GroupGetFail = "Grup bulunamadı.";
        public static string GroupAlreadyExists = "Eklemeye çalıştığınız grup ile aynı isimde başka bir kayıt var.";
        //Group Contact Messages:
        public static string GroupContactAddSuccess = "Kişi gruba başarıyla eklendi.";
        public static string GroupContactAddFail = "Kişi gruba eklenemedi.";
        public static string GroupContactDeleteSuccess = "Kişi gruptan başarıyla silindi.";
        public static string GroupContactDeleteFail = "Kişi gruptan silinemedi.";
        public static string GetGroupsOfAContactFail = "Kişinin bulunduğu gruplar bulunamadı.";
        public static string GetContactsOfAGroupFail = "Grupta kişi bulunamadı.";
        public static string ContactAlreadyExistInGroup = "Kişi zaten eklediğiniz gruptadır.";
        //Login Messages:
        public static string UserNotFound = "Kullanıcı bulunamadı.";
        public static string WrongPassword = "Girdiğiniz şifre hatalıdır.";
        public static string SuccessLogin = "Giriş başarılı.";
        //Register Messages:
        public static string UserAlreadyExists = "Bu email kullanılmaktadır.";
        public static string UserRegistered = "Kullanıcı sisteme başarıyla kayıt oldu.";
        //Password Reset Messages:
        public static string PasswordsNotMatched = "Kullanıcının girdiği şifre geçerli şifresi ile eşleşmiyor.";
        public static string ResetSuccess = "Kullanıcının şifresi değiştirildi.";
        public static string ResetFail = "Kullanıcının şifresinin değiştirilmesi sırasında bir hata meydana geldi.";
        public static string SamePasswordFail = "Kullanıcının yeni şifresi eski şifre ile aynı.";
        public static string UserNotificationFail = "Kullanıcıya şifre değiştirme kodu gönderilemedi.";
        //Token Messages:
        public static string AccessTokenCreated = "Access Token Oluşturuldu.";
        public static string AccessTokenNotCreated = "Access Token Oluşturulamadı.";
    }
}
