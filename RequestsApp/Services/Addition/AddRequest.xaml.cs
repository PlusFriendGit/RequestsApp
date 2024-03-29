using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RequestsApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RequestsApp.Services.Addition
{
    /// <summary>
    /// Логика взаимодействия для AddRequest.xaml
    /// </summary>
    public partial class AddRequest : Window
    {
        private RequestsDbContext db;
        private bool isUpdate = false;
        private int id;
        public AddRequest()
        {
            InitializeComponent();
            db = new RequestsDbContext();

            facility_cb.ItemsSource = db.FacilityTables.ToList();
            facility_cb.DisplayMemberPath = "Name";
            employee_cb.ItemsSource = db.EmployeesTables.ToList();
            employee_cb.DisplayMemberPath = "SecondName";
        }
        public AddRequest(RequestsTable req) : this()
        {
            wind_label.Content = "Обновление заявки";
            Title = "Обновление заявки";
            AddButton.Content = "Обновить";

            request_label.Visibility = Visibility.Visible;
            status_cb.Visibility = Visibility.Visible;

            id = req.RequestId;
            facility_cb.SelectedItem = db.FacilityTables.First(f => f.FacilityId == req.FacilityId);
            employee_cb.SelectedItem = db.EmployeesTables.First(f => f.EmployeeId == req.EmployeeId);
            issue_tb.Text = req.Issue;
            open_date.SelectedDate = req.OpenDate;
            close_date.SelectedDate = req.CloseDate;
            phone_tb.Text = req.Phone;
            comment_tb.Text = req.Comment;
            status_cb.IsChecked = req.Status;
            db = new RequestsDbContext();
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
            // Проверка поля 'Проблема'
            if (string.IsNullOrWhiteSpace(issue_tb.Text))
            {
                MessageBox.Show("Поле 'Проблема' не может быть пустым.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Проверка поля 'Объект'
            if (facility_cb.SelectedItem == null)
            {
                MessageBox.Show("В поле 'Объект' не может быть пустым.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Проверка поля 'Сотрудник'
            if (employee_cb.SelectedItem == null)
            {
                MessageBox.Show("В поле 'Сотрудник' не может быть пустым.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Проверка выбранных дат
            if (open_date.SelectedDate == null || close_date.SelectedDate == null)
            {
                MessageBox.Show("Выберите дату открытия и закрытия.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            DateTime openDate = open_date.SelectedDate.Value;
            DateTime closeDate = close_date.SelectedDate.Value;

            // Проверка, что дата открытия не позднее текущей даты
            if (openDate > DateTime.Today)
            {
                MessageBox.Show("Дата открытия не может быть позже текущей даты.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Проверка, что дата закрытия не раньше даты открытия
            if (openDate > closeDate)
            {
                MessageBox.Show("Дата закрытия не может быть раньше даты открытия.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Проверка формата номера телефона
            if (!Regex.IsMatch(phone_tb.Text.Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")", ""), @"^\+375\d{2}\d{3}\d{2}\d{2}$"))
            {
                MessageBox.Show("Некорректный формат номера телефона.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Проверка поля 'Comment'
            if (string.IsNullOrWhiteSpace(comment_tb.Text))
            {
                MessageBox.Show("Поле 'Коментарии' не может быть пустым.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            #endregion

            var sql_id = new SqlParameter("@ID", id);
            var facility = new SqlParameter("@F", (facility_cb.SelectedItem as FacilityTable).FacilityId);
            var employee = new SqlParameter("@E", (employee_cb.SelectedItem as EmployeesTable).EmployeeId);
            var issue = new SqlParameter("@I", issue_tb.Text);
            var date1 = new SqlParameter("@OD", openDate.ToString());
            var date2 = new SqlParameter("@CD", closeDate.ToString());
            var phone = new SqlParameter("@P", phone_tb.Text.Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")", ""));
            var comment = new SqlParameter("@C", comment_tb.Text);
            var s = new SqlParameter("@S", status_cb.IsChecked);

            if (!isUpdate)
            {
                db.Database.ExecuteSqlRawAsync("INSERT Requests_table(Facility_id, Employee_id, Issue, Open_date, Close_date, Phone, Comment)" +
                    "VALUES (@F, @E, @I, @OD, @CD, @P, @C)", facility, employee, issue, date1, date2, phone, comment);
            }
            else
            {
                db.Database.ExecuteSqlRawAsync("UPDATE Requests_table SET Facility_id = @F, Employee_id = @E, Issue = @I, Open_date = @OD, Close_date = @CD, Phone = @P, Comment = @C, Status = @S " +
                    "WHERE Request_id = @ID", facility, employee, issue, date1, date2, phone, comment, s, sql_id);
            }

            
            this.DialogResult = true;
            db = new RequestsDbContext();
            Close();
        }

    }
}
