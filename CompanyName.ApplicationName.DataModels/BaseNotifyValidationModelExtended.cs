using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using CompanyName.ApplicationName.DataModels.Enums;
using CompanyName.ApplicationName.DataModels.Interfaces;
using CompanyName.ApplicationName.Extensions;

namespace CompanyName.ApplicationName.DataModels
{
    /// <summary>
    /// Provides data type objects with validation support, so that they can report when they have valiidation errors.
    /// </summary>
    /// <typeparam name="T">The type of the data model object requiring synchronisation support.</typeparam>
    public abstract class BaseNotifyValidationModelExtended<T> : BaseSynchronizableDataModel<T>, INotifyPropertyChanged, INotifyDataErrorInfo where T : BaseDataModel, ISynchronizableDataModel<T>, new()
    {
        private ValidationLevel validationLevel = ValidationLevel.Full;
        private Dictionary<string, List<string>> allPropertyErrors = new Dictionary<string, List<string>>();
        /// <summary>
        /// A read only collection of validation messages if there are any validation errors.
        /// </summary>
        protected ObservableCollection<string> errors = new ObservableCollection<string>();

        /// <summary>
        /// Initialises a new empty BaseNotifyValidationModel object.
        /// </summary>
        protected BaseNotifyValidationModelExtended()
        {
            ExternalErrors.CollectionChanged += ExternalErrors_CollectionChanged;
        }

        /// <summary>
        /// Gets or sets the ValidationLevel of the object that specifies the level of validation required on it.
        /// </summary>
        public ValidationLevel ValidationLevel
        {
            get { return validationLevel; }
            set { if (validationLevel != value) { validationLevel = value; } }
        }

        /// <summary>
        /// Gets a collection containing validation messages for this data model object if there are any validation errors.
        /// </summary>
        protected Dictionary<string, List<string>> AllPropertyErrors => allPropertyErrors;

        /// <summary>
        /// Gets a collection containing validation messages for this data model object if there are any validation errors.
        /// </summary>
        public virtual ObservableCollection<string> Errors
        {
            get
            {
                errors = new ObservableCollection<string>();
                IEnumerable<string> allErrors = AllPropertyErrors.Values.SelectMany(e => e);
                errors.Add(allErrors.Distinct());
                ExternalErrors.Where(e => !errors.Contains(e)).ForEach(e => errors.Add(e));
                return errors;
            }
        }

        /// <summary>
        /// Gets a collection of validation messages containing any external validation errors from View Models.
        /// </summary>
        public ObservableCollection<string> ExternalErrors { get; } = new ObservableCollection<string>();

        /// <summary>
        /// Gets the validation message relating to the propertyName input parameter if there are any validation errors.
        /// </summary>
        public abstract IEnumerable<string> this[string propertyName] { get; }

        /// <summary>
        /// Validates all of the validatable properties of the data model object.
        /// </summary>
        public abstract void ValidateAllProperties();

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
        /// Validates the value associated with each property name in the collection specified by the propertyNames input parameter.
        /// </summary>
        /// <param name="propertyNames"></param>
        public void Validate(params string[] propertyNames)
        {
            foreach (string propertyName in propertyNames) Validate(propertyName, this[propertyName]);
        }

        /// <summary>
        /// Updates the errors in the AllPropertyErrors property collection and raises the ErrorsChanged event dependent upon the contents of the collection specified by the errors input parameter.
        /// </summary>
        /// <param name="propertyName">The name of the property to validate.</param>
        /// <param name="errors">The collection of error messages relating to the property specified by the propertyName input parameter.</param>
        private void Validate(string propertyName, IEnumerable<string> errors)
        {
            if (ValidationLevel == ValidationLevel.None) return;
            ValidationContext validationContext = new ValidationContext(this);
            List<ValidationResult> validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(this, validationContext, validationResults, true);
            IEnumerable<ValidationResult> propertyValidationResults = validationResults.Where(v => v.MemberNames.Contains(propertyName));
            propertyValidationResults.ForEach(v => AddValidationError(propertyName, v.ErrorMessage));
            if (errors.Any()) errors.ForEach(e => AddValidationError(propertyName, e));
            else if (propertyValidationResults.Count() == 0 && AllPropertyErrors.ContainsKey(propertyName)) RemoveValidationError(propertyName);
            NotifyPropertyChanged(nameof(Errors), nameof(HasErrors));
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

        private void ExternalErrors_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            NotifyPropertyChanged(nameof(Errors), nameof(HasErrors));
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
        public bool HasErrors => ExternalErrors.Any() || allPropertyErrors.Any(p => p.Value != null && p.Value.Any()); 

        #endregion
    }
}