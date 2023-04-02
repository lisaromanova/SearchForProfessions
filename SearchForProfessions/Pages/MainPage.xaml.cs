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

namespace SearchForProfessions.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        List<AdmissionPlanTable> admissionPlanList;
        public MainPage()
        {
            InitializeComponent();
            List<SpecializationTable> specializations = Clasees.DataBaseClass.connect.SpecializationTable.OrderBy(x=> x.Name).ToList();
            cbSpecialization.Items.Add("Все специальности");
            foreach(SpecializationTable specialization in specializations)
            {
                cbSpecialization.Items.Add(specialization.Name);
            }
            cbQualification.Items.Add("Все квалификации");
            FillQualifications();
            admissionPlanList = Clasees.DataBaseClass.connect.AdmissionPlanTable.ToList();
            listPlan.ItemsSource = admissionPlanList;
        }

        /// <summary>
        /// Заполнение выпадающего списка квалификаций
        /// </summary>
        void FillQualifications()
        {
            List<QualificationTable> qualifications = Clasees.DataBaseClass.connect.QualificationTable.OrderBy(x=> x.Name).ToList();
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
            SpecializationTable specialization = Clasees.DataBaseClass.connect.SpecializationTable.FirstOrDefault(x => x.Name == (string)cbSpecialization.SelectedValue);
            cbQualification.Items.Clear();
            cbQualification.Items.Add("Все квалификации");
            if (specialization!= null)
            {
                List<string> qualifications = Clasees.DataBaseClass.connect.SpecializationQualificationTable.Where(x => x.IDSpecialization == specialization.ID).Select(x => x.QualificationTable.Name).ToList();
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
                list = list.Where(x => x.SpecializationTable.Name == cbSpecialization.SelectedValue as string).ToList();
            }
            if (cbQualification.SelectedIndex != -1 && cbQualification.SelectedIndex != 0)
            {
                list = list.Where(x => x.Qualifications.ToLower().Contains(cbQualification.SelectedValue.ToString().ToLower())).ToList();
            }
            if(list.Count > 0)
            {
                listPlan.Visibility = Visibility.Visible;
                tbEmpty.Visibility = Visibility.Collapsed;
                listPlan.ItemsSource = list;
            }
            else
            {
                listPlan.Visibility = Visibility.Collapsed;
                tbEmpty.Visibility = Visibility.Visible;
            }
        }

        private void cbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btnAddEntry_Click(object sender, RoutedEventArgs e)
        {
            AdmissionPlanWindow window = new AdmissionPlanWindow();
            window.ShowDialog();
            Clasees.FrameClass.frame.Navigate(new MainPage());
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            int id = Convert.ToInt32(btn.Uid);
            AdmissionPlanTable admissionPlan = Clasees.DataBaseClass.connect.AdmissionPlanTable.FirstOrDefault(x => x.ID == id);
            AdmissionPlanWindow window = new AdmissionPlanWindow(admissionPlan);
            window.ShowDialog();
            Clasees.FrameClass.frame.Navigate(new MainPage());
        }
    }
}
