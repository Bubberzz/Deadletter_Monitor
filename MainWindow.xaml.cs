using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Deadletter_Monitor
{
    public partial class MainWindow
    {

        public MainWindow()
        {
            InitializeComponent();

            #region Dashboard
            // Loads data into 'Total Deadletters', 'Older than 24hrs' and 'Failed after reprocessing' charts
            var deadletterList = GetLiveDeadletters.GetFiles();
            Total.DataContext = deadletterList.Last();
            Aged.DataContext = deadletterList.Last();
            Failed.DataContext = deadletterList.Last();
            foreach (var data in deadletterList)
            {
                Clear1.Visibility = data.Total > 100 ? Visibility.Visible : Visibility.Hidden;
                Clear2.Visibility = data.AgedTotal > 100 ? Visibility.Visible : Visibility.Hidden;
                Clear3.Visibility = data.Failed > 100 ? Visibility.Visible : Visibility.Hidden;
            }
            
            // Loads data into 'This Week' chart
            var failed = new WeekViewModel.WeeklyDeadletters();
            ThisWeek.DataContext = new WeekViewModel(failed);

            // Loads data into 'Today' chart 
            var failed2 = new TodayViewModel.DailyDeadletters();
            Today.DataContext = new TodayViewModel(failed2);

            // Loads data into 'Files' chart 
            listViewInput.DataContext = new TypeViewModel();
            #endregion
        }

        #region Top Right Controls
        // Refreshes UI
        private void ButtonRefresh_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.ShutdownMode = ShutdownMode.OnLastWindowClose;
            var newWindow = new MainWindow();
            newWindow.Show();
            Close();
            configDeadletters.DataContext = new GetLiveDeadletters();
            deadletterLocations.DataContext = new GetLiveDeadletters();
            Username.Text = null;
            Password.Password = null;
        }

        // Minimises App
        private void ButtonMinimise_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        // Closes App
        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        #endregion

        #region Main menu
        // Displays Dashboard UI / Charts
        private void Button_Dashboard(object sender, RoutedEventArgs e)
        {
            var deadletterList = GetLiveDeadletters.GetFiles();
            Total.DataContext = deadletterList.Last();
            Aged.DataContext = deadletterList.Last();
            Failed.DataContext = deadletterList.Last();
            for (var index = 0; index < deadletterList.Count; index++)
            {
                var data = deadletterList[index];
                Clear1.Visibility = data.Total > 100 ? Visibility.Visible : Visibility.Hidden;
                Clear2.Visibility = data.AgedTotal > 100 ? Visibility.Visible : Visibility.Hidden;
                Clear3.Visibility = data.Failed > 100 ? Visibility.Visible : Visibility.Hidden;
            }
            var failed = new WeekViewModel.WeeklyDeadletters();
            ThisWeek.DataContext = new WeekViewModel(failed);
            var failed2 = new TodayViewModel.DailyDeadletters();
            Today.DataContext = new TodayViewModel(failed2);
            listViewInput.DataContext = new TypeViewModel();

            content_dataGrid.Visibility = Visibility.Hidden;
            content_alertsTop.Visibility = Visibility.Visible;
            content_radialGauge.Visibility = Visibility.Visible;
            reprocess_buttons.Visibility = Visibility.Hidden;
            config_LogIn.Visibility = Visibility.Hidden;
            config_dataGrid.Visibility = Visibility.Hidden;
            config_buttons.Visibility = Visibility.Hidden;
            Username.Text = null;
            Password.Password = null;
        }

        // Displays Reprocessing UI - DataGrid - Reprocess deadletters
        private void Button_Reprocess(object sender, RoutedEventArgs e)
        {
            content_alertsTop.Visibility = Visibility.Hidden;
            content_radialGauge.Visibility = Visibility.Hidden;
            content_dataGrid.Visibility = Visibility.Visible;
            reprocess_buttons.Visibility = Visibility.Visible;
            config_LogIn.Visibility = Visibility.Hidden;
            config_dataGrid.Visibility = Visibility.Hidden;
            config_buttons.Visibility = Visibility.Hidden;
            Username.Text = null;
            Password.Password = null;
            deadletterLocations.DataContext = new GetLiveDeadletters();
        }

        // Displays Configuration UI - DataGrid - Add/Remove file paths
        private void Button_Config(object sender, RoutedEventArgs e)
        {
            content_dataGrid.Visibility = Visibility.Hidden;
            content_alertsTop.Visibility = Visibility.Hidden;
            content_radialGauge.Visibility = Visibility.Hidden;
            reprocess_buttons.Visibility = Visibility.Hidden;
            config_LogIn.Visibility = Visibility.Visible;
            config_dataGrid.Visibility = Visibility.Hidden;
            config_buttons.Visibility = Visibility.Hidden;
            Username.Text = null;
            Password.Password = null;
        }
        #endregion

        #region Reprocess buttons
        // Reprocesses deadletters 
        private void Button_Reprocess_Deadletters(object sender, RoutedEventArgs e)
        {
            Deadletter_Monitor.Reprocess.reprocess();
            deadletterLocations.DataContext = new GetLiveDeadletters();
        }

        // Refreshes DataGrid with deadletters
        private void Button_Refresh(object sender, RoutedEventArgs e)
        {
            deadletterLocations.DataContext = new GetLiveDeadletters();
        }
        #endregion

        #region Config buttons
        private void Button_Add_Location(object sender, RoutedEventArgs e)
        {
            Add.add();
            configDeadletters.DataContext = new GetLiveDeadletters();
        }

        private void Button_Remove_Location(object sender, RoutedEventArgs e)
        {
            Remove.remove();
            configDeadletters.DataContext = new GetLiveDeadletters();
        }

        private void Button_LogIn(object sender, RoutedEventArgs e)
        {
            if (Username.Text == "admin")
            {
                if (Password.Password == "admin")
                {
                    config_LogIn.Visibility = Visibility.Hidden;
                    config_dataGrid.Visibility = Visibility.Visible;
                    config_buttons.Visibility = Visibility.Visible;
                    configDeadletters.DataContext = new GetLiveDeadletters();

                }
            }
        }
        #endregion

        // Allows window to be dragged
        private void GridToolbar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
