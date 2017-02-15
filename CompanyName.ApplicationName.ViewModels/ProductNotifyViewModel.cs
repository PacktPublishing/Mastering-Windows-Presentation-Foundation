using System;
using System.Linq;
using CompanyName.ApplicationName.DataModels;
using CompanyName.ApplicationName.DataModels.Collections;

namespace CompanyName.ApplicationName.ViewModels
{
    /// <summary>
    /// Provides a collection of validatable ProductNotify objects to be edited in the UI.
    /// </summary>
    public class ProductNotifyViewModel : BaseViewModel
    {
        private ProductsNotify products = new ProductsNotify();

        /// <summary>
        /// Initialises a new ProductNotifyViewModel object with default values.
        /// </summary>
        public ProductNotifyViewModel()
        {
            Products.Add(new ProductNotify() { Id = Guid.NewGuid(), Name = "Virtual Reality Headset", Price = 14.99m });
            Products.Add(new ProductNotify() { Id = Guid.NewGuid(), Name = "Virtual Reality Headset" });
            Products.CurrentItem = Products.Last();
        }

        /// <summary>
        /// Gets or sets the ProductsNotify collection of validatable ProductNotify objects to be edited in the UI.
        /// </summary>
        public ProductsNotify Products
        {
            get { return products; }
            set { if (products != value) { products = value; NotifyPropertyChanged(); } }
        }
    }
}