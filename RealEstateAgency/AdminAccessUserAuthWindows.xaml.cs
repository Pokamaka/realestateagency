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
    /// Логика взаимодействия для AdminAccessUserAuthWindows.xaml
    /// </summary>
    public partial class AdminAccessUserAuthWindows : Window
    {
        Authorization user = new Authorization();
        public AdminAccessUserAuthWindows(Authorization _user)
        {
            InitializeComponent();
            user = _user;
            DataContext = user;
        }

        //кнопка сохранения
        private void Btn_GetAccess_Click(object sender, RoutedEventArgs e)
        {
            if(tb_UserLogin.Text.Trim() == string.Empty || btn_SelectUser.Text.Trim() == string.Empty)
            {
                if(user.Email == null)
                {
                    MessageBox.Show("У пользователя не подвязан Email", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                MessageBox.Show("Заполните все обязательные поля!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            DialogResult = true;
        }

        //кнопка выбор человека
        private void BtnSelectUser_Click(object sender, RoutedEventArgs e)
        {
            App.open_InCreateWindows = true;
            AccountsBook AB = new AccountsBook();
            if (AB.ShowDialog() == true)
            {
                if(App.bd.CheckUser(string.Empty, AB.selectAccount.ID) == true)
                {
                    MessageBox.Show("У данного пользователя уже есть права для авторизации", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }         

                user.UserID = AB.selectAccount.ID;
                user.FName = AB.selectAccount.FName;
                user.Name = AB.selectAccount.Name;
                user.Email = AB.selectAccount.Email;
                btn_SelectUser.Text = user.FName;
                App.open_InCreateWindows = false;
            }
        }
    }
}
