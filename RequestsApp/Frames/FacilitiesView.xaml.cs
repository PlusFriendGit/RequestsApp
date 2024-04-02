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
    /// Логика взаимодействия для FacilitiesView.xaml
    /// </summary>
    public partial class FacilitiesView : Page
    {
        private RequestsDbContext db;
        public FacilitiesView()
        {
            InitializeComponent();
            db = new RequestsDbContext();
            dataGrid.ItemsSource = db.FacilityTables.ToList();
        }
        public FacilitiesView(string search, string find) : this()
        {
            find = find.ToLower();
            switch (search)
            {
                case "Название":
                    dataGrid.ItemsSource = db.FacilityTables.Where(w => w.Name.ToLower().Contains(find)).ToList();
                    break;
                case "Адрес":
                    dataGrid.ItemsSource = db.FacilityTables.Where(w => w.Address.ToLower().Contains(find)).ToList();
                    break;
                case "ФИО директора":
                    dataGrid.ItemsSource = db.FacilityTables.Where(w => w.DirName.ToLower().Contains(find)).ToList();
                    break;
                case "ФИО агент":
                    dataGrid.ItemsSource = db.FacilityTables.Where(w => w.AgentName.ToLower().Contains(find)).ToList();
                    break;
                case "Телефон":
                    dataGrid.ItemsSource = db.FacilityTables.Where(w => w.Phone.ToLower().Contains(find)).ToList();
                    break;
            }
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedIndex >= 0 && MessageBox.Show("Вы действительно собираетельс удалить выделенную запись?", "Подтвердите действие", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                var id = (dataGrid.SelectedItem as FacilityTable).FacilityId;
                db.Database.ExecuteSqlRaw($"DELETE FROM Facility_table WHERE Facility_id = {id}");
                db = new RequestsDbContext();
                dataGrid.ItemsSource = db.FacilityTables.ToList();
            }
        }

        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var wind = new AddFacility(db.FacilityTables
                .First(d => d.FacilityId == (dataGrid.SelectedItem as Models.FacilityTable).FacilityId));

            if (wind.ShowDialog() == true)
            {
                db = new RequestsDbContext();
                dataGrid.ItemsSource = db.FacilityTables.ToList();
            }
        }
    }
}
