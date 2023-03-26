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
    /// Логика взаимодействия для QualificationWindow.xaml
    /// </summary>
    public partial class QualificationWindow : Window
    {
        public QualificationWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Проверка заполнения полей
        /// </summary>
        /// <param name="name">Наименование квалификации</param>
        /// <returns>Поля заполнены (да), поля не заполнены (нет)</returns>
        bool CheckFields(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (CheckFields(tbName.Text))
            {
                try
                {
                    QualificationTable qualification = new QualificationTable()
                    {
                        Name = tbName.Text,
                    };
                    Clasees.DataBaseClass.connect.QualificationTable.Add(qualification);
                    Clasees.DataBaseClass.connect.SaveChanges();
                    MessageBox.Show("Данные успешно сохранены", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                    Close();
                }
                catch
                {
                    MessageBox.Show("Ошибка", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Введите наименование квалификации!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
