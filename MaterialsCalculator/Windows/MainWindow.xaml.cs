using MaterialsCalculator.Models;
using MaterialsCalculator.Pages;
using MaterialsCalculator.Windows;
using System;
using System.Windows;

namespace MaterialsCalculator
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        User _currentUser; //текущий пользователь в системе
        public MainWindow()
        {
            InitializeComponent();
            LoadData();
        }
        /// <summary>
        /// загружаем данные и инициализируем переменные
        /// </summary>
        void LoadData()
        {
            MainFrame.Navigate(new MaterialTypesPage());
            Manager.MainFrame = MainFrame;
        }
        /*void UpdateData(Object o)
        {
            Manager.MainFrame = o;
        }*/
        private void WindowClosed(object sender, EventArgs e)
        {
            // показать владельца формы
            //Owner.Show();
            // или если надо, то можно закрыть приложение командой
            Application.Current.Shutdown();
        }

        //событие попытки закрытия окна,
        // если пользователь выберет Cancel, то форму не закроем
        private void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult x = MessageBox.Show("Вы действительно хотите выйти?", "Выйти",
            MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (x == MessageBoxResult.Cancel)
                e.Cancel = true;
        }
        //авторизация
        private void BtnEnter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AutorizationWindow window = new AutorizationWindow();
                // открытие формы в режиме модального окна

                if (window.ShowDialog() == true)
                {
                    // сюда попадем, если пользователь введёт корректные учётные данные
                    // получаем RoleId пользователя
                    _currentUser = Manager.CurrentUser;
                    int role = Convert.ToInt32(_currentUser.RoleID);
                    if (role == 2) //если 1, то участник и открываем окно участника
                    {
                        UsersPage usersPage = new UsersPage(_currentUser);
                        Manager.MainFrame.Navigate(usersPage);
                    }
                    if (role == 1) //если 2, то модератор и открываем окно модератора
                    {
                        AdminPage adminPage = new AdminPage(_currentUser);
                        Manager.MainFrame.Navigate(adminPage);
                    }

                }
            }
            catch
            {
                MessageBox.Show("Ошибка");
            }
        }

        private void BtnHome_Click(object sender, RoutedEventArgs e)
        {
            this.LoadData();
        }

        private void BtnUser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
            int role = Convert.ToInt32(_currentUser.RoleID);
                if (role == 2) //если 1, то участник и открываем окно участника
                {
                    UsersPage usersPage = new UsersPage(_currentUser);
                    Manager.MainFrame.Navigate(usersPage);
                }
                if (role == 1) //если 2, то модератор и открываем окно модератора
                {
                    AdminPage adminPage = new AdminPage(_currentUser);
                    Manager.MainFrame.Navigate(adminPage);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Авторизируйтесь, пожалуйста");
            }
        }
    }
}