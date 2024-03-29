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
    /// Логика взаимодействия для AddFacility.xaml
    /// </summary>
    public partial class AddFacility : Window
    {
        private RequestsDbContext db;
        private bool isUpdate = false;
        private int id;
        public AddFacility()
        {
            InitializeComponent();
            db = new RequestsDbContext ();
        }
        public AddFacility(FacilityTable fac) : this()
        {
            wind_label.Content = "Обновление объекта";
            Title = "Обновление объекта";
            AddButton.Content = "Обновить";

            id = fac.FacilityId;
            name_tb.Text = fac.Name;
            address_tb.Text = fac.Address;
            phone_tb.Text = fac.Phone;
            dir_tb.Text = fac.DirName;
            agent_tb.Text = fac.AgentName;

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
            // Проверка на пустоту полей и корректность данных
            if (string.IsNullOrWhiteSpace(name_tb.Text))
            {
                MessageBox.Show("Поле 'Название' не может быть пустым.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(address_tb.Text))
            {
                MessageBox.Show("Поле 'Адресс' не может быть пустым.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(dir_tb.Text))
            {
                MessageBox.Show("Поле 'Имя директора' не может быть пустым.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(agent_tb.Text))
            {
                MessageBox.Show("Поле 'Имя агента' не может быть пустым.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!Regex.IsMatch(phone_tb.Text, @"^\+375\d{2}\d{3}\d{2}\d{2}$"))
            {
                MessageBox.Show("Некорректный формат номера телефона.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Продолжение операции добавления/обновления записи в базе данных
            var sql_id = new SqlParameter("@ID", id);
            var n = new SqlParameter("@N", name_tb.Text);
            var a = new SqlParameter("@A", address_tb.Text);
            var d = new SqlParameter("@D", dir_tb.Text);
            var agent = new SqlParameter("@Agent", agent_tb.Text);
            var phone = new SqlParameter("@P", phone_tb.Text);

            if (!isUpdate)
            {
                db.Database.ExecuteSqlRawAsync("INSERT Facility_table(Name, Address, Dir_name, Agent_name, Phone)" +
                    "VALUES (@N, @A, @D, @Agent, @P)", n, a, d, agent, phone);
            }
            else
            {
                db.Database.ExecuteSqlRawAsync("UPDATE Facility_table SET Name = @N, Address = @A, Dir_name = @D, Agent_name = @Agent, Phone = @P " +
                    "WHERE Facility_id = @ID", n, a, d, agent, phone, sql_id);
            }
            db = new RequestsDbContext();
            this.DialogResult = true; // Устанавливаем DialogResult в true только после успешной операции
            Close();
        }
    }
}
