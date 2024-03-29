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
    /// Логика взаимодействия для DocumentsView.xaml
    /// </summary>
    public partial class DocumentsView : Page
    {
        private RequestsDbContext db;
        public DocumentsView()
        {
            InitializeComponent();
            db = new RequestsDbContext();
            dataGrid.ItemsSource = db.DocumentsViews.ToList();
        }
        public DocumentsView(string search, string find) : this()
        {
            find = find.ToLower();
            switch (search)
            {
                case "Объекты":
                    dataGrid.ItemsSource = db.DocumentsViews.Where(w => w.Name.ToLower().Contains(find)).ToList();
                    break;
                case "Заголовок":
                    dataGrid.ItemsSource = db.DocumentsViews.Where(w => w.Title.ToLower().Contains(find)).ToList();
                    break;
            }
        }
        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedIndex >= 0 && MessageBox.Show("Вы действительно собираетельс удалить выделенную запись?", "Подтвердите действие", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                var id = (dataGrid.SelectedItem as Models.DocumentsView).DocumentId;
                db.Database.ExecuteSqlRaw($"DELETE FROM Documents_table WHERE Document_id = {id}");
                dataGrid.ItemsSource = db.DocumentsViews.ToList();
                db = new RequestsDbContext();
                dataGrid.ItemsSource = db.RequestsViews.ToList();
            }
        }

        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var wind = new AddDocument(db.DocumentsTables
                .First(d => d.DocumentId == (dataGrid.SelectedItem as Models.DocumentsView).DocumentId));

            if (wind.ShowDialog() == true)
            {
                db = new RequestsDbContext();
                dataGrid.ItemsSource = db.DocumentsViews.ToList();
            }
        }
    }
}
