﻿using BusStationWPF.Model;
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

namespace BusStationWPF.View
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private UserShow user;
        public LoginWindow(UserShow user)
        {
            InitializeComponent();
            passwordBox.Password = user.Password;
            this.user = user;
            DataContext = user;
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            user.Password = passwordBox.Password;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
