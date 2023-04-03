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

        /// <summary>
        /// Проверка заполнения полей
        /// </summary>
        /// <param name="prefix">Аббревиатура организации</param>
        /// <param name="name">Наименование организации</param>
        /// <param name="phone">Телефон организации</param>
        /// <param name="adress">Адрес организации</param>
        /// <param name="checkNo">Радио кнопка нет в поле доступная среда</param>
        /// <param name="checkYes">Радио кнопка да в поле доступная среда</param>
        /// <returns>Поля заполнены (true), поля не заполнены (false)</returns>
        bool CheckFields(string prefix, string name, string phone, string adress, bool checkYes, bool checkNo)
        {
            if (!string.IsNullOrWhiteSpace(prefix))
            {
                if (!string.IsNullOrWhiteSpace(name))
                {
                    if (!string.IsNullOrWhiteSpace(phone))
                    {
                        if (!string.IsNullOrWhiteSpace(adress))
                        {
                            if (checkYes || checkNo)
                            {
                                return true;
                            }
                            else
                            {
                                MessageBox.Show("Выберите доступную среду!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                                return false;
                            }
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
            if (CheckFields(tbPrefix.Text, tbName.Text, tbPhone.Text, tbAdress.Text, (bool)rbYes.IsChecked, (bool)rbNo.IsChecked))
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
                    if (string.IsNullOrWhiteSpace(tbHotline.Text))
                    {
                        organization.Hotline = null;
                    }
                    else
                    {
                        organization.Hotline = tbEmail.Text;
                    }
                    if ((bool)rbYes.IsChecked)
                    {
                        organization.AvailableEnvironment = true;
                    }
                    if ((bool)rbNo.IsChecked)
                    {
                        organization.AvailableEnvironment = false;
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

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (organization != null)
            {
                MessageBoxResult result = MessageBox.Show("Вы точно хотите удалить организацию?", "Удаление организации", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        Clasees.DataBaseClass.connect.OrganizationTable.Remove(organization);
                        Clasees.DataBaseClass.connect.SaveChanges();
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