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
                                            MessageBox.Show("Введите план приема корректно!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
        /// <param name="email">E-mail организации</param>
        /// <param name="site">Сайт организации</param>
        /// <param name="bpoo">Горячая линия по приему лиц с инвалидностью и ОВЗ</param>
        /// <param name="checkNo">Радио кнопка нет в поле доступная среда</param>
        /// <param name="checkYes">Радио кнопка да в поле доступная среда</param>
        /// <returns>Поля заполнены (true), поля не заполнены (false)</returns>
        public static bool CheckFieldsOrganization(string prefix, string name, string phone, string adress, string email, string site, string bpoo, bool checkYes, bool checkNo)
        {
            if (Regex.IsMatch(prefix, "^[А-Яа-я0-9]+( [А-Яа-я0-9]+)*$"))
            {
                if (Regex.IsMatch(name, "^[А-Яа-я0-9(),.\\-:]+( [А-Яа-я0-9(),.\\-:]+)*$"))
                {
                    if (Regex.IsMatch(phone, "^[А-Яа-я0-9-():;+,._]+( [А-Яа-я0-9-():;+,._]+)*$"))
                    {
                        if (Regex.IsMatch(adress, "^[А-Яа-я0-9-(),:;._\\/]+( [А-Яа-я0-9-(),:;._\\/]+)*$"))
                        {
                            if (string.IsNullOrWhiteSpace(email) || Regex.IsMatch(email, "^[A-Za-z0-9-()`#,;:%.&_@\\/]+([A-Za-z0-9-()`#%&:,;._@\\/]+)*$"))
                            {
                                if (string.IsNullOrWhiteSpace(site) || Regex.IsMatch(site, "^[A-Za-z0-9-()`#,;%:.&_?@\\/]+([A-Za-z0-9-()`#%&,;:._@?\\/]+)*$"))
                                {
                                    if (string.IsNullOrWhiteSpace(bpoo) || Regex.IsMatch(bpoo, "^[А-Яа-я0-9-()+,;:._]+( [А-Яа-я0-9-()+,;:._]+)*$"))
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
                                        MessageBox.Show("Введите горячую линию по приему лиц с инвалидностью и ОВЗ корректно!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                                        return false;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Введите сайт организации корректно!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                                    return false;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Введите e-mail организации корректно!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                                return false;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Введите адрес организации корректно!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            return false;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Введите телефон организации корректно!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show("Введите наименование организации корректно!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Введите аббревиатуру организации корректно!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
            if (Regex.IsMatch(name, "^[А-Яа-я0-9()-:;,.\\/]+( [А-Яа-я0-9()-:;,.\\/]+)*$"))
            {
                return true;
            }
            else
            {
                MessageBox.Show("Введите наименование квалификации корректно!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
                if (Regex.IsMatch(name, "^[А-Яа-я0-9()-:;,.]+( [А-Яа-я0-9()-:;,.]+)*$"))
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
                    MessageBox.Show("Введите наименование специальности корректно!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Введите шифр специальности корректно!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

    }
}
