using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RequestsApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace RequestsApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private RequestsDbContext db;
        private bool isUpdate = false;
        public MainWindow()
        {
            InitializeComponent();
            db = new RequestsDbContext();
            if (db.EmployeesTables.Count() == 0) {
                NameWindow.Content = "Регистрация администратора";
                NameWindow.Visibility = Visibility.Visible;
                NameLabel.Visibility = Visibility.Visible;
                NameTextBox.Visibility = Visibility.Visible;
                OpenDateReg.Visibility = Visibility.Visible;
                OpenDate.Visibility = Visibility.Collapsed;
            }
        }


        private void OpenDate_Click(object sender, RoutedEventArgs e)
        {
            
            foreach (var el in db.EmployeesTables)
            {
                if (el.Email == Login.Text && el.Password == Password.Text)
                {
                    var open = new DateWindow(el.EmployeeId);
                    open.Show();
                    Close();
                    break;
                }
            }
        }

        private void CloseDate_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MyCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            
        }

        private void MyCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
        }

        private void OpenDateReg_Click(object sender, RoutedEventArgs e)
        {
            var name = NameTextBox.Text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (name.Length !=  3) {
                MessageBox.Show("Вы что то не ввели, например: фамилию, имя, отчество.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(Login.Text) ||
                (!Login.Text.EndsWith("@mail.ru") && !Login.Text.EndsWith("@gmail.com")) ||
                Login.Text.IndexOf("@") != Login.Text.LastIndexOf("@") ||
                Login.Text.Count(c => c == '@') != 1)
            {
                MessageBox.Show("Некорректный формат Email. Должен быть вида: example@mail.ru или example@gmail.com.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(Password.Text) || Password.Text.Length < 8 || Password.Text.Distinct().Count() != Password.Text.Length)
            {
                MessageBox.Show("Некорректный формат пароля. Пароль должен содержать минимум 8 символов и отличаться друг от друга.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var fn = new SqlParameter("@FN", name[0]);
            var sn = new SqlParameter("@SN", name[1]);
            var tn = new SqlParameter("@TN", name[2]);
            var p = new SqlParameter("@P", "+375445684016");
            var email = new SqlParameter("@E", Login.Text);
            var pass = new SqlParameter("@PASS", Password.Text);
            var pos = new SqlParameter("@POS", "Директор");
            db.Database.ExecuteSqlRawAsync("INSERT Employees_table(First_name, Second_name, Third_name, Phone, Email, Password, Position)" +
                    "VALUES (@FN, @SN, @TN, @P, @E, @PASS, @POS)", fn, sn, tn, p, email, pass, pos);
            db = new RequestsDbContext();
            foreach (var el in db.EmployeesTables)
            {
                if (el.Email == Login.Text && el.Password == Password.Text)
                {
                    var open = new DateWindow(el.EmployeeId);
                    open.Show();
                    Close();
                    break;
                }

            }
        }
    }
}
