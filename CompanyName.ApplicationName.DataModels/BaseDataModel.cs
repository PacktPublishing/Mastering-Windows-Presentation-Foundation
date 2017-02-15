using System.ComponentModel;
using System.Runtime.CompilerServices;
using CompanyName.ApplicationName.Extensions;

namespace CompanyName.ApplicationName.DataModels
{
    /// <summary>
    /// Provides an implementation of the System.ComponentModel.INotifyPropertyChanged interface and requires the implementation of the object.ToString method.
    /// </summary>
    public abstract class BaseDataModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Initialises a new empty BaseDataModel object.
        /// </summary>
        protected BaseDataModel() { }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public abstract override string ToString();

        #region INotifyPropertyChanged Members

        /// <summary>
        /// The event that is raised when a property that calls the NotifyPropertyChanged method is changed.
        /// </summary>
        public virtual event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises the PropertyChanged event alerting the WPF Framework to update the UI.
        /// </summary>
        /// <param name="propertyNames">The names of the properties to update in the UI.</param>
        protected virtual void NotifyPropertyChanged(params string[] propertyNames)
        {
            if (PropertyChanged != null) propertyNames.ForEach(p => PropertyChanged(this, new PropertyChangedEventArgs(p)));
        }

        /// <summary>
        /// Raises the PropertyChanged event alerting the WPF Framework to update the UI.
        /// </summary>
        /// <param name="propertyName">The optional name of the property to update in the UI. If this is left blank, the name will be taken from the calling member via the CallerMemberName attribute.</param>
        protected virtual void NotifyPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}