using System;
using System.Windows.Controls;
using MaterialsCalculator.Models;
using MaterialsCalculator.Windows;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace MaterialsCalculator.Pages
{
    /// <summary>
    /// Логика взаимодействия для AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {
        public User user;
        public AdminPage(User user)
        {
            InitializeComponent();
            this.user = user;   
            InitData();
        }
        void InitData()
        {
            // получаем текущее время
            DateTime time = DateTime.Now;
            // получаем который час
            int hour = time.Hour;
            // получаем минуты
            int minutes = time.Minute;
            string s = "Доброй ночи!";
            if ((hour >= 9) && (hour < 11) && (minutes >= 0) ||
            (hour == 11) && (minutes == 0))
            {
                s = "Доброе утро!";
            }
            else if ((hour >= 11) && (minutes > 0) && (hour < 18) || (hour == 18) && (minutes == 0))
            {
                s = "Добрый день!";
            }
            else if ((hour >= 18) && (minutes > 0) && (hour < 24) || (hour == 24) && (minutes == 0))
            {
                s = "Добрый вечер!";
            }
            TextBlockDayTime.Text = s;
            if (user.GenderID == 1)
            {
                TextBlockUserName.Text = $"Ms {user.FirstName} " +
                $"{user.MiddleName}";
            }
            else
            {
                TextBlockUserName.Text = $"Mr {user.FirstName} " +
                $"{user.MiddleName}";
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddMaterialWindow addWindow = new AddMaterialWindow();

            addWindow.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DeleteMaterialsWindow delWindow = new DeleteMaterialsWindow();

            delWindow.ShowDialog();
        }
    }
}
