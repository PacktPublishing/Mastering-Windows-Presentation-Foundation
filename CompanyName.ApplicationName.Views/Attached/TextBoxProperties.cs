using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CompanyName.ApplicationName.Views.Attached
{
    /// <summary>
    /// Contains attached properties for the System.Windows.Controls.TextBox control.
    /// </summary>
    public class TextBoxProperties : DependencyObject
    {
        #region IsNumericOnly

        /// <summary>
        /// Provides the ability to restrict the text input of the TextBox control to only allow numeric values to be entered.
        /// </summary>
        public static readonly DependencyProperty IsNumericOnlyProperty = DependencyProperty.RegisterAttached("IsNumericOnly", typeof(bool), typeof(TextBoxProperties), new UIPropertyMetadata(default(bool), OnIsNumericOnlyChanged));

        /// <summary>
        /// Gets the value of the IsNumericOnly property.
        /// </summary>
        /// <param name="dependencyObject">The DependencyObject to return the IsNumericOnly property value from.</param>
        /// <returns>The value of the IsNumericOnly property.</returns>
        public static bool GetIsNumericOnly(DependencyObject dependencyObject)
        {
            return (bool)dependencyObject.GetValue(IsNumericOnlyProperty);
        }

        /// <summary>
        /// Sets the value of the IsNumericOnly property.
        /// </summary>
        /// <param name="dependencyObject">The DependencyObject to set the IsNumericOnly property value of.</param>
        /// <param name="value">The value to be assigned to the IsNumericOnly property.</param>
        public static void SetIsNumericOnly(DependencyObject dependencyObject, bool value)
        {
            dependencyObject.SetValue(IsNumericOnlyProperty, value);
        }

        /// <summary>
        /// Adds key listening event handlers to the TextBox object to prevent non numeric key strokes from being accepted if the IsNumericOnly property value is true, or removes them otherwise.
        /// </summary>
        /// <param name="dependencyObject">The TextBox object.</param>
        /// <param name="e">The DependencyPropertyChangedEventArgs object containing event specific information.</param>
        public static void OnIsNumericOnlyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            TextBox textBox = dependencyObject as TextBox;
            bool newIsNumericOnlyValue = (bool)e.NewValue;
            if (newIsNumericOnlyValue)
            {
                textBox.PreviewTextInput += TextBox_PreviewTextInput;
                textBox.PreviewKeyDown += NumericTextBox_PreviewKeyDown;
                DataObject.AddPastingHandler(textBox, TextBox_Pasting);
            }
            else
            {
                textBox.PreviewTextInput -= TextBox_PreviewTextInput;
                textBox.PreviewKeyDown -= NumericTextBox_PreviewKeyDown;
                DataObject.RemovePastingHandler(textBox, TextBox_Pasting);
            }
        }

        private static void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            string text = GetFullText((TextBox)sender, e.Text);
            e.Handled = !IsTextValid(text);
        }

        private static void NumericTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            e.Handled = e.Key == Key.Space || (textBox.Text.Length == 1 && (e.Key == Key.Delete || e.Key == Key.Back));
        }

        private static void TextBox_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                string text = GetFullText((TextBox)sender, (string)e.DataObject.GetData(typeof(string)));
                if (!IsTextValid(text)) e.CancelCommand();
            }
            else e.CancelCommand();
        }

        private static bool IsTextValid(string text)
        {
            return Regex.Match(text, @"^-?\d*\.?\d*$").Success;
        }

        private static string GetFullText(TextBox textBox, string input)
        {
            return textBox.SelectedText.Length > 0 ? string.Concat(textBox.Text.Substring(0, textBox.SelectionStart), input, textBox.Text.Substring(textBox.SelectionStart + textBox.SelectedText.Length)) : textBox.Text.Insert(textBox.SelectionStart, input);
        }

        #endregion

        #region OnEnterKeyDown

        ///// <summary>
        ///// Provides a TextBox with a bindable ICommand property that will be executed when the TextBox.PreviewKeyDown event registers that the Enter key has been pressed.
        ///// </summary>
        public static DependencyProperty OnEnterKeyDownProperty = DependencyProperty.RegisterAttached("OnEnterKeyDown", typeof(ICommand), typeof(TextBoxProperties), new
            PropertyMetadata(OnOnEnterKeyDownChanged));

        ///// <summary>
        ///// Gets the value of the OnEnterKeyDown property.
        ///// </summary>
        ///// <param name="dependencyObject">The DependencyObject to return the OnEnterKeyDown property value from.</param>
        ///// <returns>The value of the OnEnterKeyDown property.</returns>
        public static ICommand GetOnEnterKeyDown(DependencyObject dependencyObject)
        {
            return (ICommand)dependencyObject.GetValue(OnEnterKeyDownProperty);
        }

        ///// <summary>
        ///// Sets the value of the OnEnterKeyDown property.
        ///// </summary>
        ///// <param name="dependencyObject">The DependencyObject to set the OnEnterKeyDown property value of.</param>
        ///// <param name="value">The value to be assigned to the OnEnterKeyDown property.</param>
        public static void SetOnEnterKeyDown(DependencyObject dependencyObject, ICommand value)
        {
            dependencyObject.SetValue(OnEnterKeyDownProperty, value);
        }

        ///// <summary>
        ///// Attaches an event handler for the TextBox.PreviewKeyDown event if an ICommand is bound to the OnEnterKeyDown property and removes it if an ICommand is removed.
        ///// </summary>
        ///// <param name="dependencyObject">The TextBox object.</param>
        ///// <param name="e">The DependencyPropertyChangedEventArgs object containing event specific information.</param>
        public static void OnOnEnterKeyDownChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            TextBox textBox = (TextBox)dependencyObject;
            if (e.OldValue == null && e.NewValue != null) textBox.PreviewKeyDown += TextBox_OnEnterKeyDown;
            else if (e.OldValue != null && e.NewValue == null) textBox.PreviewKeyDown -= TextBox_OnEnterKeyDown;
        }

        private static void TextBox_OnEnterKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                TextBox textBox = sender as TextBox;
                ICommand command = GetOnEnterKeyDown(textBox);
                if (command != null && command.CanExecute(textBox)) command.Execute(textBox);
            }
        }

        #endregion
    }
}