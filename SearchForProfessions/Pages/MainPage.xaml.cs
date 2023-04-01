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
        public MainPage()
        {
            InitializeComponent();
            List<SpecializationTable> specializations = Clasees.DataBaseClass.connect.SpecializationTable.ToList();
            cbSpecialization.Items.Add("Все специальности");
            foreach(SpecializationTable specialization in specializations)
            {
                cbSpecialization.Items.Add(specialization.Name);
            }
            List<QualificationTable> qualifications = Clasees.DataBaseClass.connect.QualificationTable.ToList();
            cbQualification.Items.Add("Все квалификации");
            foreach (QualificationTable qualification in qualifications)
            {
                cbQualification.Items.Add(qualification.Name);
            }
            listPlan.ItemsSource = Clasees.DataBaseClass.connect.AdmissionPlanTable.ToList();
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
                foreach (string qualification in qualifications)
                {
                    cbQualification.Items.Add(qualification);
                }
            }
            else
            {
                List<QualificationTable> qualifications = Clasees.DataBaseClass.connect.QualificationTable.ToList();
                foreach (QualificationTable qualification in qualifications)
                {
                    cbQualification.Items.Add(qualification.Name);
                }
            }
        }
    }
}
