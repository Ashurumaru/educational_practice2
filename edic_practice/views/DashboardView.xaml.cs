using edic_practice.model;
using edic_practice.utilites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace edic_practice.views
{
    /// <summary>
    /// Логика взаимодействия для DashboardView.xaml
    /// </summary>
    public partial class DashboardView : Window
    {
        private List<User> UsersList;
        private List<Car> CarsList;
        private List<Rental> RentalsList;
        private List<CarMaintenence> MaintenanceList;
        private List<UserReview> ReviewList;
        public DashboardView()
        {
            InitializeComponent();
            LoadDatabase();
            Role.Text = $"Access level: {CurrentUser.Role}";
            Fio.Text = $"FIO: {CurrentUser.GetFullName()}";
        }

        private void LoadDatabase()
        {
            var context = new ed_practiceEntities();
            
            UsersList = context.Users.Include("Roles").Select(u => new User
            {
                UserID = u.UserID,
                Username = u.Username,
                Password = u.Password,
                FirstName = u.FirstName,
                SecondName = u.SecondName,
                Patronymic = u.Patronymic,
                Email = u.Email,
                RoleName = u.Roles.RoleName
            }).ToList();

            CarsList = context.Cars.Include("CarStatuses").Include("CarTypes").Select(c => new Car
            {
                CarID = c.CarID,
                TypeName = c.CarTypes.TypeName,
                Brand = c.Brand,
                Model = c.Model,
                CarYears = c.CarYears,
                LicensePlate = c.LicensePlate,
                StatusName = c.CarStatuses.StatusName
            }).ToList();

            RentalsList = context.Rentals.Select(r => new Rental
            {
                RentalID = r.RentalID,
                Model = r.Cars.Model,
                Username = r.Users.Username,
                RentalStartDate = r.RentalStartDate,
                RentalEndDate = r.RentalEndDate,
                IsReturned = r.IsReturned
            }).ToList();

            MaintenanceList = context.CarMaintenance.Include("Cars").Select(m => new CarMaintenence
            {
                MaintenanceID = m.MaintenanceID,
                Model = m.Cars.Model,
                MaintenanceStartDate = m.MaintenanceStartDate,
                MaintenanceEndDate = m.MaintenanceEndDate,
                Description = m.Description
            }).ToList();

            ReviewList = context.UserReviews.Include("Users").Select(r => new UserReview
            {
                ReviewID = r.ReviewID,
                Username = r.Users.Username,
                ReviewDate = r.ReviewDate,
                ReviewText = r.ReviewText,
                Rating = r.Rating
            }).ToList();
            DataGridUsers.ItemsSource = UsersList;
            DataGridCars.ItemsSource = CarsList;
            DataGridRentals.ItemsSource = RentalsList;
            DataGridMaintenance.ItemsSource = MaintenanceList;
            DataGridReviews.ItemsSource = ReviewList;
        }

        private void btn_leave_Click(object sender, RoutedEventArgs e)
        {
            LoginView loginView = new LoginView();
            loginView.Show();
            this.Close();
        }

        private void FilterTextBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            var filter = FilterTextBox1.Text.ToLower();
            var filteredList = UsersList.Where(user =>
                user.Username.ToLower().Contains(filter) ||
                user.FirstName.ToLower().Contains(filter) ||
                user.SecondName.ToLower().Contains(filter) ||
                user.Email.ToLower().Contains(filter)).ToList();

            DataGridUsers.ItemsSource = filteredList;
        }

        private void FilterTextBox2_TextChanged(object sender, TextChangedEventArgs e)
        {
            var filter = FilterTextBox2.Text.ToLower();
            var filteredList = CarsList.Where(car =>
                car.Brand.ToLower().Contains(filter) ||
                car.Model.ToLower().Contains(filter) ||
                (car.CarType != null && car.CarType.TypeName.ToLower().Contains(filter)) || 
                car.CarYears.ToString().Contains(filter) ||
                car.LicensePlate.ToLower().Contains(filter) ||
                (car.CarStatus != null && car.CarStatus.StatusName.ToLower().Contains(filter)) 
            ).ToList();

            DataGridCars.ItemsSource = filteredList;
        }

        private void FilterTextBox3_TextChanged(object sender, TextChangedEventArgs e)
        {
            var filter = FilterTextBox3.Text.ToLower();
            var filteredList = RentalsList.Where(rental =>
                rental.Car.Brand.ToLower().Contains(filter) || 
                rental.User.Username.ToLower().Contains(filter) ||
                rental.RentalStartDate.ToString().ToLower().Contains(filter) ||
                rental.RentalEndDate.ToString().ToLower().Contains(filter)).ToList();

            DataGridRentals.ItemsSource = filteredList;
        }

        private void FilterTextBox4_TextChanged(object sender, TextChangedEventArgs e)
        {
            var filter = FilterTextBox4.Text.ToLower();
            var filteredList = MaintenanceList.Where(maintenance =>
                maintenance.Description.ToLower().Contains(filter) ||
                maintenance.MaintenanceStartDate.ToString().ToLower().Contains(filter) ||
                maintenance.MaintenanceEndDate.ToString().ToLower().Contains(filter)).ToList();

            DataGridMaintenance.ItemsSource = filteredList;
        }

        private void FilterTextBox5_TextChanged(object sender, TextChangedEventArgs e)
        {
            var filter = FilterTextBox5.Text.ToLower();
            var filteredList = ReviewList.Where(review =>
                review.User.Username.ToLower().Contains(filter) || 
                review.ReviewText.ToLower().Contains(filter) ||
                review.ReviewDate.ToString().ToLower().Contains(filter)).ToList();

            DataGridReviews.ItemsSource = filteredList;
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

        private void btn_generate_report_cars_Click(object sender, RoutedEventArgs e)
        {
            ReportGenerator.GenerateCarsReport();
            StatusLabelGenerateCar.Text = "Отчёт успешно сгенерирован.";
        }
        private void btn_genarate_report_revenue_Click(object sender, RoutedEventArgs e)
        {
            if (StartDatePicker.SelectedDate == null || EndDatePicker.SelectedDate == null)
            {
                MessageBox.Show("Пожалуйста, выберите начальную и конечную дату.");
                return;
            }

            DateTime startDate = StartDatePicker.SelectedDate.Value;
            DateTime endDate = EndDatePicker.SelectedDate.Value;

            if (startDate > endDate)
            {
                MessageBox.Show("Начальная дата должна быть раньше конечной даты.");
                return;
            }

            ReportGenerator.GenerateRevenueReport(startDate, endDate);
            StatusLabelGenerateRevenue.Text = "Отчёт успешно сгенерирован.";
        }
    }
}
