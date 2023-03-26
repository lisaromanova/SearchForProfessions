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
        public SpecializationPage()
        {
            InitializeComponent();
            listSpecialization.ItemsSource = Clasees.DataBaseClass.connect.SpecializationTable.ToList();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            int id = Convert.ToInt32(btn.Uid);
            SpecializationTable specialization = Clasees.DataBaseClass.connect.SpecializationTable.FirstOrDefault(x => x.ID == id);
            SpecializationWindow window = new SpecializationWindow(specialization);
            window.ShowDialog();
        }

        private void btnAddSpecialization_Click(object sender, RoutedEventArgs e)
        {
            SpecializationWindow window = new SpecializationWindow();
            window.ShowDialog();
        }
    }
}
