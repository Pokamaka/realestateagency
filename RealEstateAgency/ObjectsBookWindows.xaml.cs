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
    /// Логика взаимодействия для ObjectsBookWindows.xaml
    /// </summary>
    public partial class ObjectsBookWindows : Window
    {
        ObservableCollection<Objects> objectCollection;
        public Objects selectObject;

        public ObjectsBookWindows()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            objectCollection = new ObservableCollection<Objects>(App.bd.GetObject());
            grid_objects.ItemsSource = objectCollection;
        }

        //Создание нового объекта
        private void Btn_ObjectCreate_Click(object sender, RoutedEventArgs e)
        {
            Objects obj = new Objects();

            obj.ID = Guid.NewGuid();
            obj.Status = new Guid("2692C961-ACD3-4516-A0E3-C79B89E428A2");
            obj.AgentID = Properties.Settings.Default.User_ID;

            ObjectWindows OW = new ObjectWindows(obj);

            if (OW.ShowDialog() == true)
            {
                if(App.bd.CreateObject(obj) == false)
                {
                    return;
                }
                UpdateData(obj);
            }
        }

        //Изменение объекта
        private void Btn_ObjectEdite_Click(object sender, RoutedEventArgs e)
        {
            selectObject = (Objects)grid_objects.SelectedItem;
            if(selectObject != null)
            {
                ObjectWindows OW = new ObjectWindows(selectObject);

                if (OW.ShowDialog() == true)
                {
                    if (App.bd.EditObject(selectObject) == false)
                    {
                        return;
                    }
                    UpdateData(selectObject);
                }
            }
        }

        //выбор объекта если открыто из окна создания сделки 
        private void Grid_objects_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            selectObject = (Objects)grid_objects.SelectedItem;
            if (App.open_InCreateWindows == true && selectObject != null)
            {
                DialogResult = true;
            }
        }

        /// <summary>
        /// Функция обновления данных в гриде
        /// </summary>
        /// <param name="desc">Запись нового объекта</param>
        private void UpdateData(Objects obj)
        {
            objectCollection.Clear();
            foreach (Objects curItem in App.bd.GetObject())
            {
                objectCollection.Add(curItem);
            }
            Objects selectRec = objectCollection.Where(r => r.ID == obj.ID).FirstOrDefault();
            grid_objects.CurrentItem = selectRec;
            grid_objects.SelectedItem = selectRec;
        }

        //Кнопка создания скидки
        private void Btn_Sale_Click(object sender, RoutedEventArgs e)
        {
            selectObject = (Objects)grid_objects.SelectedItem;
            if (selectObject != null)
            {
                ProcentSaleWindows PSW = new ProcentSaleWindows(selectObject);
                if (PSW.ShowDialog() == true)
                {
                    if (App.bd.CreateSale(selectObject) == true)
                    {
                        UpdateData(selectObject);
                    }
                    return;
                }
            }
        }
    }
}
