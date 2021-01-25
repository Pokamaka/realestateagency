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
    /// Логика взаимодействия для Account.xaml
    /// </summary>
    public partial class AccountWindows : Window
    {
        public AccountWindows(Accounts account)
        {
            InitializeComponent();
            DataContext = account;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cb_UserRole.ItemsSource = App.bd.GetRole();
            if (Properties.Settings.Default.User_RoleName == "Агент")
            {
                cb_UserRole.IsEnabled = false;
            }
        }

        private void Btn_UserSave_Click(object sender, RoutedEventArgs e)
        {
            if (tb_UserName.Text.Trim() == string.Empty || tb_UserSurname.Text.Trim() == string.Empty || tb_UserPatronic.Text.Trim() == string.Empty || tb_UserPassport.Text.Trim() == string.Empty || tb_UserMobilPhone.Text.Trim() == string.Empty || tb_UserEmail.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Обязательные поля остались не заполнеными!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                DialogResult = true;
            }
        }
  
    }
}
