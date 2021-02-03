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
    /// Логика взаимодействия для AdminMenuWindows.xaml
    /// </summary>
    public partial class AdminMenuWindows : Window
    {
        public AdminMenuWindows()
        {
            InitializeComponent();
        }

        //Выдача доступа к приложению
        private void Btn_GetAccess_Click(object sender, RoutedEventArgs e)
        {
            Authorization user = new Authorization();
            user.ID = Guid.NewGuid();
            AdminAccessUserAuthWindows AAUAW = new AdminAccessUserAuthWindows(user);
            if (AAUAW.ShowDialog() == true)
            {
                if(App.bd.GetAccess(user) == true)
                {
                    MessageBox.Show($"Вы успешно выдали права на запуск пользователю - {user.FName}", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Environment.Exit(0);
        }

        private void Btn_EditVersion_Click(object sender, RoutedEventArgs e)
        {
            ProgrammVersion ver = App.bd.Version();
            AdminVersionEditWindows AVEW = new AdminVersionEditWindows(ver);
            if (AVEW.ShowDialog() == true)
            {
                if (App.bd.EditVersion(ver) == true)
                {
                    return;
                }
            }
        }
    }
}
