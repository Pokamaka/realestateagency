using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Web;

namespace RealEstateAgency.Classes
{
    public class SendMail
    {
        /// <summary>
        /// Функция отправки сообщение с восстановлением пароля
        /// </summary>
        /// <param name="SendAdrees">Принимает адрес получателя</param>
        /// <param name="NewPassword">Принимает новый пароль</param>
        /// <returns>Возращает True - сообщение успешно отправлено, False - сообщенеие не отправлено</returns>
        public bool SendAuthMail(string SendAdrees, string NewPassword)
        {
            try
            {
                MailAddress from = new MailAddress("auth@pokama.ru", "Восстановления пароля");
                MailAddress to = new MailAddress(SendAdrees);
                MailMessage m = new MailMessage(from, to); //сам объект сообщения

                m.Subject = "Восстановление пароля"; // Тема письма
                m.Body = $"Ваш старый пароль сброшен. Чтобы авторизоваться в системе используйте новый пароль. <br/ > Ваш новый пароль - <b>{NewPassword}</b><br/ > Если Вы не меняли пароль, срочно обратитесь к вашему локальному адимнистратору!<br /> Данное письмо было сгенерированно автоматически и не нуждается в ответе.";

                m.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient("mail.hosting.reg.ru", 25);

                smtp.Credentials = new NetworkCredential("auth@pokama.ru", "Skazka123");
                smtp.EnableSsl = true;
                smtp.Send(m);
                return true;
            }
            catch
            {
                return false;
            }   
        }

        /// <summary>
        /// Функция отправки потверждаюшего письма 
        /// </summary>
        /// <param name="SendAdrees">Принимает адрес получателя</param>
        /// <param name="Login">Принимает логин для входа в систему</param>
        /// <param name="Password">Принимает новый пароль</param>
        /// <param name="UserName">Принимает имя пользователя</param>
        /// <returns>Возращает True - сообщение успешно отправлено, False - сообщенеие не отправлено</returns>
        public bool SendAccessMail(string SendAdrees, string Login, string Password, string UserName)
        {
            try
            {
                MailAddress from = new MailAddress("auth@pokama.ru", "Предоставление доступа");
                MailAddress to = new MailAddress(SendAdrees);
                MailMessage m = new MailMessage(from, to); //сам объект сообщения

                m.Subject = "Данные для авторизации в системе"; // Тема письма
                m.Priority = MailPriority.High;
                m.Body = $"Здраствуйте, {UserName}!<br/> В этом письме Вы найдете все необходимые данные для авторизации в системе.<br/> Ваш логин: <b>{Login}</b><br/> Ваш пароль: <b>{Password}</b><br/><br/> Данное письмо было сгенерированно автоматически и не ожидает ответа на него.<br/> Удачи =)";
                m.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient("mail.hosting.reg.ru", 25);

                smtp.Credentials = new NetworkCredential("auth@pokama.ru", "Skazka123");
                smtp.EnableSsl = true;
                smtp.Send(m);
                return true;
            }
            catch 
            {
                return false;
            }
        }
        
    }
}
