using SearchForProfessions.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        }

        public AdmissionPlanWindow(AdmissionPlanTable admissionPlan)
        {
            InitializeComponent();
            tbTitle.Text = "Изменение плана приема";
            btnDeletePlan.Visibility = Visibility.Visible;
            btnSave.HorizontalAlignment = HorizontalAlignment.Left;
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
            List<AdmissionPlanQualificationTable> admissionQualification = admissionPlan.AdmissionPlanQualificationTable.ToList();
            foreach (AdmissionPlanQualificationTable table in admissionQualification)
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
            List<QualificationTable> qualifications = Classes.DataBaseClass.connect.QualificationTable.OrderBy(x => x.Name).ToList();
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
            cbOrganization.ItemsSource = Classes.DataBaseClass.connect.OrganizationTable.ToList().OrderBy(x=> x.FullName);
            cbOrganization.SelectedValuePath = "ID";
            cbOrganization.DisplayMemberPath = "FullName";
            cbSpecialization.ItemsSource = Classes.DataBaseClass.connect.SpecializationTable.ToList().OrderBy(x => x.Name);
            cbSpecialization.SelectedValuePath = "ID";
            cbSpecialization.DisplayMemberPath = "Name";
            FillQualifications();
            cbFormOfTraining.ItemsSource = Classes.DataBaseClass.connect.FormOfTrainingTable.ToList();
            cbFormOfTraining.SelectedValuePath = "ID";
            cbFormOfTraining.DisplayMemberPath = "Name";
            cbFinancialBase.ItemsSource = Classes.DataBaseClass.connect.FinancialBasisTable.ToList();
            cbFinancialBase.SelectedValuePath = "ID";
            cbFinancialBase.DisplayMemberPath = "Name";
            cbEducationLevel.ItemsSource = Classes.DataBaseClass.connect.EducationLevelTable.ToList();
            cbEducationLevel.SelectedValuePath = "ID";
            cbEducationLevel.DisplayMemberPath = "Name";
        }

        private void cbSpecialization_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            gpQualifications.Visibility = Visibility.Visible;
            SpecializationTable specialization = Classes.DataBaseClass.connect.SpecializationTable.FirstOrDefault(x => x.ID == (int)cbSpecialization.SelectedValue);
            cbQualification.Items.Clear();
            if (specialization != null)
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
                QualificationTable qualification = Classes.DataBaseClass.connect.QualificationTable.FirstOrDefault(x => x.Name == (string)cbQualification.SelectedValue);
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

        

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (Classes.CheckFieldsClasses.CheckFieldsAdmissionPlan(cbOrganization.SelectedIndex, cbSpecialization.SelectedIndex, qualifications, cbFormOfTraining.SelectedIndex, cbFinancialBase.SelectedIndex, cbEducationLevel.SelectedIndex, tbPeriodOfStudy.Text, tbAdmissionPlan.Text, (bool)rbYes.IsChecked, (bool)rbNo.IsChecked))
            {
                try
                {
                    if(admissionPlan == null)
                    {
                        admissionPlan = new AdmissionPlanTable();
                        Classes.DataBaseClass.connect.AdmissionPlanTable.Add(admissionPlan);
                    }
                    admissionPlan.IDSpecialization = (int)cbSpecialization.SelectedValue;
                    admissionPlan.IDOrganization = (int)cbOrganization.SelectedValue;
                    admissionPlan.IDFinancialBasisis = (int)cbFinancialBase.SelectedValue;
                    admissionPlan.IDEducationLevel = (int)cbEducationLevel.SelectedValue;
                    admissionPlan.IDFormOfTraining = (int)cbFormOfTraining.SelectedValue;
                    admissionPlan.PeriodOfStudy = tbPeriodOfStudy.Text;
                    admissionPlan.AdmissionPlan = Convert.ToInt32(tbAdmissionPlan.Text);
                    if(rbYes.IsChecked == true)
                    {
                        admissionPlan.EntranceTest = true;
                    }
                    if (rbNo.IsChecked == true)
                    {
                        admissionPlan.EntranceTest = false;
                    }
                    if (admissionPlan.ID != 0)
                    {
                        List<AdmissionPlanQualificationTable> list = admissionPlan.AdmissionPlanQualificationTable.ToList();
                        foreach (AdmissionPlanQualificationTable table in list)
                        {
                            Classes.DataBaseClass.connect.AdmissionPlanQualificationTable.Remove(table);
                        }
                    }
                    foreach (QualificationTable qualification in qualifications)
                    {
                        Classes.DataBaseClass.connect.AdmissionPlanQualificationTable.Add(new AdmissionPlanQualificationTable()
                        {
                            IDAdmissionPlan = admissionPlan.ID,
                            IDQualification = qualification.ID
                        });
                    }
                    Classes.DataBaseClass.connect.SaveChanges();
                    MessageBox.Show("Данные успешно сохранены!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                    Close();
                }
                catch
                {
                    MessageBox.Show("Ошибка!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btnDeletePlan_Click(object sender, RoutedEventArgs e)
        {
            if (admissionPlan != null)
            {
                MessageBoxResult result = MessageBox.Show("Вы точно хотите удалить запись?", "Удаление записи", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        Classes.DataBaseClass.connect.AdmissionPlanTable.Remove(admissionPlan);
                        Classes.DataBaseClass.connect.SaveChanges();
                        MessageBox.Show("Запись удалена!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                        Close();
                    }
                    catch
                    {
                        MessageBox.Show("Ошибка", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }
    }
}
