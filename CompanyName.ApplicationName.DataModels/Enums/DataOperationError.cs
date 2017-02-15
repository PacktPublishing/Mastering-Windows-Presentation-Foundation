using System.ComponentModel;

namespace CompanyName.ApplicationName.DataModels.Enums
{
    /// <summary>
    /// Represents the various data operation errors used in the application.
    /// </summary>
    public enum DataOperationError
    {
        /// <summary>
        /// Represents a situation where there is no data operation error.
        /// </summary>
        [Description("")]
        None = 0,
        /// <summary>
        /// Represents a situation where one or more database table constraints have not been adhered to.
        /// </summary>
        [Description("A database constraint has not been adhered to, so this operation cannot be completed")]
        DatabaseConstraintError = 9995,
        /// <summary>
        /// Represents a situation where there was an undetermined data operation error.
        /// </summary>
        [Description("There was an undetermined data operation error")]
        UndeterminedDataOperationError = 9997,
        /// <summary>
        /// Represents a situation where there was a problem connecting to the database.
        /// </summary>
        [Description("There was a problem connecting to the database")]
        DatabaseConnectionError = 9998,
    }
}