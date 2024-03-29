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
using System.Windows.Navigation;
using System.Windows.Shapes;

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

    }
}
