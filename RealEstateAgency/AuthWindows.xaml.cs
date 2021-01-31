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
using System.Windows.Navigation;
using System.Windows.Shapes;
using RealEstateAgency.Classes;

namespace RealEstateAgency
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class AuthWindows : Window
    {
        public AuthWindows()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Properties.Settings.Default.User_AuthRemember == true)
            {
                tb_Login.Text = Properties.Settings.Default.User_AuthLogin;
                tb_Password.Password = Properties.Settings.Default.User_AuthPassword;
                cb_rememberPass.IsChecked = true;
            }
        }

        //Кнопка авторизации
        private void Btn_login_Click(object sender, RoutedEventArgs e)
        {
            if (App.bd.Auth(tb_Login.Text, tb_Password.Password) == true)
            {
                if (Properties.Settings.Default.User_AuthRemember == true) //если стоит галка о запоминание
                {
                    Properties.Settings.Default.User_AuthLogin = tb_Login.Text;
                    Properties.Settings.Default.User_AuthPassword = tb_Password.Password;
                    Properties.Settings.Default.Save();
                }

                MainMenuWindows MM = new MainMenuWindows();
                MM.Show();
                this.Hide();
            }
            else
            {
                tb_Login.BorderBrush = Brushes.Red;
                tb_Password.BorderBrush = Brushes.Red;
            }
        }

        //Для дизайна обводки texBox
        private void Tb_Login_TextChanged(object sender, TextChangedEventArgs e)
        {
            tb_Login.BorderBrush = Brushes.Gray;
        }
        private void Tb_Password_PasswordChanged(object sender, RoutedEventArgs e)
        {
            tb_Password.BorderBrush = Brushes.Gray;
        }

        //Запомнить меня?
        private void Cb_rememberPass_Checked(object sender, RoutedEventArgs e)
        {
            if(cb_rememberPass.IsChecked == true)
            {
                Properties.Settings.Default.User_AuthRemember = true;
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.User_AuthRemember = false;
                Properties.Settings.Default.Save();
            }
        }

        //Кнопка восстановления пароля
        private void Img_recover_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            AuthRecoverPasswordWindows RPW = new AuthRecoverPasswordWindows();

            if(RPW.ShowDialog() == true)
            {
                return;
            }
        }

        //Закрытие окна
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
