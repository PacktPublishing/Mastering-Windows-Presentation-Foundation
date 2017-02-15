using System;
using System.ComponentModel;
using System.Linq;
using CompanyName.ApplicationName.DataModels;
using CompanyName.ApplicationName.DataModels.Collections;

namespace CompanyName.ApplicationName.ViewModels
{
    /// <summary>
    /// Provides a collection of validatable ProductNotifyExtended objects to be edited in the UI.
    /// </summary>
    public class ProductNotifyViewModelExtended : BaseViewModel
    {
        private ProductsNotifyExtended products = new ProductsNotifyExtended();

        /// <summary>
        /// Initialises a new ProductNotifyViewModelExtended object with default values.
        /// </summary>
        public ProductNotifyViewModelExtended()
        {
            Products.Add(new ProductNotifyExtended() { Id = Guid.NewGuid(), Name = "Virtual Reality Headset", Price = 14.99m });
            Products.Add(new ProductNotifyExtended() { Id = Guid.NewGuid(), Name = "Virtual Reality Headset" });
            Products.CurrentItemChanged += Products_CurrentItemChanged;
            Products.CurrentItem = Products.Last();
            //Products.CurrentItem.Validate(nameof(Products.CurrentItem.Name), nameof(Products.CurrentItem.Price));
            ValidateUniqueName(Products.CurrentItem);
        }

        /// <summary>
        /// Gets or sets the ProductsNotifyExtended collection of validatable ProductNotifyExtended objects to be edited in the UI.
        /// </summary>
        public ProductsNotifyExtended Products
        {
            get { return products; }
            set { if (products != value) { products = value; NotifyPropertyChanged(); } }
        }

        private void Products_CurrentItemChanged(ProductNotifyExtended oldProduct, ProductNotifyExtended newProduct)
        {
            if (newProduct != null) newProduct.PropertyChanged += Product_PropertyChanged;
            if (oldProduct != null) oldProduct.PropertyChanged -= Product_PropertyChanged;
        }

        private void Product_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Name") ValidateUniqueName(Products.CurrentItem);
        }

        private void ValidateUniqueName(ProductNotifyExtended product)
        {
            string errorMessage = "The product name must be unique";
            if (!IsProductNameUnique(product)) product.ExternalErrors.Add(errorMessage);
            else product.ExternalErrors.Remove(errorMessage);
        }

        private bool IsProductNameUnique(ProductNotifyExtended product)
        {
            return Products.Count(p => p.Id != product.Id && p.Name != string.Empty && p.Name == product.Name) == 0;
        }
    }
}