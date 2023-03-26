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
    /// Логика взаимодействия для SpecializationWindow.xaml
    /// </summary>
    public partial class SpecializationWindow : Window
    {
        public SpecializationWindow()
        {
            InitializeComponent();
            cbQualification.ItemsSource = Clasees.DataBaseClass.connect.QualificationTable.ToList();
            cbQualification.SelectedValuePath = "ID";
            cbQualification.DisplayMemberPath = "Name";
        }

        List<QualificationTable> qualifications = new List<QualificationTable>();

        public SpecializationWindow(SpecializationTable specialization)
        {
            InitializeComponent();
            cbQualification.ItemsSource = Clasees.DataBaseClass.connect.QualificationTable.ToList();
            cbQualification.SelectedValuePath = "ID";
            cbQualification.DisplayMemberPath = "Name";
            tbCode.Text = specialization.Code;
            tbName.Text = specialization.Name;
            List<SpecializationQualificationTable> specializationQualifications = specialization.SpecializationQualificationTable.ToList();
            foreach(SpecializationQualificationTable table in specializationQualifications)
            {
                qualifications.Add(table.QualificationTable);
            }
            lbQualification.ItemsSource = qualifications;
        }

        private void btnAddQualification_Click(object sender, RoutedEventArgs e)
        {
            QualificationWindow window = new QualificationWindow();
            window.ShowDialog();
            cbQualification.ItemsSource = null;
            cbQualification.ItemsSource = Clasees.DataBaseClass.connect.QualificationTable.ToList();
        }

        private void cbQualification_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            QualificationTable table = qualifications.FirstOrDefault(x=> x == cbQualification.SelectedItem as QualificationTable);
            if(table != null)
            {
                MessageBox.Show("Такой элемент уже есть в списке", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                qualifications.Add(cbQualification.SelectedItem as QualificationTable);
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
