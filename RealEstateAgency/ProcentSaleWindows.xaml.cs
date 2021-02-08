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
    /// Логика взаимодействия для ProcentSaleWindows.xaml
    /// </summary>
    public partial class ProcentSaleWindows : Window
    {
        Objects obj = new Objects();
        public ProcentSaleWindows(Objects _obj)
        {
            InitializeComponent();
            obj = _obj;
            DataContext = obj;
        }

        private void Btn_save_Click(object sender, RoutedEventArgs e)
        {
            if (tb_procent.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Введите корректный процент скидки", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            DialogResult = true;
        }

        private void Tb_procent_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                obj.SaleCost = obj.Coast - (obj.Coast / 100 * int.Parse(tb_procent.Text));
                tb_summ.Text = obj.SaleCost.ToString();
            }
            catch
            {
                MessageBox.Show("Введите корректный процент скидки", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
