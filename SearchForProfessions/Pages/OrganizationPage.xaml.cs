﻿using SearchForProfessions.Model;
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
    /// Логика взаимодействия для OrganizationPage.xaml
    /// </summary>
    public partial class OrganizationPage : Page
    {
        public OrganizationPage()
        {
            InitializeComponent();
            listOrganization.ItemsSource = Clasees.DataBaseClass.connect.OrganizationTable.ToList();
        }

        private void btnAddOrganization_Click(object sender, RoutedEventArgs e)
        {
            OrganizationWindow window = new OrganizationWindow();
            window.ShowDialog();
            Clasees.FrameClass.frame.Navigate(new OrganizationPage());
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            int id = Convert.ToInt32(btn.Uid);
            OrganizationTable organization = Clasees.DataBaseClass.connect.OrganizationTable.FirstOrDefault(x=> x.ID == id);
            OrganizationWindow window = new OrganizationWindow(organization);
            window.ShowDialog();
            Clasees.FrameClass.frame.Navigate(new OrganizationPage());
        }
    }
}
