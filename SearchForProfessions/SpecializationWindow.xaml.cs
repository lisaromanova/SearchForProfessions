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
    /// Логика взаимодействия для SpecializationWindow.xaml
    /// </summary>
    public partial class SpecializationWindow : Window
    {
        SpecializationTable specializationTable;
        public SpecializationWindow()
        {
            InitializeComponent();
            cbQualification.ItemsSource = Clasees.DataBaseClass.connect.QualificationTable.ToList();
            cbQualification.SelectedValuePath = "ID";
            cbQualification.DisplayMemberPath = "Name";
            specializationTable = new SpecializationTable();
            Clasees.DataBaseClass.connect.SpecializationTable.Add(specializationTable);
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
            specializationTable = specialization;
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

        /// <summary>
        /// Проверка заполнения полей
        /// </summary>
        /// <param name="code">Шифр специальности</param>
        /// <param name="name">Наименование специальности</param>
        /// <param name="collection">Список квалификаций</param>
        /// <returns>Поля заполнены (true), поля не заполнены (false)</returns>
        bool CheckFields(string code, string name, List<QualificationTable> collection)
        {
            if(Regex.IsMatch(code, "^\\d\\d.\\d\\d.\\d\\d$"))
            {
                if (!string.IsNullOrEmpty(name))
                {
                    if (collection.Count!=0)
                    {
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Выберите квалификацию!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show("Введите наименование специальности!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Введите корректно шифр специальности!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if(CheckFields(tbCode.Text, tbName.Text, qualifications))
            {
                try
                {
                    specializationTable.Code = tbCode.Text;
                    specializationTable.Name = tbName.Text;
                    if (specializationTable.ID != 0)
                    {
                        List<SpecializationQualificationTable> list = specializationTable.SpecializationQualificationTable.ToList();
                        foreach (SpecializationQualificationTable table in list)
                        {
                            Clasees.DataBaseClass.connect.SpecializationQualificationTable.Remove(table);
                        }
                    }
                    foreach (QualificationTable qualification in qualifications)
                    {
                        Clasees.DataBaseClass.connect.SpecializationQualificationTable.Add(new SpecializationQualificationTable()
                        {
                            IDSpecialization = specializationTable.ID,
                            IDQualification = qualification.ID
                        });
                    }
                    Clasees.DataBaseClass.connect.SaveChanges();
                    MessageBox.Show("Данные успешно сохранены", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
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
