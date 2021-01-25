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
    /// Логика взаимодействия для AccountsBook.xaml
    /// </summary>
    public partial class AccountsBook : Window
    {
        ObservableCollection<Accounts> accountsCollection;
        public Accounts selectAccount;

        public AccountsBook()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            accountsCollection = new ObservableCollection<Accounts>(App.bd.GetAccounts());
            grid_UserAcc.ItemsSource = accountsCollection;
        }

        //кнопка изменения
        private void Btn_UserEdite_Click(object sender, RoutedEventArgs e)
        {
            selectAccount = (Accounts)grid_UserAcc.SelectedItem;
            if (selectAccount != null)
            {
                AccountWindows AW = new AccountWindows(selectAccount);
                if (AW.ShowDialog() == true)
                {
                    if (App.bd.EditUser(selectAccount) == false)
                    {
                        return;
                    }
                    UpdateData(selectAccount);
                }
            }
        }

        //кнопка создания
        private void Btn_UserCreate_Click(object sender, RoutedEventArgs e)
        {
           //selectAccount = (Accounts)grid_UserAcc.SelectedItem;
            Accounts newAcc = new Accounts();
            #region Поля для аккаута
            newAcc.ID = Guid.NewGuid();
            newAcc.DateBrth = DateTime.Now;

            if (Properties.Settings.Default.User_RoleName == "Агент" || Properties.Settings.Default.User_RoleName == "Разработчик")
            {
                newAcc.UserRole = Guid.Parse("7BB39BE3-8866-46A2-A38D-FB1EAE13531E");
            }

            #endregion

            AccountWindows AW = new AccountWindows(newAcc);
            if(AW.ShowDialog() == true)
            {
                if (App.bd.CreateUser(newAcc) == false)
                {
                    return;
                }
                UpdateData(newAcc);
            }
        }

        /// <summary>
        /// Функция обновления данных в таблице
        /// </summary>
        /// <param name="desc">Запись нового пользователя</param>
        private void UpdateData(Accounts acc)
        {
            accountsCollection.Clear();
            foreach (Accounts curItem in App.bd.GetAccounts())
            {
                accountsCollection.Add(curItem);
            }
            Accounts selectRec = accountsCollection.Where(r => r.ID == acc.ID).FirstOrDefault();
            grid_UserAcc.CurrentItem = selectRec;
            grid_UserAcc.SelectedItem = selectRec;
        }

        //выбор человека если открыто из окна создания объекта или сделки 
        private void Grid_UserAcc_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            selectAccount = (Accounts)grid_UserAcc.SelectedItem;
            if (App.open_InCreateWindows == true && selectAccount != null)
            {
                DialogResult = true;
            }
        }
    }
}
