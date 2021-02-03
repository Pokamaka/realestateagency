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
    /// Логика взаимодействия для AdminVersionEditWindows.xaml
    /// </summary>
    public partial class AdminVersionEditWindows : Window
    {
        public AdminVersionEditWindows(ProgrammVersion version)
        {
            InitializeComponent();
            DataContext = version;
        }

        private void Btn_save_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы уверены что хотите изменить версию программы?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if(result == MessageBoxResult.Yes)
            {
                DialogResult = true;
            }
            else
            {
                return;
            }
        }
    }
}
