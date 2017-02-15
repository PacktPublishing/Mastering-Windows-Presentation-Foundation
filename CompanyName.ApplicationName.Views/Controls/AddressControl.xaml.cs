using System.Windows;
using System.Windows.Controls;
using CompanyName.ApplicationName.DataModels;

namespace CompanyName.ApplicationName.Views.Controls
{
    /// <summary>
    /// Provides the ability to edit addresses.
    /// </summary>
    public partial class AddressControl : UserControl
    {
        /// <summary>
        /// Initialises a new empty AddressControl object with default values.
        /// </summary>
        public AddressControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Represents the Address object to edit in the View.
        /// </summary>
        public static readonly DependencyProperty AddressProperty = DependencyProperty.Register(nameof(Address), typeof(Address), typeof(AddressControl), new PropertyMetadata(new Address()));

        /// <summary>
        /// Gets or sets the Address object to edit in the View.
        /// </summary>
        public Address Address
        {
            get { return (Address)GetValue(AddressProperty); }
            set { SetValue(AddressProperty, value); }
        }
    }
}