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
            organization = new OrganizationTable();
            Clasees.DataBaseClass.connect.OrganizationTable.Add(organization);
        }

        public OrganizationWindow(OrganizationTable organization)
        {
            InitializeComponent();
            this.organization = organization;
            tbPrefix.Text = organization.Prefix;
            tbName.Text = organization.Name;
            tbPhone.Text = organization.Phone;
            tbAdress.Text = organization.Adress;
            tbEmail.Text = organization.E_mail;
        }

        /// <summary>
        /// Проверка заполнения полей
        /// </summary>
        /// <param name="prefix">Аббревиатура организации</param>
        /// <param name="name">Наименование организации</param>
        /// <param name="phone">Телефон организации</param>
        /// <param name="adress">Адрес организации</param>
        /// <returns>Поля заполнены (true), поля не заполнены (false)</returns>
        bool CheckFields(string prefix, string name, string phone, string adress)
        {
            if (!string.IsNullOrWhiteSpace(prefix))
            {
                if (!string.IsNullOrWhiteSpace(name))
                {
                    if (!string.IsNullOrWhiteSpace(phone))
                    {
                        if (!string.IsNullOrWhiteSpace(adress))
                        {
                            return true;
                        }
                        else
                        {
                            MessageBox.Show("Введите адрес организации!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            return false;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Введите телефон организации!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show("Введите наименование организации!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Введите аббревиатуру организации!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (CheckFields(tbPrefix.Text, tbName.Text, tbPhone.Text, tbAdress.Text))
            {
                try
                {
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
                    Clasees.DataBaseClass.connect.SaveChanges();
                    MessageBox.Show("Данные успешно сохранены!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
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