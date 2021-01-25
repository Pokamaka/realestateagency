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
    /// Логика взаимодействия для ObjectWindows.xaml
    /// </summary>
    public partial class ObjectWindows : Window
    {
        Objects obj = new Objects();
        public ObjectWindows(Objects _obj)
        {
            InitializeComponent();
            obj = _obj;
            DataContext = obj;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cb_district.ItemsSource = App.bd.GetDistrict();
        }

        //выбор владельца
        private void BtnSelectOwner_Click(object sender, RoutedEventArgs e)
        {
            App.open_InCreateWindows = true;
            AccountsBook AB = new AccountsBook();
            if (AB.ShowDialog() == true)
            {
                obj.OwnerID = AB.selectAccount.ID;
                obj.OwnerName = AB.selectAccount.FName;
                btn_SelectOwner.Text = obj.OwnerName;
                App.open_InCreateWindows = false;
            }

        }

        private void Btn_save_Click(object sender, RoutedEventArgs e)
        {
            if (tb_adress.Text.Trim() == string.Empty || tb_countRoom.Text.Trim() == string.Empty || tb_Floor.Text.Trim() == string.Empty || tb_name.Text.Trim() == string.Empty || tb_note.Text.Trim() == string.Empty || tb_SObject.Text.Trim() == string.Empty || btn_SelectOwner.Text.Trim() == string.Empty)
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
