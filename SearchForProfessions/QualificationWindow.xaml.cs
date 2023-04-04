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

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (Classes.CheckFieldsClasses.CheckFieldsQualification(tbName.Text))
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
