using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RealEstateAgency
{
    /// <summary>
    /// Логика взаимодействия для AuthRecoverPasswordWindows.xaml
    /// </summary>
    public partial class AuthRecoverPasswordWindows : Window
    {
        public AuthRecoverPasswordWindows()
        {
            InitializeComponent();
        }

        private void Btn_recover_Click(object sender, RoutedEventArgs e)
        {
            if(tb_Email.Text.Trim() == string.Empty)
            {
                tb_Email.BorderBrush = Brushes.Red;
                return;
            }

            string Email = tb_Email.Text.Trim();
           
            try
            {
                var mail = new System.Net.Mail.MailAddress(Email);
            }
            catch
            {
                MessageBox.Show("Похоже что вы ввели не email адрес", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Warning);
                tb_Email.BorderBrush = Brushes.Red;
                return;
            }

            if(App.bd.CheckUser(Email, Guid.Empty) == true)
            {
                if(App.bd.RecoverPassword(Email) == true)
                {
                    MessageBox.Show("Новый пароль отправлен Вам на почтовый ящик!", "Успешно!", MessageBoxButton.OK, MessageBoxImage.Information);
                    DialogResult = true;
                }
                else
                {
                    MessageBox.Show("Не удалось изменить пароль", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    tb_Email.BorderBrush = Brushes.Red;
                    return;
                }
            }
            else
            {
                MessageBox.Show("Увы, такого пользователя не существует", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                tb_Email.BorderBrush = Brushes.Red;
                return;
            }
        }

        private void Tb_Email_TextChanged(object sender, TextChangedEventArgs e)
        {
            tb_Email.BorderBrush = Brushes.Gray;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            lbl_version.Text = App.bd.GetVersion();
        }
    }
}
