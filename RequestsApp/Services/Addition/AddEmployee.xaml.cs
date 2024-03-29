using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RequestsApp.Models;
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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace RequestsApp.Services.Addition
{
    /// <summary>
    /// Логика взаимодействия для AddEmployee.xaml
    /// </summary>
    public partial class AddEmployee : Window
    {
        private RequestsDbContext db;
        private bool isUpdate = false;
        private int id;
        public AddEmployee()
        {
            InitializeComponent();
            db = new RequestsDbContext();
        }
        public AddEmployee(EmployeesTable emp) : this()
        {
            wind_label.Content = "Обновление сотрудника";
            Title = "Обновление сотрудника";
            AddButton.Content = "Обновить";

            id = emp.EmployeeId;
            first_name.Text = emp.FirstName;
            second_name.Text = emp.SecondName;
            third_name.Text = emp.ThirdName;
            email_tb.Text = emp.Email;
            password_tb.Text = emp.Password;
            position_tb.Text = emp.Position;
            phone_tb.Text = emp.Phone;

            isUpdate = true;
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {

            #region Field_Validation
            // Проверка поля 'Имя'
            if (string.IsNullOrWhiteSpace(first_name.Text))
            {
                MessageBox.Show("Поле 'Имя' не может быть пустым.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Проверка поля 'Фамилия'
            if (string.IsNullOrWhiteSpace(second_name.Text))
            {
                MessageBox.Show("Поле 'Фамилия' не может быть пустым.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Проверка поля 'Отчество'
            if (string.IsNullOrWhiteSpace(third_name.Text))
            {
                MessageBox.Show("Поле 'Отчество' не может быть пустым.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Проверка поля 'Телефон'
            if (string.IsNullOrWhiteSpace(phone_tb.Text.Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")", "")))
            {
                MessageBox.Show("Поле 'Телефон' не может быть пустым.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Проверка поля 'Email'
            if (string.IsNullOrWhiteSpace(email_tb.Text) ||
                (!email_tb.Text.EndsWith("@mail.ru") && !email_tb.Text.EndsWith("@gmail.com")) ||
                email_tb.Text.IndexOf("@") != email_tb.Text.LastIndexOf("@") ||
                email_tb.Text.Count(c => c == '@') != 1)
            {
                MessageBox.Show("Некорректный формат Email. Должен быть вида: example@mail.ru или example@gmail.com.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Проверка поля 'Пароль'
            if (string.IsNullOrWhiteSpace(password_tb.Text) ||
                password_tb.Text.Length < 8 ||
                password_tb.Text.Distinct().Count() != password_tb.Text.Length)
            {
                MessageBox.Show("Некорректный формат пароля. Пароль должен содержать минимум 8 символов и отличаться друг от друга.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Проверка поля 'Должность'
            if (string.IsNullOrWhiteSpace(position_tb.Text))
            {
                MessageBox.Show("Поле 'Должность' не может быть пустым.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            #endregion

            // Продолжение операции добавления/обновления записи в базе данных
            var sql_id = new SqlParameter("@ID", id);
            var fn = new SqlParameter("@FN", first_name.Text);
            var sn = new SqlParameter("@SN", second_name.Text);
            var tn = new SqlParameter("@TN", third_name.Text);
            var p = new SqlParameter("@P", phone_tb.Text);
            var email = new SqlParameter("@E", email_tb.Text);
            var pass = new SqlParameter("@PASS", password_tb.Text);
            var pos = new SqlParameter("@POS", position_tb.Text.Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")", ""));

            if (!isUpdate)
            {
                db.Database.ExecuteSqlRawAsync("INSERT Employees_table(First_name, Second_name, Third_name, Phone, Email, Password, Position)" +
                    "VALUES (@FN, @SN, @TN, @P, @E, @PASS, @POS)", fn, sn, tn, p, email, pass, pos);
            }
            else
            {
                db.Database.ExecuteSqlRawAsync("UPDATE Employees_table SET First_name = @FN, Second_name = @SN, Third_name = @TN, Phone = @P, Email = @E, Password = @PASS, Position = @POS " +
                    "WHERE Employee_id = @ID", fn, sn, tn, p, email, pass, pos, sql_id);
            }
            db = new RequestsDbContext();
            Close();
        }

    }
}
