using MaterialsCalculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Threading;

namespace MaterialsCalculator.Windows
{
    /// <summary>
    /// Логика взаимодействия для AutorizationWindow.xaml
    /// </summary>
    public partial class AutorizationWindow : Window
    {
        // Переменная флаг, меняет свое значение,
        // если пользователь не смог ввести с первого раза пароль и логин
        // если b == true, то отобразим капчу
        bool b = false;
        int count = 0;
        // таймер
        DispatcherTimer timer = new DispatcherTimer();
        int seconds = 0; // секунды
        string captcha = ""; // текст капчи
        public AutorizationWindow()
        {
            InitializeComponent();
            LoadAndInitData();
        }
        /// <summary>
        /// Загружает и инициализирует данные
        /// </summary>
        void LoadAndInitData()
        {
            this.Height = 200; // задаем высоту окна
            timer.Tick += timer_Tick; // к событию Tick таймера привязываем обработчик события
                                      // скрываем строки с капчой
            RowCaptcha1.Height = new GridLength(0);
            RowCaptcha2.Height = new GridLength(0);
            //TbLogin.Text = "loginDEpxl2018";
            //TbPass.Password = "P6h4Jq";
        }
        // обаботчик события срабатывает через каждые т секунд
        void timer_Tick(object sender, EventArgs e)
        {
            seconds -= 1;
            TextBlockTime.Text = $"Осталось {seconds} секунд до разблокировки";
            if (seconds == 0) // оставливаем таймер, разблокировываем кнопку
            {
                BtnOk.IsEnabled = true;
                TextBlockTime.Text = "";
                timer.Stop();
            }
        }

        private void BtnOkClick(object sender, RoutedEventArgs e)
        {
            try
            { //загрузка всех пользователей из БД в список
                List<User> users = MaterialsCalculatorEntities.GetContext().Users.ToList();
                //попытка найти пользователя с указанным паролем и логином
                //если такого пользователя не будет обнаружено то переменная u будет равна null
                User user = users.FirstOrDefault(p => p.Password == TbPass.Password
                && p.Email.ToString() == TbLogin.Text);
                // удачный вход после ввода пароля и логина или пароля, логина и капчи
                if ((user != null && !b) || (user != null && b && TbCaptcha.Text == captcha))
                {
                    Manager.CurrentUser = user;
                    // логин и пароль корректные запускаем главную форму приложения
                    DialogResult = true;
                }
                else
                {
                    MessageBox.Show("Не верный логин или пароль");
                    count++;
                    if (count >= 3)
                    {
                        // меняем высоту формы
                        this.Height = 350;
                        // вызываем метод отображения капчи
                        ShowCaptcha();
                        // задаем параметры таймера, событие Tick срабатывает через каждую секунду
                        timer.Interval = TimeSpan.FromSeconds(1);
                        // блокируем кнопку
                        BtnOk.IsEnabled = false;
                        // на 10 секунд
                        seconds = 10;
                        // отображает сколько секунд осталось до разблокировки
                        TextBlockTime.Text = $"Осталось {seconds} секунд до разблокировки";
                        // включаем таймер
                        timer.Start();
                        RowCaptcha1.Height = new GridLength(34);
                        RowCaptcha2.Height = new GridLength(1, GridUnitType.Star);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        //код кнопки Cancel
        private void BtnCancelClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }
        // попытка закрыть приложение

        private void BtnRenewCaptcha_Click(object sender, RoutedEventArgs e)
        {
            // кнопка обновить капчу
            ShowCaptcha();
        }
        /// <summary>
        /// отображает капчу
        /// </summary>
        void ShowCaptcha()
        {
            // из класса MakeCaptcha вызываем метод CreateImage
            var tuple = MakeCaptcha.CreateImage(300, 100, 4);
            // получаем ImageSource
            ImageCaptcha.Source = tuple.image;
            // получаем текст капчи
            captcha = tuple.captcha;
        }


        private void BtnRegistration_Click(object sender, RoutedEventArgs e)
        {
            RegistrationWindow regWindow = new RegistrationWindow();
            this.Close();

            regWindow.ShowDialog();
        }
    }
}
