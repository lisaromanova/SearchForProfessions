using SearchForProfessions.Model;
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

namespace SearchForProfessions
{
    /// <summary>
    /// Логика взаимодействия для AdmissionPlanWindow.xaml
    /// </summary>
    public partial class AdmissionPlanWindow : Window
    {
        List<QualificationTable> qualifications = new List<QualificationTable>();
        AdmissionPlanTable admissionPlan;
        public AdmissionPlanWindow()
        {
            InitializeComponent();
            FillComboBox();
            admissionPlan = new AdmissionPlanTable();
            Clasees.DataBaseClass.connect.AdmissionPlanTable.Add(admissionPlan);
        }

        public AdmissionPlanWindow(AdmissionPlanTable admissionPlan)
        {
            InitializeComponent();
            FillComboBox();
            cbOrganization.SelectedValue = admissionPlan.IDOrganization;
            cbSpecialization.SelectedValue = admissionPlan.IDSpecialization;
            cbFormOfTraining.SelectedValue = admissionPlan.IDFormOfTraining;
            cbFinancialBase.SelectedValue = admissionPlan.IDFinancialBasisis;
            cbEducationLevel.SelectedValue = admissionPlan.IDEducationLevel;
            tbPeriodOfStudy.Text = admissionPlan.PeriodOfStudy;
            tbAdmissionPlan.Text = admissionPlan.AdmissionPlan.ToString();
            if (admissionPlan.EntranceTest)
            {
                rbYes.IsChecked = true;
            }
            if (!admissionPlan.EntranceTest)
            {
                rbNo.IsChecked = true;
            }
            List<SpecializationQualificationTable> specializationQualifications = admissionPlan.SpecializationTable.SpecializationQualificationTable.ToList();
            foreach (SpecializationQualificationTable table in specializationQualifications)
            {
                qualifications.Add(table.QualificationTable);
            }
            lbQualification.ItemsSource = qualifications;
            this.admissionPlan = admissionPlan;
        }

        /// <summary>
        /// Заполнение выпадающего списка квалификаций
        /// </summary>
        void FillQualifications()
        {
            List<QualificationTable> qualifications = Clasees.DataBaseClass.connect.QualificationTable.OrderBy(x => x.Name).ToList();
            foreach (QualificationTable qualification in qualifications)
            {
                cbQualification.Items.Add(qualification.Name);
            }
        }

        /// <summary>
        /// Заполнение выпадающих списков
        /// </summary>
        void FillComboBox()
        {
            cbOrganization.ItemsSource = Clasees.DataBaseClass.connect.OrganizationTable.ToList().OrderBy(x=> x.FullName);
            cbOrganization.SelectedValuePath = "ID";
            cbOrganization.DisplayMemberPath = "FullName";
            cbSpecialization.ItemsSource = Clasees.DataBaseClass.connect.SpecializationTable.ToList().OrderBy(x => x.Name);
            cbSpecialization.SelectedValuePath = "ID";
            cbSpecialization.DisplayMemberPath = "Name";
            FillQualifications();
            cbFormOfTraining.ItemsSource = Clasees.DataBaseClass.connect.FormOfTrainingTable.ToList();
            cbFormOfTraining.SelectedValuePath = "ID";
            cbFormOfTraining.DisplayMemberPath = "Name";
            cbFinancialBase.ItemsSource = Clasees.DataBaseClass.connect.FinancialBasisTable.ToList();
            cbFinancialBase.SelectedValuePath = "ID";
            cbFinancialBase.DisplayMemberPath = "Name";
            cbEducationLevel.ItemsSource = Clasees.DataBaseClass.connect.EducationLevelTable.ToList();
            cbEducationLevel.SelectedValuePath = "ID";
            cbEducationLevel.DisplayMemberPath = "Name";
        }

        private void cbSpecialization_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SpecializationTable specialization = Clasees.DataBaseClass.connect.SpecializationTable.FirstOrDefault(x => x.ID == (int)cbSpecialization.SelectedValue);
            cbQualification.Items.Clear();
            if (specialization != null)
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
            qualifications.Clear();
            lbQualification.ItemsSource = null;
            lbQualification.ItemsSource = qualifications;
        }

        private void cbQualification_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            QualificationTable table = qualifications.FirstOrDefault(x => x.Name == (string)cbQualification.SelectedValue);
            if (table != null)
            {
                MessageBox.Show("Такой элемент уже есть в списке", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                QualificationTable qualification = Clasees.DataBaseClass.connect.QualificationTable.FirstOrDefault(x => x.Name == (string)cbQualification.SelectedValue);
                qualifications.Add(qualification);
                lbQualification.ItemsSource = null;
                lbQualification.ItemsSource = qualifications;
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            int id = Convert.ToInt32(btn.Uid);
            QualificationTable table = qualifications.FirstOrDefault(x => x.ID == id);
            qualifications.Remove(table);
            lbQualification.ItemsSource = null;
            lbQualification.ItemsSource = qualifications;
        }
    }
}
