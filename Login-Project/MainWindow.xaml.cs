using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace Login_Project
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xamlcv
    /// </summary>

    public partial class MainWindow : Window
    { 
        public Login login = null;
        public Register register = null;
        public ForgottenPassword forgottenpassword = null;
        public AdminOverview adminoverview = null;
        public UserInformation userinformation = null;

        private DispatcherTimer loadingTimer;
        private int ellipsisCount = 1;


        public MainWindow()
        {
            InitializeComponent();
            StartProgressBar();
            StartLoadingAnimation();

            login = new Login(this);
            register = new Register(this);
            forgottenpassword = new ForgottenPassword(this);
            adminoverview = new AdminOverview(this);
            userinformation = new UserInformation(this);

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            labelZeit.Content = DateTime.Now.ToLongTimeString();
        }

        private void schließen_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void minimeren_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private async void StartProgressBar()
        {
            await FillProgressBarAsync(2000);

            content.Content = new Login(this);
        }

        private async Task FillProgressBarAsync(int duration)
        {
            int interval = 100;
            int steps = duration / interval;

            for (int i = 0; i < steps; i++)
            {
                progressBar.Value += interval;
                await Task.Delay(interval);
            }

            progressBar.Value = progressBar.Maximum;
            progressBar.Visibility = Visibility.Collapsed;
            labelLoading.Visibility = Visibility.Collapsed;
        }

        private void StartLoadingAnimation()
        {
            loadingTimer = new DispatcherTimer();
            loadingTimer.Interval = TimeSpan.FromSeconds(0.25);
            loadingTimer.Tick += LoadingTimer_Tick;
            loadingTimer.Start();
        }

        private void LoadingTimer_Tick(object sender, EventArgs e)
        {
            ellipsisCount = (ellipsisCount + 1) % 3;
            ellipsis.Text = new string('.', ellipsisCount + 1);
        }

        private void Window_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
    }
}