﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SearchForProfessions.Model
{
    public partial class OrganizationTable
    {
        public string FullName
        {
            get
            {
                return Prefix + " " + Name;
            }
        }

        public Visibility EmailVisibility
        {
            get
            {
                if (E_mail != null)
                {
                    return Visibility.Visible;
                }
                else
                {
                    return Visibility.Collapsed;
                }
            }
        }

        public Visibility SiteVisibility
        {
            get
            {
                if (Site != null)
                {
                    return Visibility.Visible;
                }
                else
                {
                    return Visibility.Collapsed;
                }
            }
        }

        public Visibility HotlineVisibility
        {
            get
            {
                if (Hotline != null)
                {
                    return Visibility.Visible;
                }
                else
                {
                    return Visibility.Collapsed;
                }
            }
        }

        public string AvailableEnvironmentString
        {
            get
            {
                if (AvailableEnvironment)
                {
                    return "Да";
                }
                else
                {
                    return "Нет";
                }
            }
        }
    }
}
