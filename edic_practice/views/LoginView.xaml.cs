﻿using edic_practice.model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace edic_practice.views
{
    /// <summary>
    /// Логика взаимодействия для LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        private string LogInPassword;

        public LoginView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new ed_practiceEntities())
            {
                var user = context.Users
                    .FirstOrDefault(u => u.Username == UsernameTextBox.Text 
                    && u.Password == LogInPassword); 

                if (user != null)
                {
                    CurrentUser.Username = user.Username;
                    CurrentUser.Role = (int)user.RoleID;
                    CurrentUser.FirstName = user.FirstName;
                    CurrentUser.SecondName = user.SecondName;
                    CurrentUser.Patronymic = user.Patronymic;

                    DashboardView dashboard = new DashboardView();
                    dashboard.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Invalid username or password.");
                }
            }
        }

        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start("https://github.com/Ashurumaru");
        }

        private void SignUp_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            RegisterView register = new RegisterView();
            register.Show();
        }


        private void PasswordBox1_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var passwordBox = (PasswordBox)sender;
            LogInPassword = passwordBox.Password;
        }

        private void btn_close_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btn_maximize_Click(object sender, RoutedEventArgs e)
        {
            SwitchWindowState();
        }

        private void btn_minimize_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).WindowState = WindowState.Minimized;
        }

        private void DockPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount >= 2)
            {
                SwitchWindowState();
                return;
            }

            if (Window.GetWindow(this).WindowState == WindowState.Maximized)
            {
                return;
            }
            else
            {
                if (e.LeftButton == MouseButtonState.Pressed) Window.GetWindow(this).DragMove();
            }
        }

        private void MaximizeWindow()
        {
            Window.GetWindow(this).WindowState = WindowState.Maximized;
        }

        private void RestoreWindow()
        {
            Window.GetWindow(this).WindowState = WindowState.Normal;
        }

        private void SwitchWindowState()
        {
            if (Window.GetWindow(this).WindowState == WindowState.Normal) MaximizeWindow();
            else RestoreWindow();
        }

        private void MinimizeAndDragMove(MouseButtonEventArgs e)
        {
            if (Window.GetWindow(this).WindowState == WindowState.Maximized)
            {
                double percentHorizontal = e.GetPosition(this).X / ActualWidth;
                double targetHorizontal = Window.GetWindow(this).RestoreBounds.Width * percentHorizontal;

                double percentVertical = e.GetPosition(this).Y / ActualHeight;
                double targetVertical = Window.GetWindow(this).RestoreBounds.Height * percentVertical;

                Window.GetWindow(this).WindowStyle = WindowStyle.None;
                RestoreWindow();

                var mousePosition = e.GetPosition(this);

                Window.GetWindow(this).Left = mousePosition.X - targetHorizontal;
                Window.GetWindow(this).Top = mousePosition.Y - targetVertical;
            }

            if (e.LeftButton == MouseButtonState.Pressed) Window.GetWindow(this).DragMove();
            Window.GetWindow(this).WindowStyle = WindowStyle.SingleBorderWindow;
        }

        public void WindowStateChanged(WindowState state)
        {
            if (Window.GetWindow(this).WindowState == WindowState.Maximized)
            {
                btn_maximize.Content = "\uE923";
                titleBar.Height = 24;
            }
            else if (Window.GetWindow(this).WindowState == WindowState.Normal)
            {
                btn_maximize.Content = "\uE922";
                titleBar.Height = 32;
            }
        }
    }
}
