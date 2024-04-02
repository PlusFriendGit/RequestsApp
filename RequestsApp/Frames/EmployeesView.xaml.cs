using DocumentFormat.OpenXml.InkML;
using Microsoft.EntityFrameworkCore;
using RequestsApp.Models;
using RequestsApp.Services.Addition;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RequestsApp.Frames
{
    /// <summary>
    /// Логика взаимодействия для EmployeesView.xaml
    /// </summary>
    public partial class EmployeesView : Page
    {
        private RequestsDbContext db;
        public EmployeesView()
        {
            InitializeComponent();
            db = new RequestsDbContext();
            dataGrid.ItemsSource = db.EmployeesTables.ToList();
        }
        public EmployeesView(string search, string find) : this()
        {
            find = find.ToLower();
            switch (search)
            {
                case "Фамилия":
                    dataGrid.ItemsSource = db.EmployeesTables.Where(w => w.SecondName.ToLower().Contains(find)).ToList();
                    break;
                case "Имя":
                    dataGrid.ItemsSource = db.EmployeesTables.Where(w => w.FirstName.ToLower().Contains(find)).ToList();
                    break;
                case "Отчество":
                    dataGrid.ItemsSource = db.EmployeesTables.Where(w => w.ThirdName.ToLower().Contains(find)).ToList();
                    break;
                case "Телефон":
                    dataGrid.ItemsSource = db.EmployeesTables.Where(w => w.Phone.ToLower().Contains(find)).ToList();
                    break;
                case "Email":
                    dataGrid.ItemsSource = db.EmployeesTables.Where(w => w.Email.ToLower().Contains(find)).ToList();
                    break;
                case "Пароль":
                    dataGrid.ItemsSource = db.EmployeesTables.Where(w => w.Password.ToLower().Contains(find)).ToList();
                    break;
                case "Должность":
                    dataGrid.ItemsSource = db.EmployeesTables.Where(w => w.Position.ToLower().Contains(find)).ToList();
                    break;

            }
        }
        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedIndex >= 0 && MessageBox.Show("Вы действительно собираетельс удалить выделенную запись?", "Подтвердите действие", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                var id = (dataGrid.SelectedItem as EmployeesTable).EmployeeId;
                db.Database.ExecuteSqlRaw($"DELETE FROM Employees_table WHERE Employee_id = {id}");
                db = new RequestsDbContext();
                dataGrid.ItemsSource = db.EmployeesTables.ToList();
            }
        }

        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var wind = new AddEmployee(db.EmployeesTables
                .First(d => d.EmployeeId == (dataGrid.SelectedItem as Models.EmployeesTable).EmployeeId));

            if (wind.ShowDialog() == true)
            {
                db = new RequestsDbContext();
                dataGrid.ItemsSource = db.EmployeesTables.ToList();
            }
        }
    }
}
