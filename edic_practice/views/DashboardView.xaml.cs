using edic_practice.model;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace edic_practice.views
{
    /// <summary>
    /// Логика взаимодействия для DashboardView.xaml
    /// </summary>
    public partial class DashboardView : Window
    {
        public DashboardView()
        {
            InitializeComponent();
            DataGridUsers.ItemsSource = ed_practiceEntities.GetContext().Users.ToList();
            DataGridCars.ItemsSource = ed_practiceEntities.GetContext().Cars.ToList();
            DataGridRentals.ItemsSource = ed_practiceEntities.GetContext().Rentals.ToList();
            DataGridMaintenance.ItemsSource = ed_practiceEntities.GetContext().CarMaintenance.ToList();
            DataGridReviews.ItemsSource = ed_practiceEntities.GetContext().UserReviews.ToList();
            Role.Text = $"Access level: {CurrentUser.Role}";
            Fio.Text = $"FIO: {CurrentUser.GetFullName()}";
        }

        private void btn_leave_Click(object sender, RoutedEventArgs e)
        {
            LoginView loginView = new LoginView();
            loginView.Show();
            this.Close();
        }


        private void btn_close_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Close();
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

        private void TabItem_LogOut_click(object sender, MouseButtonEventArgs e)
        {
            LoginView loginView = new LoginView();
            loginView.Show();
            Application.Current.Shutdown();
        }
    }
}
