using ClosedXML.Excel;
using RequestsApp.Frames;
using RequestsApp.Models;
using RequestsApp.Services.Addition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Diagnostics;

using System.Windows.Shapes;

namespace RequestsApp
{
    /// <summary>
    /// Логика взаимодействия для DateWindow.xaml
    /// </summary>
    public partial class DateWindow : Window
    {
        public int userId;

        RequestsDbContext db;
        public DateWindow(int userId)
        {
            InitializeComponent();

            db = new RequestsDbContext();
            this.userId = userId;
            if (userId != 2)
                EmployeesButton.Visibility = Visibility.Collapsed;
            MainFrame.Navigate(new Uri(@"Frames/RequestsView.xaml", UriKind.Relative));
        }

        private void ApplicationsButton_Click(object sender, RoutedEventArgs e)
        {
            SearchText.Clear();
            MainFrame.Visibility = Visibility.Visible;
            SearchBox.Items.Clear();
            string[] arr = { "Объекты", "Сотрудник", "Проблема", "Телефон" };
            for (int i = 0; i < arr.Length; i++)
            {
                SearchBox.Items.Add(arr[i]);
            }
            TableLabel.Content = "Список заявок";
            MainFrame.Navigate(new Uri(@"Frames/RequestsView.xaml", UriKind.Relative));
        }
        private void GraphicButton_Click(object sender, RoutedEventArgs e)
        {
            SearchText.Clear();
            MainFrame.Visibility = Visibility.Visible;
            SearchBox.Items.Clear();
            string[] arr = { "Объекты", "Проблема", "Телефон" };
            for (int i = 0; i < arr.Length; i++)
            {
                SearchBox.Items.Add(arr[i]);
            }
            var user = db.EmployeesTables.First(s => s.EmployeeId == userId);
            TableLabel.Content = "Список заявок сотрудника: " + user.SecondName + " " + user.FirstName.ToUpper()[0] + ". " + user.ThirdName.ToUpper()[0] + ".";
            MainFrame.Navigate(new Frames.ScheduleView(userId));
        }
        private void AgreementButton_Click(object sender, RoutedEventArgs e)
        {
            SearchText.Clear();
            MainFrame.Visibility = Visibility.Visible;
            SearchBox.Items.Clear();
            string[] arr = { "Объекты", "Заголовок" };
            for (int i = 0; i < arr.Length; i++)
            {
                SearchBox.Items.Add(arr[i]);
            }
            TableLabel.Content = "Список заключенных документов";
            MainFrame.Navigate(new Uri(@"Frames/DocumentsView.xaml", UriKind.Relative));
        }

        private void ComponyButton_Click(object sender, RoutedEventArgs e)
        {
            SearchText.Clear();
            MainFrame.Visibility = Visibility.Visible;
            SearchBox.Items.Clear();
            string[] arr = { "Название", "Адрес", "ФИО директора", "ФИО агент", "Телефон" };
            for (int i = 0; i < arr.Length; i++)
            {
                SearchBox.Items.Add(arr[i]);
            }
            TableLabel.Content = "Список организации";
            MainFrame.Navigate(new Uri(@"Frames/FacilitiesView.xaml", UriKind.Relative));
        }

        private void EmployeesButton_Click(object sender, RoutedEventArgs e)
        {
            SearchText.Clear();
            MainFrame.Visibility = Visibility.Visible;
            SearchBox.Items.Clear();
            string[] arr = { "Фамилия", "Имя", "Отчество", "Телефон", "Email", "Пароль", "Должность" };
            for (int i = 0; i < arr.Length; i++)
            {
                SearchBox.Items.Add(arr[i]);
            }
            TableLabel.Content = "Список сотрудников";
            MainFrame.Navigate(new Uri(@"Frames/EmployeesView.xaml", UriKind.Relative));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            switch (MainFrame.Content.ToString().Split('.')[2])
            {
                case "RequestsView":
                    TableLabel.Content = "Список заявок";
                    var reqWind = new AddRequest();
                    if (reqWind.ShowDialog() == true)
                        MainFrame.Navigate(new Frames.RequestsView());
                    break;
                case "EmployeesView":
                    TableLabel.Content = "Список сотрудников";
                    var empView = new AddEmployee();
                    if (empView.ShowDialog() == true)
                        MainFrame.Navigate(new Frames.EmployeesView());
                    break;
                case "DocumentsView":
                    TableLabel.Content = "Список заключенных документов";
                    var docView = new AddDocument();
                    if (docView.ShowDialog() == true)
                        MainFrame.Navigate(new Frames.DocumentsView());
                    break;
                case "FacilitiesView":
                    TableLabel.Content = "Список объектов";
                    var facView = new AddFacility();
                    if (facView.ShowDialog() == true)
                        MainFrame.Navigate(new Frames.FacilitiesView());
                    break;
            }
        }

        private void ClearSearchButton_Click(object sender, RoutedEventArgs e)
        {
            switch (MainFrame.Content.ToString().Split('.')[2])
            {
                case "RequestsView":
                    MainFrame.Navigate(new Frames.RequestsView());
                    break;
                case "EmployeesView":
                    MainFrame.Navigate(new Frames.EmployeesView());
                    break;
                case "DocumentsView":
                    MainFrame.Navigate(new Frames.DocumentsView());
                    break;
                case "FacilitiesView":
                    MainFrame.Navigate(new Frames.FacilitiesView());
                    break;
                case "ScheduleView":
                    MainFrame.Navigate(new Frames.ScheduleView());
                    break;
            }
            SearchText.Clear();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            switch (MainFrame.Content.ToString().Split('.')[2])
            {
                case "RequestsView":
                    MainFrame.Navigate(new Frames.RequestsView(SearchBox.Text, SearchText.Text));
                    break;
                case "EmployeesView":
                    MainFrame.Navigate(new Frames.EmployeesView(SearchBox.Text, SearchText.Text));
                    break;
                case "DocumentsView":
                    MainFrame.Navigate(new Frames.DocumentsView(SearchBox.Text, SearchText.Text));
                    break;
                case "FacilitiesView":
                    MainFrame.Navigate(new Frames.FacilitiesView(SearchBox.Text, SearchText.Text));
                    break;
                case "ScheduleView":
                    MainFrame.Navigate(new Frames.ScheduleView(SearchBox.Text, SearchText.Text));
                    break;
            }
        }

        private void TablesButton_Click(object sender, RoutedEventArgs e)
        {
            AddButton.Visibility = Visibility.Visible;
            TablesButton.Visibility = Visibility.Collapsed;
            RasGraButton.Visibility = Visibility.Collapsed;
            ApplicationsButton.Visibility = Visibility.Visible;
            AgreementButton.Visibility = Visibility.Visible;
            ComponyButton.Visibility = Visibility.Visible;
            EmployeesButton.Visibility = Visibility.Visible;
            BackButton.Visibility = Visibility.Visible;
        }

        private void RasGraButton_Click(object sender, RoutedEventArgs e)
        {
            AddButton.Visibility = Visibility.Visible;
            TablesButton.Visibility = Visibility.Collapsed;
            GraphicButton.Visibility = Visibility.Visible;
            ReportsButton.Visibility = Visibility.Visible;
            RasGraButton.Visibility = Visibility.Collapsed;
            BackButton.Visibility = Visibility.Visible;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (ApplicationsButton.Visibility == Visibility.Visible)
                if (AgreementButton.Visibility == Visibility.Visible)
                    if(ComponyButton.Visibility == Visibility.Visible)
                        if(EmployeesButton.Visibility == Visibility.Visible)
                        {
                            TablesButton.Visibility = Visibility.Visible;
                            ApplicationsButton.Visibility = Visibility.Collapsed;
                            AgreementButton.Visibility = Visibility.Collapsed;
                            ComponyButton.Visibility = Visibility.Collapsed;
                            EmployeesButton.Visibility = Visibility.Collapsed;
                            RasGraButton.Visibility = Visibility.Visible;
                            BackButton.Visibility = Visibility.Collapsed;
                            SearchBox.Items.Clear();
                        }
            if(GraphicButton.Visibility == Visibility.Visible)
                if(ReportsButton.Visibility == Visibility.Visible)
                {
                    TablesButton.Visibility = Visibility.Visible;
                    RasGraButton.Visibility = Visibility.Visible;
                    BackButton.Visibility = Visibility.Collapsed;
                    GraphicButton.Visibility = Visibility.Collapsed;
                    ReportsButton.Visibility = Visibility.Collapsed;
                    SearchBox.Items.Clear();
                }
            if (ReportCloseButton.Visibility == Visibility.Visible)
                if (ReportOrgSotrButton.Visibility == Visibility.Visible)
                {
                    GraphicButton.Visibility = Visibility.Visible;
                    ReportsButton.Visibility = Visibility.Visible;
                    ReportCloseButton.Visibility = Visibility.Collapsed;
                    ReportOrgSotrButton.Visibility = Visibility.Collapsed;
                    RasGraButton.Visibility = Visibility.Collapsed;
                }
        }

        private void ReportsButton_Click(object sender, RoutedEventArgs e)
        {
            GraphicButton.Visibility = Visibility.Collapsed;
            ReportsButton.Visibility = Visibility.Collapsed;
            RasGraButton.Visibility = Visibility.Collapsed;
            ReportCloseButton.Visibility = Visibility.Visible;
            ReportOrgSotrButton.Visibility = Visibility.Visible;
            BackButton.Visibility = Visibility.Visible;
        }

        private void ReportOrgSotrButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Статистический отчет");

                    // Добавление заголовков статистических данных
                    worksheet.Cell("A1").Value = "Всего закрытых актов";
                    worksheet.Cell("B1").Value = "Средняя продолжительность выполнения";
                    worksheet.Cell("C1").Value = "Кол-во актов за месяц";
                    worksheet.Cell("D1").Value = "Сотрудник";
                    worksheet.Cell("E1").Value = "Кол-во актов выполненных сотрудником";

                    // Получение данных из таблицы закрытых актов
                    var closedActs = db.ClosedRequestsTables.ToList();

                    // Вычисление статистических данных
                    int totalClosedActs = closedActs.Count;
                    TimeSpan averageDuration = TimeSpan.Zero;

                    if (totalClosedActs > 0)
                    {
                        DateTime minOpenDate = closedActs.Min(act => act.OpenDate);
                        DateTime maxCloseDate = closedActs.Max(act => act.CloseDate ?? DateTime.Now);

                        averageDuration = maxCloseDate - minOpenDate;
                        averageDuration = TimeSpan.FromTicks(averageDuration.Ticks / totalClosedActs);
                    }

                    // Заполнение данных статистического отчета
                    worksheet.Cell("A2").Value = totalClosedActs;
                    worksheet.Cell("B2").Value = averageDuration.TotalDays.ToString("0.00") + " дней";

                    // Вычисление количества актов по месяцам и сотрудникам
                    var actsByMonth = closedActs.GroupBy(act => new { act.CloseDate?.Year, act.CloseDate?.Month })
                                                 .Select(group => new
                                                 {
                                                     Month = $"{group.Key.Month:D2}/{group.Key.Year}",
                                                     Count = group.Count()
                                                 });

                    var actsByEmployee = closedActs.GroupBy(act => act.Employee)
                                                    .Select(group => new
                                                    {
                                                        Employee = group.Key,
                                                        Count = group.Count()
                                                    });

                    // Заполнение данных о количестве актов по месяцам
                    int row = 2;
                    foreach (var monthAct in actsByMonth)
                    {
                        worksheet.Cell("C" + row).Value = monthAct.Count;
                        row++;
                    }

                    // Заполнение данных о количестве актов по сотрудникам
                    row = 2;
                    foreach (var employeeAct in actsByEmployee)
                    {
                        worksheet.Cell("D" + row).Value = employeeAct.Employee;
                        worksheet.Cell("E" + row).Value = employeeAct.Count;
                        row++;
                    }

                   

                    // Форматирование текста в ячейках
                    worksheet.Columns().AdjustToContents(); // Автоматическое изменение ширины столбцов

                    // Сохранение файла статистического отчета
                    var saveFileDialog = new Microsoft.Win32.SaveFileDialog();
                    saveFileDialog.Filter = "Excel файл (*.xlsx)|*.xlsx";
                    saveFileDialog.FileName = "Статистический_отчет";
                    if (saveFileDialog.ShowDialog() == true)
                    {
                        workbook.SaveAs(saveFileDialog.FileName);
                        MessageBox.Show("Статистический отчет успешно создан и сохранен.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при создании статистического отчета: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }

        private void ReportCloseButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Отчет по закрытым актам");

                    // Добавление заголовков
                    worksheet.Cell("A1").Value = "Организация";
                    worksheet.Cell("B1").Value = "Сотрудник";
                    worksheet.Cell("C1").Value = "Проблема";
                    worksheet.Cell("D1").Value = "Дата открытия";
                    worksheet.Cell("E1").Value = "Дата закрытия";
                    worksheet.Cell("F1").Value = "Телефон";
                    worksheet.Cell("G1").Value = "Комментарий";

                    // Получение данных из таблицы закрытых актов
                    var closedActs = db.ClosedRequestsTables.ToList();

                    // Заполнение данных в таблицу отчета
                    int row = 2;
                    foreach (var act in closedActs)
                    {
                        worksheet.Cell("A" + row).Value = act.Facility;
                        worksheet.Cell("B" + row).Value = act.Employee;
                        worksheet.Cell("C" + row).Value = act.Issue;
                        worksheet.Cell("D" + row).Value = act.OpenDate.ToString("dd.MM.yyyy");
                        worksheet.Cell("E" + row).Value = act.CloseDate?.ToString("dd.MM.yyyy"); // Проверка на null
                        worksheet.Cell("F" + row).Value = act.Phone;
                        worksheet.Cell("G" + row).Value = act.Comment;

                        row++;
                    }

                    // Сохранение файла отчета
                    var saveFileDialog = new Microsoft.Win32.SaveFileDialog();
                    saveFileDialog.Filter = "Excel файл (*.xlsx)|*.xlsx";
                    saveFileDialog.FileName = "Отчет_по_закрытым_актам";
                    if (saveFileDialog.ShowDialog() == true)
                    {
                        workbook.SaveAs(saveFileDialog.FileName);
                        MessageBox.Show("Отчет успешно создан и сохранен.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при создании отчета: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void HelpButton_Click(object sender, RoutedEventArgs e)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "C:\\Windows\\hh.exe";
            startInfo.Arguments = "Help.chm";
            Process.Start(startInfo);
        }

    }
}
