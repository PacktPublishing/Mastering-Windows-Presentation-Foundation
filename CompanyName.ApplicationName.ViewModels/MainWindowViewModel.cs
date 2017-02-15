using CompanyName.ApplicationName.ViewModels.Properties;

namespace CompanyName.ApplicationName.ViewModels
{
    /// <summary>
    /// Serves the MainWindow class with its data via the ViewModel property and provides custom user settings management.
    /// </summary>
    public class MainWindowViewModel : BaseViewModel
    {
        private BaseViewModel viewModel;

        /// <summary>
        /// Initialises a new MainWindowViewModel with default values.
        /// </summary>
        public MainWindowViewModel() : base()
        {
            ViewModel = new TextViewModel();
        }

        /// <summary>
        /// Gets or sets the BaseViewModel object that is currently displayed in the application.
        /// </summary>
        public BaseViewModel ViewModel
        {
            get { return viewModel; }
            set { if (viewModel != value) { viewModel = value; NotifyPropertyChanged(); } }
        }

        /// <summary>
        /// Loads the default project settings.
        /// </summary>
        public void LoadSettings()
        {
            Settings.Default.Reload();
            StateManager.AreAuditFieldsVisible = Settings.Default.AreAuditFieldsVisible;
            StateManager.AreSearchTermsSaved = Settings.Default.AreSearchTermsSaved;
        }

        /// <summary>
        /// Saves the default project settings.
        /// </summary>
        public void SaveSettings()
        {
            Settings.Default.AreAuditFieldsVisible = StateManager.AreAuditFieldsVisible;
            Settings.Default.AreSearchTermsSaved= StateManager.AreSearchTermsSaved;
            Settings.Default.Save();
        }
    }
}