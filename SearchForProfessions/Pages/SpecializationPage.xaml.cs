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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SearchForProfessions.Pages
{
    /// <summary>
    /// Логика взаимодействия для SpecializationPage.xaml
    /// </summary>
    public partial class SpecializationPage : Page
    {
        List<SpecializationTable> specializations;
        public SpecializationPage()
        {
            InitializeComponent();
            specializations = Classes.DataBaseClass.connect.SpecializationTable.OrderBy(x=> x.Name).ToList();
            listSpecialization.ItemsSource = specializations;
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            int id = Convert.ToInt32(btn.Uid);
            SpecializationTable specialization = Classes.DataBaseClass.connect.SpecializationTable.FirstOrDefault(x => x.ID == id);
            SpecializationWindow window = new SpecializationWindow(specialization);
            window.ShowDialog();
            Classes.FrameClass.frame.Navigate(new SpecializationPage());
        }

        private void btnAddSpecialization_Click(object sender, RoutedEventArgs e)
        {
            SpecializationWindow window = new SpecializationWindow();
            window.ShowDialog();
            Classes.FrameClass.frame.Navigate(new SpecializationPage());
        }

        /// <summary>
        /// Поиск специальностей
        /// </summary>
        void Search()
        {
            List<SpecializationTable> list = specializations.ToList();
            if (!string.IsNullOrWhiteSpace(tbSearch.Text))
            {
                list = list.Where(x => x.FullName.ToLower().Contains(tbSearch.Text.ToLower()) || x.Qualifications.ToLower().Contains(tbSearch.Text.ToLower())).ToList();
            }
            if (list.Count > 0)
            {
                listSpecialization.Visibility = Visibility.Visible;
                listSpecialization.ItemsSource = list;
                tbEmpty.Visibility = Visibility.Collapsed;
            }
            else
            {
                listSpecialization.Visibility = Visibility.Collapsed;
                tbEmpty.Visibility = Visibility.Visible;
            }
        }

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Search();
        }
    }
}
