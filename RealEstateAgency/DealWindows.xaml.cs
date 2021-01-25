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
    /// Логика взаимодействия для DealWindows.xaml
    /// </summary>
    public partial class DealWindows : Window
    {
        Deals deal = new Deals();
        public DealWindows(Deals _deal)
        {
            InitializeComponent();
            deal = _deal;
            DataContext = deal;
        }

        //выбор клиента
        private void BtnSelectClient_Click(object sender, RoutedEventArgs e)
        {
            App.open_InCreateWindows = true;
            AccountsBook AB = new AccountsBook();
            if (AB.ShowDialog() == true)
            {
                deal.UserID = AB.selectAccount.ID;
                deal.ClientFName = AB.selectAccount.FName;
                btn_SelectClient.Text = deal.ClientFName;
                App.open_InCreateWindows = false;
            }
        }

        //выбор объекта
        private void BtnSelectObject_Click(object sender, RoutedEventArgs e)
        {
            App.open_InCreateWindows = true;
            ObjectsBookWindows OB = new ObjectsBookWindows();
            if (OB.ShowDialog() == true)
            {
                deal.ObjectID = OB.selectObject.ID;
                deal.ObjectName = OB.selectObject.NameObj;
                btn_SelectObject.Text = deal.ObjectName;
                App.open_InCreateWindows = false;
            }
        }

        private void Btn_save_Click(object sender, RoutedEventArgs e)
        {
            if (btn_SelectObject.Text.Trim() == string.Empty || tb_procent.Text.Trim() == string.Empty || tb_summ.Text.Trim() == string.Empty || btn_SelectClient.Text.Trim() == string.Empty)
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
