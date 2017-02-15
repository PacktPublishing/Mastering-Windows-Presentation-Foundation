using CompanyName.ApplicationName.DataModels.Enums;

namespace CompanyName.ApplicationName.DataModels.Interfaces
{
    /// <summary>
    /// Represents a dynamic, sortable data collection that provides notifications when items get added and removed, maintains a current item, provides members that enable the animation and synchronisation of its items.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    public interface ISynchronizableDataModel<T> where T : class, new()
    {
        /// <summary>
        /// Gets or sets a value that specifies the current state of the object.
        /// </summary>
        ObjectState ObjectState { get; set; }

        /// <summary>
        /// Gets a value that specifies whether any properties in any object in the collection have changed since they were last synchronised or not.
        /// </summary>
        bool HasChanges { get; }

        /// <summary>
        /// Gets a value that specifies whether the object has been synchronised or not.
        /// </summary>
        bool IsSynchronized { get; }

        /// <summary>
        /// Reverts the values of all of the properties in this object to the state that they were in when they were last synchronised.
        /// </summary>
        void RevertState();

        /// <summary>
        /// Synchronises the value of the internal synchronisation member with the current property values of this object.
        /// </summary>
        void Synchronize();

        /// <summary>
        /// Specifies whether the property values of the object are equal to the property values of the object specified by the dataModel input parameter, or not.
        /// </summary>
        /// <param name="dataModel">The object to compare with the current object.</param>
        /// <returns>True if the property values of the specified object are equal to the property values of the object specified by the dataModel input parameter, otherwise false.</returns>
        bool PropertiesEqual(T dataModel);

        /// <summary>
        /// Copies all of the values from the dataModel input parameter to this object.
        /// </summary>
        /// <param name="dataModel">The object to copy the values from.</param>
        void CopyValuesFrom(T dataModel);

        /// <summary>
        /// Returns a deep copy of the object being cloned.
        /// </summary>
        /// <returns>A deep copy of the object being cloned.</returns>
        T Clone();
    }
}