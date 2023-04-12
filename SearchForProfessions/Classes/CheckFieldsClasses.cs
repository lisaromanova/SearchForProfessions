using SearchForProfessions.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace SearchForProfessions.Classes
{
    public class CheckFieldsClasses
    {
        /*
        /// <summary>
        /// Проверка заполнения полей плана приема
        /// </summary>
        /// <param name="organization">Организация</param>
        /// <param name="specialization">Специальность</param>
        /// <param name="qualifications">Список квалификаций</param>
        /// <param name="form">Форма обучения</param>
        /// <param name="finance">Финансовая основа</param>
        /// <param name="level">Уровень образования</param>
        /// <param name="period">Период обучения</param>
        /// <param name="plan">План приема</param>
        /// <param name="checkYes">Радио кнопка да в поле вступительные испытания</param>
        /// <param name="checkNo">Радио кнопка нет в поле вступительные испытания</param>
        /// <returns>Поля заполнены (true), поля не заполнены (false)</returns>
        public static bool CheckFieldsAdmissionPlan(int organization, int specialization, List<QualificationTable> qualifications, int form, int finance, int level, string period, string plan, bool checkYes, bool checkNo)
        {
            if (organization != -1)
            {
                if (specialization != -1)
                {
                    if (qualifications.Count > 0)
                    {
                        if (form != -1)
                        {
                            if (Regex.IsMatch(period, "^\\d+ г\\. \\d+ мес\\.$"))
                            {
                                if (level != -1)
                                {
                                    if (finance != -1)
                                    {
                                        if (Regex.IsMatch(plan, @"^\d+$"))
                                        {
                                            if (checkYes || checkNo)
                                            {
                                                return true;
                                            }
                                            else
                                            {
                                                MessageBox.Show("Выберите наличие вступительных испытаний!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                                                return false;
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("Введите план приема!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                                            return false;
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Выберите финансовую основу!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                                        return false;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Выберите уровень образования!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                                    return false;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Введите период обучения корректно!\nПример 3 г. 10 мес.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                                return false;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Выберите форму обучения!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            return false;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Выберите квалификацию!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show("Выберите специальность!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Выберите организацию!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        /// <summary>
        /// Проверка заполнения полей организации
        /// </summary>
        /// <param name="prefix">Аббревиатура организации</param>
        /// <param name="name">Наименование организации</param>
        /// <param name="phone">Телефон организации</param>
        /// <param name="adress">Адрес организации</param>
        /// <param name="checkNo">Радио кнопка нет в поле доступная среда</param>
        /// <param name="checkYes">Радио кнопка да в поле доступная среда</param>
        /// <returns>Поля заполнены (true), поля не заполнены (false)</returns>
        public static bool CheckFieldsOrganization(string prefix, string name, string phone, string adress, bool checkYes, bool checkNo)
        {
            if (Regex.IsMatch(prefix, "^[А-Яа-я0-9]+( [А-Яа-я0-9]+)*$"))
            {
                if (Regex.IsMatch(name, "^[А-Яа-я0-9]+( [А-Яа-я0-9]+)*$"))
                {
                    if (Regex.IsMatch(phone, "^[А-Яа-я0-9-()+,._]+( [А-Яа-я0-9-()+,._]+)*$"))
                    {
                        if (Regex.IsMatch(adress, "^[А-Яа-я0-9-(),_]+( [А-Яа-я0-9-(),_]+)*$"))
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

        /// <summary>
        /// Проверка заполнения полей квалификации
        /// </summary>
        /// <param name="name">Наименование квалификации</param>
        /// <returns>Поля заполнены (true), поля не заполнены (false)</returns>
        public static bool CheckFieldsQualification(string name)
        {
            if (Regex.IsMatch(name, "^[А-Яа-я0-9]+( [А-Яа-я0-9]+)*$"))
            {
                return true;
            }
            else
            {
                MessageBox.Show("Введите наименование квалификации!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        /// <summary>
        /// Проверка заполнения полей специальности
        /// </summary>
        /// <param name="code">Шифр специальности</param>
        /// <param name="name">Наименование специальности</param>
        /// <param name="collection">Список квалификаций</param>
        /// <returns>Поля заполнены (true), поля не заполнены (false)</returns>
        public static bool CheckFieldsSpecilization(string code, string name, List<QualificationTable> collection)
        {
            if (Regex.IsMatch(code, "^\\d\\d.\\d\\d.\\d\\d$"))
            {
                if (Regex.IsMatch(name, "^[А-Яа-я0-9]+( [А-Яа-я0-9]+)*$"))
                {
                    if (collection.Count != 0)
                    {
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Выберите квалификацию!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show("Введите наименование специальности!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Введите корректно шифр специальности!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        /// <summary>
        /// Проверка наличия квалификации в базе данных
        /// </summary>
        /// <param name="name">Наименование квалификации</param>
        /// <returns>Квалификации нет в базе (true), квалификация есть в базе (false)</returns>
        public static bool CheckQualifications(string name)
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
        }*/




        /// <summary>
        /// Проверка заполнения полей плана приема
        /// </summary>
        /// <param name="organization">Организация</param>
        /// <param name="specialization">Специальность</param>
        /// <param name="qualifications">Список квалификаций</param>
        /// <param name="form">Форма обучения</param>
        /// <param name="finance">Финансовая основа</param>
        /// <param name="level">Уровень образования</param>
        /// <param name="period">Период обучения</param>
        /// <param name="plan">План приема</param>
        /// <param name="checkYes">Радио кнопка да в поле вступительные испытания</param>
        /// <param name="checkNo">Радио кнопка нет в поле вступительные испытания</param>
        /// <returns>Поля заполнены (true), поля не заполнены (false)</returns>
        public static bool CheckFieldsAdmissionPlan(int organization, int specialization, List<QualificationTable> qualifications, int form, int finance, int level, string period, string plan, bool checkYes, bool checkNo)
        {
            if (organization != -1)
            {
                if (specialization != -1)
                {
                    if (qualifications.Count > 0)
                    {
                        if (form != -1)
                        {
                            if (!string.IsNullOrWhiteSpace(period))
                            {
                                if (level != -1)
                                {
                                    if (finance != -1)
                                    {
                                        if (Regex.IsMatch(plan, @"^\d+$"))
                                        {
                                            if (checkYes || checkNo)
                                            {
                                                return true;
                                            }
                                            else
                                            {
                                                MessageBox.Show("Выберите наличие вступительных испытаний!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                                                return false;
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("Введите план приема!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                                            return false;
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Выберите финансовую основу!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                                        return false;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Выберите уровень образования!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                                    return false;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Введите период обучения корректно!\nПример 3 г. 10 мес.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                                return false;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Выберите форму обучения!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            return false;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Выберите квалификацию!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show("Выберите специальность!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Выберите организацию!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        /// <summary>
        /// Проверка заполнения полей организации
        /// </summary>
        /// <param name="prefix">Аббревиатура организации</param>
        /// <param name="name">Наименование организации</param>
        /// <param name="phone">Телефон организации</param>
        /// <param name="adress">Адрес организации</param>
        /// <param name="checkNo">Радио кнопка нет в поле доступная среда</param>
        /// <param name="checkYes">Радио кнопка да в поле доступная среда</param>
        /// <returns>Поля заполнены (true), поля не заполнены (false)</returns>
        public static bool CheckFieldsOrganization(string prefix, string name, string phone, string adress, bool checkYes, bool checkNo)
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

        /// <summary>
        /// Проверка заполнения полей квалификации
        /// </summary>
        /// <param name="name">Наименование квалификации</param>
        /// <returns>Поля заполнены (true), поля не заполнены (false)</returns>
        public static bool CheckFieldsQualification(string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                return true;
            }
            else
            {
                MessageBox.Show("Введите наименование квалификации!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        /// <summary>
        /// Проверка заполнения полей специальности
        /// </summary>
        /// <param name="code">Шифр специальности</param>
        /// <param name="name">Наименование специальности</param>
        /// <param name="collection">Список квалификаций</param>
        /// <returns>Поля заполнены (true), поля не заполнены (false)</returns>
        public static bool CheckFieldsSpecilization(string code, string name, List<QualificationTable> collection)
        {
            if (!string.IsNullOrWhiteSpace(code))
            {
                if (!string.IsNullOrWhiteSpace(name))
                {
                    if (collection.Count != 0)
                    {
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Выберите квалификацию!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show("Введите наименование специальности!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Введите корректно шифр специальности!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        /// <summary>
        /// Проверка наличия квалификации в базе данных
        /// </summary>
        /// <param name="name">Наименование квалификации</param>
        /// <returns>Квалификации нет в базе (true), квалификация есть в базе (false)</returns>
        public static bool CheckQualifications(string name)
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
    }
}
