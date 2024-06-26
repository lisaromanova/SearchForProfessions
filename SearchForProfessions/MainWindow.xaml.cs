﻿using System;
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

namespace SearchForProfessions
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Classes.DataBaseClass.connect = new Model.BaseOfSpecializationsEntities();
            Classes.FrameClass.frame = frmLoad;
            Classes.FrameClass.frame.Navigate(new Pages.MainPage());
        }

        private void btnViewSpecialization_Click(object sender, RoutedEventArgs e)
        {
            Classes.FrameClass.frame.Navigate(new Pages.SpecializationPage());
        }

        private void btnViewOrganization_Click(object sender, RoutedEventArgs e)
        {
            Classes.FrameClass.frame.Navigate(new Pages.OrganizationPage());
        }

        private void btnMain_Click(object sender, RoutedEventArgs e)
        {
            Classes.FrameClass.frame.Navigate(new Pages.MainPage());
        }
    }
}
