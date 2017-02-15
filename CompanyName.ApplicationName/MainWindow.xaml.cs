using System;
using System.Windows;
using CompanyName.ApplicationName.ViewModels;

namespace CompanyName.ApplicationName
{
    /// <summary>
    /// The main display window for a WPF application
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Initialises a new MainWindow object.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
            Closed += MainWindow_Closed;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            MainWindowViewModel viewModel = new MainWindowViewModel();
            viewModel.LoadSettings();
            DataContext = viewModel;
        }

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            MainWindowViewModel viewModel = (MainWindowViewModel)DataContext;
            viewModel.SaveSettings();
        }
    }
}