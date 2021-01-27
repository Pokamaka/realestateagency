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
    /// Логика взаимодействия для MainMenu.xaml
    /// </summary>
    public partial class MainMenuWindows : Window
    {
        public MainMenuWindows()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            tb_UserSName.Text = Properties.Settings.Default.User_SName;
            if (Properties.Settings.Default.User_RoleName == "Разработчик")
            {
                btn_adminka.Visibility = Visibility.Visible;
            }
        }

        //Кнопка "Клиенты"
        private void Btn_clients_Click(object sender, RoutedEventArgs e)
        {
            AccountsBook AB = new AccountsBook();
            if(AB.ShowDialog() == true)
            {
                return;
            }
        }

        //Кнопка "Объекты"
        private void Btn_object_Click(object sender, RoutedEventArgs e)
        {
            ObjectsBookWindows OB = new ObjectsBookWindows();
            if (OB.ShowDialog() == true)
            {
                return;
            }
        }

        //Кнопка "Сделки"
        private void Btn_deals_Click(object sender, RoutedEventArgs e)
        {
            DealsWindows DW = new DealsWindows();
            if (DW.ShowDialog() == true)
            {
                return;
            }
        }

        //Кнопка "Статистика"
        private void Btn_stats_Click(object sender, RoutedEventArgs e)
        {

        }

        //Кнопка "Выход" это назад на форму авторизации
        private void Btn_exit_Click(object sender, RoutedEventArgs e)
        {
            AuthWindows AW = new AuthWindows();
            AW.Show();
            this.Hide();
        }

        //обработка закрытия приложение -> сразу проходит де авторизация
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
