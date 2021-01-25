using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для DealsWindows.xaml
    /// </summary>
    public partial class DealsWindows : Window
    {
        ObservableCollection<Deals> dealsCollection;
        Deals selectDeal;

        public DealsWindows()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dealsCollection = new ObservableCollection<Deals>(App.bd.GetDeals());
            grid_deals.ItemsSource = dealsCollection;
        }

        //Кнопка создания сделки
        private void Btn_DealCreate_Click(object sender, RoutedEventArgs e)
        {
            Deals deal = new Deals();
            deal.ID = Guid.NewGuid();
            deal.AgentID = Properties.Settings.Default.User_ID;

            DealWindows DW = new DealWindows(deal);
            if (DW.ShowDialog() == true)
            {
                if (App.bd.CreateDeal(deal) == false)
                {
                    return;
                }
                UpdateData(deal);
            }
        }

        //Кнопка редактирования сделки
        private void Btn_DealEdite_Click(object sender, RoutedEventArgs e)
        {
            selectDeal = (Deals)grid_deals.SelectedItem;
            if(selectDeal != null && selectDeal.StatusName == "В работе")
            {
                DealWindows DW = new DealWindows(selectDeal);
                if (DW.ShowDialog() == true)
                {
                    if (App.bd.EditDeal(selectDeal) == false)
                    {
                        return;
                    }
                    UpdateData(selectDeal);
                }
            }
            else
            {
                if(selectDeal != null)
                {
                    MessageBox.Show($"Сделка №{selectDeal.Number} уже завершена!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                }   
            }
           
        }

        private void Grid_deals_SelectedItemChanged(object sender, DevExpress.Xpf.Grid.SelectedItemChangedEventArgs e)
        {

        }

        //Кнопка завершения сделки
        private void Btn_DealStop_Click(object sender, RoutedEventArgs e)
        {
            selectDeal = (Deals)grid_deals.SelectedItem;
            if (selectDeal != null)
            {
                MessageBoxResult result = MessageBox.Show($"Вы уверены что хотите завершить сделку №{selectDeal.Number}", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    if (App.bd.StopDeal(selectDeal) == false)
                    {
                        return;
                    }
                    UpdateData(selectDeal);
                }
            }
        }

        //Кнопка отмены сделки
        private void Btn_DealCancel_Click(object sender, RoutedEventArgs e)
        {
            selectDeal = (Deals)grid_deals.SelectedItem;
            if (selectDeal != null)
            {
                MessageBoxResult result = MessageBox.Show($"Вы уверены что хотите отменить сделку №{selectDeal.Number}", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    if (App.bd.CancelDeal(selectDeal) == false)
                    {
                        return;
                    }
                    UpdateData(selectDeal);
                }
            }
        }

        /// <summary>
        /// Функция обновления данных в таблице
        /// </summary>
        /// <param name="desc">Запись новой сделки</param>
        private void UpdateData(Deals acc)
        {
            dealsCollection.Clear();
            foreach (Deals curItem in App.bd.GetDeals())
            {
                dealsCollection.Add(curItem);
            }
            Deals selectRec = dealsCollection.Where(r => r.ID == acc.ID).FirstOrDefault();
            grid_deals.CurrentItem = selectRec;
            grid_deals.SelectedItem = selectRec;
        }

      
    }
}
