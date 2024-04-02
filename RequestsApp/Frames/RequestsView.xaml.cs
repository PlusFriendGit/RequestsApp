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
    /// Логика взаимодействия для RequestsView.xaml
    /// </summary>
    public partial class RequestsView : Page
    {
        private RequestsDbContext db;
        
        public RequestsView()
        {
            InitializeComponent();

            db = new RequestsDbContext();
            dataGrid.ItemsSource = db.RequestsViews.ToList();
        }

        public RequestsView(string search, string find) : this()
        {
            find = find.ToLower();
            switch (search)
            {
                case "Объекты":
                    dataGrid.ItemsSource = db.RequestsViews.Where(w => w.Name.ToLower().Contains(find)).ToList();
                    break;
                case "Сотрудник":
                    dataGrid.ItemsSource = db.RequestsViews.Where(w => w.SecondName.ToLower().Contains(find)).ToList();
                    break;
                case "Проблема":
                    dataGrid.ItemsSource = db.RequestsViews.Where(w => w.Issue.ToLower().Contains(find)).ToList();
                    break;
                case "Телефон":
                    dataGrid.ItemsSource = db.RequestsViews.Where(w => w.Phone.ToLower().Contains(find)).ToList();
                    break;

            }
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedIndex >= 0 && MessageBox.Show("Вы действительно собираетельс удалить выделенную запись?", "Подтвердите действие", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                var id = (dataGrid.SelectedItem as Models.RequestsView).RequestId;
                db.Database.ExecuteSqlRaw($"DELETE FROM Requests_table WHERE Request_id = {id}");
                db = new RequestsDbContext();
                dataGrid.ItemsSource = db.RequestsViews.ToList();
            }
        }

        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var wind = new AddRequest(db.RequestsTables
                .First(d => d.RequestId == (dataGrid.SelectedItem as Models.RequestsView).RequestId));

            if (wind.ShowDialog() == true)
            {
                db = new RequestsDbContext();
                dataGrid.ItemsSource = db.RequestsViews.ToList();
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
