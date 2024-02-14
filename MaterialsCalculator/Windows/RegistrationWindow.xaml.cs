 using MaterialsCalculator.Models;
using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using MaterialsCalculator.Pages;
using MaterialsCalculator.Windows;

namespace MaterialsCalculator.Windows
{
    /// <summary>
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        // создаваемый новый пользователь
        private User _currentUser = new User();
        
        public RegistrationWindow()
        {
            InitializeComponent();
            LoadAndInitData();
        }
        // загрузка и задание начальных значений полям
        void LoadAndInitData()
        {
            // формируем новый IdNumber нового пользоватля
            // получам количество пользователей
            int k = MaterialsCalculatorEntities.GetContext().Users.Count();
            int numberid = 1;
            if (k > 0)
                numberid = Convert.ToInt32(MaterialsCalculatorEntities.GetContext().Users.
                Max(x => x.UserID)) + 1;

        this.DataContext = _currentUser;

        }
        bool CheckPassword(string password)
        {
            // проверка длины пароля
            if (password.Length < 6)
                return false;
            // проверка заглавных и строчных букв
            if (password.ToLower() == password || password.ToUpper() == password)
                return false;
            int count = 0;
            int spec = 0;
            for (int i = 0; i < password.Length; i++)
            {
                // подсчёт цифр
                if (char.IsDigit(password[i]))
                    count++;
                // подсчёт спецсимволов
                if (!char.IsLetterOrDigit(password[i]))
                    spec++;
            }
            //MessageBox.Show($"цифр {count} спец {spec}");
            // проверка не менее одного спецсимвола и не менее одной цифры
            if (count == 0 || spec == 0)
                return false;
            return true;
        }

        /// <summary>
        /// Проверка полей ввод на корректыне данные
        /// </summary>
        /// <returns>текст StringBuilder содержащий ошибки, если они есть</returns>
        private StringBuilder CheckFields()
        {
            StringBuilder s = new StringBuilder();
            // проверка полей на содержимое
            if (string.IsNullOrWhiteSpace(TextBoxLastName.Text))
                s.AppendLine("Укажите Фамилию");
            if (string.IsNullOrWhiteSpace(TextBoxFirstName.Text))
                s.AppendLine("Укажите Имя");          
            if (ComboBoxGender.SelectedIndex == -1)
                s.AppendLine("Выберите пол");           

            if (string.IsNullOrWhiteSpace(TextBoxEmail.Text))
                s.AppendLine("Укажите Email");
            // проверка пароля
            if (string.IsNullOrWhiteSpace(PasswordBoxFirst.Password) &&
            string.IsNullOrWhiteSpace(TextBoxPasswordFirst.Text))
                s.AppendLine("Укажите пароль");
            else
            {
                    if (PasswordBoxFirst.Password != PasswordBoxAgain.Password)
                        s.AppendLine("Пароли отличаются");
                    else
                    if (!CheckPassword(PasswordBoxFirst.Password))
                        s.AppendLine("Пароль должен соотвествовать требованиям:\n - не менее 6 символов; " +
                "\n - заглавные и строчные буквы;\n - не менее одного спецсимвола;\n - не менее одной цифры.");   
            }
            return s;
        }
        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder _error = CheckFields();
            // если ошибки есть, то выводим ошибки в MessageBox
            // и прерываем выполнение
            if (_error.Length > 0)
            {
                MessageBox.Show(_error.ToString());
                return;
            }
            // выполняем транзакцию
            using (var transaction =
            MaterialsCalculatorEntities.GetContext().Database.BeginTransaction())
            {
                try
                {
                    // формируем нового пользователя, добавляя данные из полей
                    _currentUser.LastName = TextBoxLastName.Text;
                    _currentUser.FirstName = TextBoxFirstName.Text;
                    _currentUser.MiddleName = TextBoxMiddleName.Text;
                    _currentUser.Email = TextBoxEmail.Text;
                        _currentUser.Password = PasswordBoxAgain.Password;
                    _currentUser.GenderID = 1;
                    if (ComboBoxGender.SelectedIndex == 1)
                        _currentUser.GenderID = 2;
                    _currentUser.RoleID = 2;                   

                    MaterialsCalculatorEntities.GetContext().Users.Add(_currentUser);
                    MaterialsCalculatorEntities.GetContext().SaveChanges();

                    transaction.Commit();
                    // Сохраняем изменения в БД
                    MessageBox.Show("Запись добавлена");
                    DialogResult = true; // Возвращаемся на предыдущую форму
                    Manager.CurrentUser = _currentUser;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        } 

        //подбор имени файла
        
        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        /*private void CheckBoxIsVisiblePassword_Unchecked(object sender, RoutedEventArgs e)
        {
            PasswordBoxFirst.Password = TextBoxPasswordFirst.Text;
            PasswordBoxFirst.Visibility = Visibility.Visible;
            TextBoxPasswordFirst.Visibility = Visibility.Collapsed;
            PasswordBoxAgain.Password = TextBoxPasswordAgain.Text;
            PasswordBoxAgain.Visibility = Visibility.Visible;
            TextBoxPasswordAgain.Visibility = Visibility.Collapsed;
        }*/
    }
    
}