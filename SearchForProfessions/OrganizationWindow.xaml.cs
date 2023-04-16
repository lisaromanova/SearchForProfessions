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
    /// Логика взаимодействия для OrganizationWindow.xaml
    /// </summary>
    public partial class OrganizationWindow : Window
    {
        OrganizationTable organization;
        public OrganizationWindow()
        {
            InitializeComponent();
        }

        public OrganizationWindow(OrganizationTable organization)
        {
            InitializeComponent();
            tbTitle.Text = "Изменение организации";
            this.organization = organization;
            tbPrefix.Text = organization.Prefix;
            tbName.Text = organization.Name;
            tbPhone.Text = organization.Phone;
            tbAdress.Text = organization.Adress;
            tbEmail.Text = organization.E_mail;
            tbSite.Text = organization.Site;
            tbHotline.Text = organization.Hotline;
            if (organization.AvailableEnvironment)
            {
                rbYes.IsChecked = true;
            }
            else
            {
                rbNo.IsChecked = true;
            }
            btnDelete.Visibility = Visibility.Visible;
            btnSave.HorizontalAlignment= HorizontalAlignment.Left;
        }

        

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (Classes.CheckFieldsClasses.CheckFieldsOrganization(tbPrefix.Text, tbName.Text, tbPhone.Text, tbAdress.Text, tbEmail.Text, tbSite.Text, tbHotline.Text, (bool)rbYes.IsChecked, (bool)rbNo.IsChecked))
            {
                try
                {
                    if(organization == null)
                    {
                        organization = new OrganizationTable();
                        Classes.DataBaseClass.connect.OrganizationTable.Add(organization);
                    }
                    organization.Prefix = tbPrefix.Text;
                    organization.Name = tbName.Text;
                    organization.Phone = tbPhone.Text;
                    organization.Adress = tbAdress.Text;
                    if (string.IsNullOrWhiteSpace(tbEmail.Text))
                    {
                        organization.E_mail = null;
                    }
                    else
                    {
                        organization.E_mail = tbEmail.Text;
                    }
                    if (string.IsNullOrWhiteSpace(tbSite.Text))
                    {
                        organization.Site = null;
                    }
                    else
                    {
                        organization.Site = tbSite.Text;
                    }
                    if (string.IsNullOrWhiteSpace(tbHotline.Text))
                    {
                        organization.Hotline = null;
                    }
                    else
                    {
                        organization.Hotline = tbHotline.Text;
                    }
                    if ((bool)rbYes.IsChecked)
                    {
                        organization.AvailableEnvironment = true;
                    }
                    if ((bool)rbNo.IsChecked)
                    {
                        organization.AvailableEnvironment = false;
                    }
                    Classes.DataBaseClass.connect.SaveChanges();
                    MessageBox.Show("Данные успешно сохранены!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                    Close();
                }
                catch
                {
                    MessageBox.Show("Ошибка", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (organization != null)
            {
                MessageBoxResult result = MessageBox.Show("Вы точно хотите удалить организацию?", "Удаление организации", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        Classes.DataBaseClass.connect.OrganizationTable.Remove(organization);
                        Classes.DataBaseClass.connect.SaveChanges();
                        MessageBox.Show("Организация удалена!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
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