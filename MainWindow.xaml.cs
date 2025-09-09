using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ContractMonthlyClaimSystem2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Get values from UI controls
                string email = txtEmail.Text.Trim();
                string password = txtPassword.Password;
                string role = (cmbRole.SelectedItem as ComboBoxItem)?.Content.ToString();

                // Validate input
                if (string.IsNullOrEmpty(email))
                {
                    MessageBox.Show("Please enter your email address.", "Validation Error",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    txtEmail.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("Please enter your password.", "Validation Error",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    txtPassword.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(role))
                {
                    MessageBox.Show("Please select your role.", "Validation Error",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    cmbRole.Focus();
                    return;
                }

                // Check credentials for Lecturer
                if (email.ToLower() == "lecturer@test.com" && password == "123" && role == "Lecturer")
                {
                    // Create and show the Dashboard window for Lecturer
                    DashboardWindow dashboardWindow = new DashboardWindow("Lecturer");
                    dashboardWindow.Show();
                    this.Close();
                }
                // Check credentials for Academic Manager/Admin
                else if (email.ToLower() == "admin@test.com" && password == "123" && role == "Academic Manager")
                {
                    // Create and show the Dashboard window for Admin
                    DashboardWindow dashboardWindow = new DashboardWindow("Admin");
                    dashboardWindow.Show();
                    this.Close();
                }
                // Check credentials for Programme Coordinator
                else if (email.ToLower() == "coordinator@test.com" && password == "123" && role == "Programme Coordinator")
                {
                    // Create and show the Dashboard window for Coordinator
                    DashboardWindow dashboardWindow = new DashboardWindow("Coordinator");
                    dashboardWindow.Show();
                    this.Close();
                }
                else
                {
                    // Invalid credentials
                    MessageBox.Show("Invalid email, password, or role selection.\n\nValid credentials:\n" +
                                   "Lecturer: lecturer@test.com / 123\n" +
                                   "Admin: admin@test.com / 123\n" +
                                   "Coordinator: coordinator@test.com / 123",
                                   "Login Failed",
                                   MessageBoxButton.OK,
                                   MessageBoxImage.Error);

                    // Clear password field for security
                    txtPassword.Clear();
                    txtEmail.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred during login: {ex.Message}",
                               "Error",
                               MessageBoxButton.OK,
                               MessageBoxImage.Error);
            }
        }

        private void TxtEmail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txtPassword.Focus();
            }
        }

        private void TxtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Login_Click(sender, e);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtEmail.Focus();
        }
    }

    /// <summary>
    /// Dashboard Window - front-end prototype
    /// </summary>
    public partial class DashboardWindow : Window
    {
        private string _userRole;

        public DashboardWindow(string userRole)
        {
            _userRole = userRole;
            InitializeComponent();
            SetupDashboard();
        }

        private void SetupDashboard()
        {
            this.Title = $"CMCS Dashboard - {_userRole}";
        }

        private void InitializeComponent()
        {
            // Window setup
            this.Width = 1100;
            this.Height = 700;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.Background = new SolidColorBrush(Color.FromRgb(240, 242, 245));

            // Root grid with 2 columns (sidebar + main content)
            Grid rootGrid = new Grid();
            this.Content = rootGrid;

            rootGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(220) });
            rootGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            // ---------------- Sidebar ----------------
            StackPanel sidebar = new StackPanel
            {
                Background = new SolidColorBrush(Color.FromRgb(44, 62, 80)),
                Orientation = Orientation.Vertical
            };
            Grid.SetColumn(sidebar, 0);
            rootGrid.Children.Add(sidebar);

            // Sidebar title
            TextBlock sidebarTitle = new TextBlock
            {
                Text = "CMCS",
                Foreground = Brushes.White,
                FontSize = 28,
                FontWeight = FontWeights.Bold,
                Margin = new Thickness(20, 30, 0, 30)
            };
            sidebar.Children.Add(sidebarTitle);

            // Sidebar buttons
            sidebar.Children.Add(CreateSidebarButton("Lecturer"));
            sidebar.Children.Add(CreateSidebarButton("Academic Managers"));
            sidebar.Children.Add(CreateSidebarButton("Claims"));
            sidebar.Children.Add(CreateSidebarButton("Approvals"));

            // Spacer
            sidebar.Children.Add(new TextBlock { Height = 50 });

            // Footer
            TextBlock footer = new TextBlock
            {
                Text = "Consistent & Reliable",
                Foreground = Brushes.LightGray,
                FontSize = 14,
                Margin = new Thickness(20, 40, 0, 0)
            };
            sidebar.Children.Add(footer);

            // ---------------- Main Content ----------------
            StackPanel mainPanel = new StackPanel
            {
                Margin = new Thickness(40),
                HorizontalAlignment = HorizontalAlignment.Center
            };
            Grid.SetColumn(mainPanel, 1);
            rootGrid.Children.Add(mainPanel);

            // Title
            TextBlock title = new TextBlock
            {
                Text = "Contract Monthly Claim System",
                FontSize = 28,
                FontWeight = FontWeights.Bold,
                Foreground = new SolidColorBrush(Color.FromRgb(44, 62, 80)),
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(0, 0, 0, 20)
            };
            mainPanel.Children.Add(title);

            // Subtitle
            TextBlock subtitle = new TextBlock
            {
                Text = "This is a front-end prototype only. Features will on Part 2.",
                FontSize = 16,
                Foreground = Brushes.Gray,
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(0, 0, 0, 40)
            };
            mainPanel.Children.Add(subtitle);

            // Big colorful buttons in a WrapPanel
            WrapPanel buttonPanel = new WrapPanel
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                ItemWidth = 200,
                ItemHeight = 100,
                Margin = new Thickness(0, 20, 0, 0)
            };
            mainPanel.Children.Add(buttonPanel);

            buttonPanel.Children.Add(CreateMainButton("Submit Claims", Color.FromRgb(52, 152, 219)));
            buttonPanel.Children.Add(CreateMainButton("Verify & Approve", Color.FromRgb(46, 204, 113)));
            buttonPanel.Children.Add(CreateMainButton("Upload Documents", Color.FromRgb(243, 156, 18)));
            buttonPanel.Children.Add(CreateMainButton("Track Status", Color.FromRgb(155, 89, 182)));
        }

        // Helper for sidebar buttons
        private Button CreateSidebarButton(string text)
        {
            return new Button
            {
                Content = text,
                Foreground = Brushes.White,
                Background = Brushes.Transparent,
                BorderBrush = Brushes.Transparent,
                FontSize = 16,
                Height = 40,
                Margin = new Thickness(10, 5, 10, 5),
                HorizontalAlignment = HorizontalAlignment.Stretch
            };
        }

        // Helper for main colorful buttons
        private Button CreateMainButton(string text, Color bgColor)
        {
            return new Button
            {
                Content = text,
                Background = new SolidColorBrush(bgColor),
                Foreground = Brushes.White,
                FontSize = 16,
                FontWeight = FontWeights.Bold,
                Width = 200,
                Height = 100,
                Margin = new Thickness(10),
                BorderThickness = new Thickness(0)
            };
        }
    }
}