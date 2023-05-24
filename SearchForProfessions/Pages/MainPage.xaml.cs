using Microsoft.Office.Interop.Excel;
using SearchForProfessions.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

using Excel = Microsoft.Office.Interop.Excel;

namespace SearchForProfessions.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : System.Windows.Controls.Page
    {
        List<AdmissionPlanTable> admissionPlanList;
        public MainPage()
        {
            InitializeComponent();
            List<SpecializationTable> specializations = Classes.DataBaseClass.connect.SpecializationTable.OrderBy(x=> x.Name).ToList();
            cbSpecialization.Items.Add("Все специальности");
            foreach(SpecializationTable specialization in specializations)
            {
                cbSpecialization.Items.Add(specialization.FullName);
            }
            cbQualification.Items.Add("Все квалификации");
            FillQualifications();
            admissionPlanList = Classes.DataBaseClass.connect.AdmissionPlanTable.ToList();
            listPlan.ItemsSource = admissionPlanList;
        }

        /// <summary>
        /// Заполнение выпадающего списка квалификаций
        /// </summary>
        void FillQualifications()
        {
            List<QualificationTable> qualifications = Classes.DataBaseClass.connect.QualificationTable.OrderBy(x=> x.Name).ToList();
            foreach (QualificationTable qualification in qualifications)
            {
                cbQualification.Items.Add(qualification.Name);
            }
        }

        private void rSite_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Run run = sender as Run;
            string site = (string)run.Tag;
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = site,
                    UseShellExecute = true
                });
            }
            catch
            {
                MessageBox.Show("Ошибка", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void cbSpecialization_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<SpecializationTable> specializations = Classes.DataBaseClass.connect.SpecializationTable.ToList();
            SpecializationTable specialization = specializations.FirstOrDefault(x => x.FullName == cbSpecialization.SelectedValue as string);
            cbQualification.Items.Clear();
            cbQualification.Items.Add("Все квалификации");
            if (specialization!= null)
            {
                List<string> qualifications = Classes.DataBaseClass.connect.SpecializationQualificationTable.Where(x => x.IDSpecialization == specialization.ID).Select(x => x.QualificationTable.Name).ToList();
                qualifications.Sort();
                foreach (string qualification in qualifications)
                {
                    cbQualification.Items.Add(qualification);
                }
            }
            else
            {
                FillQualifications();
            }
            Filter();
        }

        /// <summary>
        /// Фильтрация и сортировка
        /// </summary>
        void Filter()
        {
            List<AdmissionPlanTable> list = admissionPlanList.ToList();
            if (cbSort.SelectedIndex != -1)
            {
                switch(cbSort.SelectedIndex)
                {
                    case 0:
                        list = list.OrderBy(x=> x.OrganizationTable.Name).ToList();
                        break;
                    case 1:
                        list = list.OrderByDescending(x => x.OrganizationTable.Name).ToList();
                        break;
                }
            }
            if(cbSpecialization.SelectedIndex != -1 && cbSpecialization.SelectedIndex!=0)
            {
                list = list.Where(x => x.SpecializationTable.FullName == cbSpecialization.SelectedValue as string).ToList();
            }
            if (cbQualification.SelectedIndex != -1 && cbQualification.SelectedIndex != 0)
            {
                list = list.Where(x => x.Qualifications.ToLower().Contains(cbQualification.SelectedValue.ToString().ToLower())).ToList();
            }
            if (!string.IsNullOrWhiteSpace(tbSearch.Text))
            {
                list = list.Where(x => x.OrganizationTable.FullName.ToLower().Contains(tbSearch.Text.ToLower())).ToList();
            }
            if (list.Count > 0)
            {
                listPlan.Visibility = Visibility.Visible;
                tbEmpty.Visibility = Visibility.Collapsed;
                btnAddDataToExcel.IsEnabled = true;
                listPlan.ItemsSource = list;
            }
            else
            {
                listPlan.Visibility = Visibility.Collapsed;
                tbEmpty.Visibility = Visibility.Visible;
                btnAddDataToExcel.IsEnabled = false;
            }
        }

        /// <summary>
        /// Запись плана приема в файл excel
        /// </summary>
        /// <param name="list">Лист с планом приема</param>
        void DataToExcel(List<AdmissionPlanTable> list)
        {
            try
            {
                var excelApp = new Excel.Application();
                Workbook book = excelApp.Workbooks.Add();
                Excel._Worksheet workSheet = (Excel.Worksheet)excelApp.Worksheets[1];
                workSheet.Cells[1, "A"] = "Организация";
                workSheet.Cells[1, "B"] = "Телефон";
                workSheet.Cells[1, "C"] = "E-mail";
                workSheet.Cells[1, "D"] = "Адрес";
                workSheet.Cells[1, "E"] = "Сайт";
                workSheet.Cells[1, "F"] = "Горячая линия по приему лиц с инвалидностью и ОВЗ";
                workSheet.Cells[1, "G"] = "Доступная среда";
                workSheet.Cells[1, "H"] = "Специальность";
                workSheet.Cells[1, "I"] = "Квалификация";
                workSheet.Cells[1, "J"] = "Форма обучения";
                workSheet.Cells[1, "K"] = "Период обучения";
                workSheet.Cells[1, "L"] = "Уровень образования";
                workSheet.Cells[1, "M"] = "Финансовая основа";
                workSheet.Cells[1, "N"] = "План приема";
                workSheet.Cells[1, "O"] = "Вступительные испытания";
                var row = 1;
                foreach (var plan in list)
                {
                    row++;
                    workSheet.Cells[row, "A"] = plan.OrganizationTable.FullName;
                    workSheet.Cells[row, "B"] = plan.OrganizationTable.Phone;
                    workSheet.Cells[row, "C"] = plan.OrganizationTable.E_mail;
                    workSheet.Cells[row, "D"] = plan.OrganizationTable.Adress;
                    workSheet.Cells[row, "E"] = plan.OrganizationTable.Site;
                    workSheet.Cells[row, "F"] = plan.OrganizationTable.Hotline;
                    workSheet.Cells[row, "G"] = plan.OrganizationTable.AvailableEnvironmentString;
                    workSheet.Cells[row, "H"] = plan.SpecializationTable.FullName;
                    workSheet.Cells[row, "I"] = plan.Qualifications;
                    workSheet.Cells[row, "J"] = plan.FormOfTrainingTable.Name;
                    workSheet.Cells[row, "K"] = plan.PeriodOfStudy;
                    workSheet.Cells[row, "L"] = plan.EducationLevelTable.Name;
                    workSheet.Cells[row, "M"] = plan.FinancialBasisTable.Name;
                    workSheet.Cells[row, "N"] = plan.AdmissionPlan;
                    workSheet.Cells[row, "O"] = plan.EntranceTestString;
                }
                for (int i = 1; i <= 15; i++)
                {
                    workSheet.Columns[i].AutoFit();
                }
                book.SaveAs(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\План_приема_" + DateTime.Now.ToString("dd.MM.yyy_HH.mm.d") +".xlsx");
                book.Close();
                MessageBox.Show("Данные успешно записаны в файл excel.\nФайл находится в папке Документы", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch
            {
                MessageBox.Show("Ошибка!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void cbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filter();
        }

        private void btnAddEntry_Click(object sender, RoutedEventArgs e)
        {
            AdmissionPlanWindow window = new AdmissionPlanWindow();
            window.ShowDialog();
            Classes.FrameClass.frame.Navigate(new MainPage());
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.Button btn = sender as System.Windows.Controls.Button;
            int id = Convert.ToInt32(btn.Uid);
            AdmissionPlanTable admissionPlan = Classes.DataBaseClass.connect.AdmissionPlanTable.FirstOrDefault(x => x.ID == id);
            AdmissionPlanWindow window = new AdmissionPlanWindow(admissionPlan);
            window.ShowDialog();
            Classes.FrameClass.frame.Navigate(new MainPage());
        }

        private void btnAddDataToExcel_Click(object sender, RoutedEventArgs e)
        {
            btnAddDataToExcel.IsEnabled = false;
            List<AdmissionPlanTable> list = listPlan.Items.Cast<AdmissionPlanTable>().ToList();
            DataToExcel(list);
            btnAddDataToExcel.IsEnabled = true;
        }
    }
}
