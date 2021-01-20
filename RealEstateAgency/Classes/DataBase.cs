using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RealEstateAgency.Classes
{
    /// <summary>
    /// Класс для работы с БД
    /// </summary>
    public class DataBase
    { 
        Guid PokamaGuid = new Guid("F09F642A-9247-4B0D-A1CC-A39148EC5E59"); //GUID моего аккаута
        Guid UserStatus_Active = new Guid("301C9782-D208-42CD-862C-6F2D30C46E0E"); //GUID пользовательского статуса - активирован

        /// <summary>
        /// Функция авторизации пользователя.
        /// Условия для авторизации: совпадение пароля и логина + активированый аккаунт
        /// pokama - test123
        /// </summary>
        /// <param name="Login">Принимает логин пользователя</param>
        /// <param name="Password">Принимает пароль пользователя</param>
        /// <returns>Возращет True - авторизован, False - не авторизован</returns>
        public bool Auth(string Login, string Password)
        {
            using (RealEstateAgencyEntities db = new RealEstateAgencyEntities())
            {
                byte[] bytes = Encoding.Unicode.GetBytes(Password);
                MD5CryptoServiceProvider CSP = new MD5CryptoServiceProvider();

                byte[] byteHash = CSP.ComputeHash(bytes);
                string hash = string.Empty;

                foreach (byte b in byteHash)
                {
                    hash += string.Format("{0:x2}", b);
                }
                Guid enterPass = new Guid(hash);

                try
                {
                    Authorization account = db.Authorization.Where(x => x.Login == Login && x.Password == enterPass).FirstOrDefault();
                    Accounts check = db.Accounts.Where(x => x.ID == account.UserID && x.UserStatus == UserStatus_Active).FirstOrDefault();
                    if (account == null || check == null)
                    {
                        MessageBox.Show("Вы ввели не правильные данные для входа!", "Ошибка авторизации", MessageBoxButton.OK, MessageBoxImage.Error);
                        return false;
                    }
                }
                catch
                {
                    MessageBox.Show("Вы ввели не правильные данные для входа!", "Ошибка авторизации", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                return true;
            }
        }
    }
}
