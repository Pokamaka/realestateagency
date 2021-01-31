using RealEstateAgency.Classes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace RealEstateAgency
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static DataBase bd { get; } = new DataBase();
        public static bool open_InCreateWindows { get; set; }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            using (RealEstateAgencyEntities db = new RealEstateAgencyEntities())
            {
                try
                {
                    db.Database.Exists();
                }
                catch
                {
                    MessageBox.Show("К сожалению не удалось установить устойчевое соединение с сервером. Проверьте Ваше интернет подключение", "Проверьте интернет соединение", MessageBoxButton.OK, MessageBoxImage.Error);
                    Environment.Exit(0);
                }
               
            }
        }
    }
}
