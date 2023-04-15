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
        /// Проверка наличия квалификации в базе данных
        /// </summary>
        /// <param name="name">Наименование квалификации</param>
        /// <returns>Квалификации нет в базе (true), квалификация есть в базе (false)</returns>
        bool CheckQualifications(string name)
        {
            QualificationTable qualification = Classes.DataBaseClass.connect.QualificationTable.FirstOrDefault(x => x.Name == name);
            if (qualification == null)
            {
                return true;
            }
            else
            {
                MessageBox.Show("Такая квалификация уже есть в базе!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (Classes.CheckFieldsClasses.CheckFieldsQualification(tbName.Text))
            {
                if(CheckQualifications(tbName.Text))
                {
                    try
                    {
                        QualificationTable qualification = new QualificationTable()
                        {
                            Name = tbName.Text,
                        };
                        Classes.DataBaseClass.connect.QualificationTable.Add(qualification);
                        Classes.DataBaseClass.connect.SaveChanges();
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
}
