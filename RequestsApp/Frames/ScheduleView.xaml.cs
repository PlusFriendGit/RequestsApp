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
    /// Логика взаимодействия для ScheduleView.xaml
    /// </summary>
    public partial class ScheduleView : Page
    {
        private RequestsDbContext db;
        public ScheduleView(int empId) : this()
        {
            dataGrid.ItemsSource = db.ScheduleViews.Where(d => d.EmployeeId == empId).OrderByDescending(d => d.EmployeeId).ToList();
        }
        public ScheduleView(string search, string find) : this()
        {
            find = find.ToLower();
            switch (search)
            {
                case "Объекты":
                    dataGrid.ItemsSource = db.ScheduleViews.Where(w => w.Name.ToLower().Contains(find)).ToList();
                    break;
                case "Проблема":
                    dataGrid.ItemsSource = db.ScheduleViews.Where(w => w.Issue.ToLower().Contains(find)).ToList();
                    break;
                case "Телефон":
                    dataGrid.ItemsSource = db.ScheduleViews.Where(w => w.Phone.ToLower().Contains(find)).ToList();
                    break;
            }
        }
        public ScheduleView()
        {
            InitializeComponent();

            db = new RequestsDbContext();
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedIndex >= 0 && MessageBox.Show("Вы действительно собираетесь закрыть заявку?", "Подтвердите действие", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                var id = (dataGrid.SelectedItem as Models.ScheduleView).RequestId;
                //db.Database.ExecuteSqlRaw($"DELETE FROM Requests_table WHERE Request_id = {id}");
                db.Database.ExecuteSqlRaw($"EXEC CloseRequestProcedure @RequestID = {id}");
                db = new RequestsDbContext();
                dataGrid.ItemsSource = db.RequestsViews.ToList();

            }
        }

    }
}
