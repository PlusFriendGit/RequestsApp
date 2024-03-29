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

namespace RequestsApp.Services.Addition
{
    /// <summary>
    /// Логика взаимодействия для AddDocument.xaml
    /// </summary>
    public partial class AddDocument : Window
    {
        private RequestsDbContext db;
        private bool isUpdate = false;
        private int id;
        public AddDocument()
        {
            InitializeComponent();
            db = new RequestsDbContext();

            facility_cb.ItemsSource = db.FacilityTables.ToList();
            facility_cb.DisplayMemberPath = "Name";
        }
        public AddDocument(DocumentsTable doc) : this()
        {
            wind_label.Content = "Обновление документа";
            Title = "Обновление документа";
            AddButton.Content = "Обновить";

            id = doc.DocumentId;
            facility_cb.SelectedItem = db.FacilityTables.First(f => f.FacilityId == doc.FacilityId);
            title_tb.Text = doc.Title;
            desc_tb.Text = doc.Description;

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
            if (string.IsNullOrWhiteSpace(title_tb.Text) || string.IsNullOrWhiteSpace(desc_tb.Text))
            {
                MessageBox.Show("Поля 'Название' и 'Описание' не могут быть пустыми.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            #endregion

            this.DialogResult = true;

            var sql_id = new SqlParameter("@ID", id);
            var f = new SqlParameter("@F", (facility_cb.SelectedItem as FacilityTable).FacilityId);
            var t = new SqlParameter("@T", title_tb.Text);
            var d = new SqlParameter("@D", desc_tb.Text);

            if (!isUpdate)
            {
                db.Database.ExecuteSqlRawAsync("INSERT Documents_table(Facility_id, Title, Description)" +
                    "VALUES (@F, @T, @D)", f, t, d);
            }
            else
            {
                db.Database.ExecuteSqlRawAsync("UPDATE Documents_table SET Facility_id = @F, Title = @T, Description = @D " +
                    "WHERE Document_id = @ID", f, t, d, sql_id);
            }
            db = new RequestsDbContext();
            Close();
        }

    }
}
