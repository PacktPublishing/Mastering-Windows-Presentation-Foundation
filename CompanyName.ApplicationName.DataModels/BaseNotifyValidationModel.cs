using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using CompanyName.ApplicationName.Extensions;

namespace CompanyName.ApplicationName.DataModels
{
    /// <summary>
    /// Provides data type objects with validation support, so that they can report when they have valiidation errors.
    /// </summary>
    public abstract class BaseNotifyValidationModel : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        /// <summary>
        /// A read only collection of validation messages if there are any validation errors.
        /// </summary>
        private Dictionary<string, List<string>> allPropertyErrors = new Dictionary<string, List<string>>();

        /// <summary>
        /// Gets a collection containing validation messages for this data model object if there are any validation errors.
        /// </summary>
        protected Dictionary<string, List<string>> AllPropertyErrors => allPropertyErrors;

        /// <summary>
        /// Gets the validation message relating to the propertyName input parameter if there are any validation errors.
        /// </summary>
        public abstract IEnumerable<string> this[string propertyName] { get; }

        /// <summary>
        /// Raises the PropertyChanged event alerting the WPF Framework to update the UI and then validates each of the object properties specified by the propertyNames input parameter.
        /// </summary>
        /// <param name="propertyNames">The names of the properties to update in the UI.</param>
        public void NotifyPropertyChangedAndValidate(params string[] propertyNames)
        {
            foreach (string propertyName in propertyNames) NotifyPropertyChangedAndValidate(propertyName);
        }

        /// <summary>
        /// Raises the PropertyChanged event alerting the WPF Framework to update the UI and then validates each of the object properties specified by the propertyNames input parameter.
        /// </summary>
        /// <param name="propertyName">The optional name of the property to update in the UI. If this is left blank, the name will be taken from the calling member via the CallerMemberName attribute.</param>
        public void NotifyPropertyChangedAndValidate([CallerMemberName]string propertyName = "")
        {
            NotifyPropertyChanged(propertyName);
            Validate(propertyName, this[propertyName]);
        }

        /// <summary>
        /// Updates the errors in the AllPropertyErrors property collection and raises the ErrorsChanged event dependent upon the contents of the collection specified by the errors input parameter.
        /// </summary>
        /// <param name="propertyName">The name of the property to validate.</param>
        /// <param name="errors">The collection of error messages relating to the property specified by the propertyName input parameter.</param>
        public void Validate(string propertyName, IEnumerable<string> errors)
        {
            if (errors.Any()) errors.ForEach(e => AddValidationError(propertyName, e));
            else if (AllPropertyErrors.ContainsKey(propertyName)) RemoveValidationError(propertyName);
        }

        private void AddValidationError(string propertyName, string error)
        {
            if (AllPropertyErrors.ContainsKey(propertyName))
            {
                if (!AllPropertyErrors[propertyName].Contains(error))
                {
                    AllPropertyErrors[propertyName].Add(error);
                    OnErrorsChanged(propertyName);
                }
            }
            else
            {
                AllPropertyErrors.Add(propertyName, new List<string>() { error });
                OnErrorsChanged(propertyName);
            }
        }

        private void RemoveValidationError(string propertyName)
        {
            AllPropertyErrors.Remove(propertyName);
            OnErrorsChanged(propertyName);
        }

        #region INotifyDataErrorInfo Members

        /// <summary>
        /// The event that is raised when the validation errors have changed for a property or for the entire entity. 
        /// </summary>
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        /// <summary>
        /// Raises the ErrorsChanged event.
        /// </summary>
        /// <param name="propertyName">The name of the property that the error relates to.</param>
        public void OnErrorsChanged(string propertyName) => ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));

        /// <summary>
        /// Gets the validation errors for a specified property or for the entire entity.
        /// </summary>
        /// <param name="propertyName">The name of the property to retrieve validation errors for; or null or Empty, to retrieve entity-level errors.</param>
        /// <returns>The validation errors for the property or entity.</returns>
        public IEnumerable GetErrors(string propertyName)
        {
            List<string> propertyErrors = new List<string>();
            if (string.IsNullOrEmpty(propertyName)) return propertyErrors;
            allPropertyErrors.TryGetValue(propertyName, out propertyErrors);
            return propertyErrors;
        }

        /// <summary>
        /// Gets a value that indicates whether the entity has validation errors. 
        /// </summary>
        public bool HasErrors => allPropertyErrors.Any(p => p.Value != null && p.Value.Any());

        #endregion

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