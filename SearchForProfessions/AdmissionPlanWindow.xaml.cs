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
            admissionPlan = new AdmissionPlanTable();
            Clasees.DataBaseClass.connect.AdmissionPlanTable.Add(admissionPlan);
        }

        public AdmissionPlanWindow(AdmissionPlanTable admissionPlan)
        {
            InitializeComponent();
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
            gpQualifications.Visibility = Visibility.Visible;
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

        /// <summary>
        /// Проверка заполнения полей
        /// </summary>
        /// <param name="organization">Организация</param>
        /// <param name="specialization">Специальность</param>
        /// <param name="qualifications">Список квалификаций</param>
        /// <param name="form">Форма обучения</param>
        /// <param name="finance">Финансовая основа</param>
        /// <param name="level">Уровень образования</param>
        /// <param name="period">Период обучения</param>
        /// <param name="plan">План приема</param>
        /// <param name="checkYes">Радио кнопка да в поле вступительные испытания</param>
        /// <param name="checkNo">Радио кнопка нет в поле вступительные испытания</param>
        /// <returns>Поля заполнены (true), поля не заполнены (false)</returns>
        bool CheckFields(int organization, int specialization, List<QualificationTable> qualifications, int form, int finance, int level, string period, string plan, bool checkYes, bool checkNo)
        {
            if (organization != 1)
            {
                if (specialization != 1)
                {
                    if (qualifications.Count > 0)
                    {
                        if (form != -1)
                        {
                            if (Regex.IsMatch(period, "^\\d+ г\\. \\d+ мес\\.$"))
                            {
                                if (level != -1)
                                {
                                    if (finance != -1)
                                    {
                                        if (Regex.IsMatch(plan, @"^\d+$"))
                                        {
                                            if (checkYes || checkNo)
                                            {
                                                return true;
                                            }
                                            else
                                            {
                                                MessageBox.Show("Выберите наличие вступительных испытаний!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                                                return false;
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("Введите план приема!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                                            return false;
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Выберите финансовую основу!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                                        return false;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Выберите уровень образования!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                                    return false;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Введите период обучения корректно!\nПример 3 г. 10 мес.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                                return false;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Выберите форму обучения!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            return false;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Выберите квалификацию!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show("Выберите специальность!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Выберите организацию!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (CheckFields(cbOrganization.SelectedIndex, cbSpecialization.SelectedIndex, qualifications, cbFormOfTraining.SelectedIndex, cbFinancialBase.SelectedIndex, cbEducationLevel.SelectedIndex, tbPeriodOfStudy.Text, tbAdmissionPlan.Text, (bool)rbYes.IsChecked, (bool)rbNo.IsChecked))
            {
                try
                {
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
                    Clasees.DataBaseClass.connect.SaveChanges();
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
                        Clasees.DataBaseClass.connect.AdmissionPlanTable.Remove(admissionPlan);
                        Clasees.DataBaseClass.connect.SaveChanges();
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
