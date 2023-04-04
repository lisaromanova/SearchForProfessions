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
    /// Логика взаимодействия для OrganizationPage.xaml
    /// </summary>
    public partial class OrganizationPage : Page
    {
        List<OrganizationTable> organizations;
        public OrganizationPage()
        {
            InitializeComponent();
            organizations = Classes.DataBaseClass.connect.OrganizationTable.ToList();
            listOrganization.ItemsSource = organizations;
        }

        private void btnAddOrganization_Click(object sender, RoutedEventArgs e)
        {
            OrganizationWindow window = new OrganizationWindow();
            window.ShowDialog();
            Classes.FrameClass.frame.Navigate(new OrganizationPage());
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            int id = Convert.ToInt32(btn.Uid);
            OrganizationTable organization = Classes.DataBaseClass.connect.OrganizationTable.FirstOrDefault(x=> x.ID == id);
            OrganizationWindow window = new OrganizationWindow(organization);
            window.ShowDialog();
            Classes.FrameClass.frame.Navigate(new OrganizationPage());
        }

        /// <summary>
        /// Поиск и сортировка организаций
        /// </summary>
        void Search()
        {
            List<OrganizationTable> list = organizations.ToList();
            if (!string.IsNullOrWhiteSpace(tbSearch.Text))
            {
                list = list.Where(x => x.FullName.ToLower().Contains(tbSearch.Text.ToLower())).ToList();
            }
            if (cbSort.SelectedIndex != -1)
            {
                switch (cbSort.SelectedIndex)
                {
                    case 0:
                        list = list.OrderBy(x => x.Name).ToList();
                        break;
                    case 1:
                        list = list.OrderByDescending(x => x.Name).ToList();
                        break;
                }
            }
            if(list.Count > 0)
            {
                listOrganization.ItemsSource = list;
                listOrganization.Visibility= Visibility.Visible;
                tbEmpty.Visibility = Visibility.Collapsed;
            }
            else
            {
                listOrganization.Visibility = Visibility.Collapsed;
                tbEmpty.Visibility = Visibility.Visible;
            }
        }

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Search();
        }

        private void cbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Search();
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
    }
}
