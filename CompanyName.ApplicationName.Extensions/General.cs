using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CompanyName.ApplicationName.Extensions
{
    /// <summary>
    /// Provides a variety of commonly used methods that extend the functionality of the .NET Framework classes.
    /// </summary>
    public static class General
    {
        /// <summary>
        /// Returns a new IEnumerable&lt;T&gt; collection containing all of the items in this collection plus the item specified by the item input parameter.
        /// </summary>
        /// <typeparam name="T">The type of the IEnumerable&lt;T&gt; collection.</typeparam>
        /// <param name="collection">The this IEnumerable&lt;T&gt; collection.</param>
        /// <param name="item">The item to add to the IEnumerable&lt;T&gt; collection.</param>
        /// <returns>A new IEnumerable&lt;T&gt; collection containing all of the items in this collection plus the item specified by the item input parameter.</returns>
        /// <remarks>This method works by returning a new IEnumerable&lt;T&gt; collection including the new item rather than simply adding it to the collection which is impossible.</remarks>
        public static IEnumerable<T> Add<T>(this IEnumerable<T> collection, T item)
        {
            foreach (T currentItem in collection) yield return currentItem;
            yield return item;
        }

        /// <summary>
        /// Adds the items from the collection specified by the range input parameter into this collection.
        /// </summary>
        /// <typeparam name="T">The type of items inside the collections.</typeparam>
        /// <param name="collection">This collection.</param>
        /// <param name="range">The collection containing the items to add to this collection.</param>
        public static void Add<T>(this ICollection<T> collection, IEnumerable<T> range)
        {
            for (int index = 0; index < range.Count(); index++) collection.Add(range.ElementAt(index));
        }

        /// <summary>
        /// Adds the items from the collection specified by the range input parameter into this collection.
        /// </summary>
        /// <typeparam name="T">The type of items inside the collections.</typeparam>
        /// <param name="collection">This collection.</param>
        /// <param name="range">The collection containing the items to add to this collection.</param>
        public static void AddRange<T>(this ICollection<T> collection, ICollection<T> range)
        {
            foreach (T item in range) collection.Add(item);
        }

        /// <summary>
        /// Adds the text as an item into the collection if it does not already exist in it.
        /// </summary>
        /// <param name="collection">The this collection.</param>
        /// <param name="text">The text to add as an item if it is unique.</param>
        public static void AddUniqueIfNotEmpty(this ObservableCollection<string> collection, string text)
        {
            if (!string.IsNullOrEmpty(text) && !collection.Contains(text)) collection.Add(text);
        }

        /// <summary>
        /// Appends the text input parameter to the end of the StringBuilder text if it does not already contain it.
        /// </summary>
        /// <param name="stringBuilder">The this StringBuilder.</param>
        /// <param name="text">The text to append to the StringBuilder object if it is unique.</param>
        public static void AppendUniqueOnNewLineIfNotEmpty(this StringBuilder stringBuilder, string text)
        {
            if (text.Trim().Length > 0 && !stringBuilder.ToString().Contains(text)) stringBuilder.AppendFormat("{0}{1}", stringBuilder.ToString().Trim().Length == 0 ? string.Empty : Environment.NewLine, text);
        }

        /// <summary>
        /// Returns the number of items in the collection specified by the collection input parameter.
        /// </summary>
        /// <param name="collection">The collection to return the number items of.</param>
        /// <returns>The number of items in the collection specified by the collection input parameter.</returns>
        public static int Count(this IEnumerable collection)
        {
            int count = 0;
            foreach (object item in collection) count++;
            return count;
        }

        /// <summary>
        /// Returns a generic ICollection containing an item for each member of the type of enum specified by the genric T type parameter. This method will throw an ArgumentException if the the provided genric T type parameter is not an enum.
        /// </summary>
        /// <typeparam name="T">The type of enum to fill the collection with.</typeparam>
        /// <param name="collection">The this ICollection.</param>
        /// <exception cref="ArgumentException" />
        public static void FillWithMembers<T>(this ICollection<T> collection)
        {
            if (typeof(T).BaseType != typeof(Enum)) throw new ArgumentException("The FillWithMembers<T> method can only be called with an enum as the generic type.");
            collection.Clear();
            foreach (string name in Enum.GetNames(typeof(T))) collection.Add((T)Enum.Parse(typeof(T), name));
        }

        /// <summary>
        /// Returns the content of the System.ComponentModel.DescriptionAttribute that relates to this enum if one is set, or the name of the enum otherwise.
        /// </summary>
        /// <param name="value">The this enum.</param>
        /// <returns>The content of the System.ComponentModel.DescriptionAttribute that relates to this enum if one is set, or the name of the enum otherwise.</returns>
        public static string GetDescription(this Enum value)
        {
            FieldInfo fieldInfo = value.GetType().GetField(value.ToString());
            if (fieldInfo == null) return Enum.GetName(value.GetType(), value);
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes != null && attributes.Length > 0) return attributes[0].Description;
            return Enum.GetName(value.GetType(), value);
        }

        /// <summary>
        /// Removes the items from the collection specified by the range input parameter from this collection.
        /// </summary>
        /// <typeparam name="T">The type of items inside the collections.</typeparam>
        /// <param name="collection">This collection.</param>
        /// <param name="range">The collection containing the items to remove from this collection.</param>
        public static void Remove<T>(this ICollection<T> collection, IEnumerable<T> range)
        {
            for (int index = 0; index < range.Count(); index++) collection.Remove(range.ElementAt(index));
        }

        /// <summary>
        /// Populates and returns a generic ObservableCollection from the items in the generic IEnumerable
        /// </summary>
        /// <typeparam name="T">Any generic type.</typeparam>
        /// <param name="inputCollection">This generic IEnumerable.</param>
        /// <returns>A generic ObservableCollection from the items in the generic IEnumerable.</returns>
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> inputCollection)
        {
            return new ObservableCollection<T>(inputCollection);
        }

        #region LINQ Extentions

        /// <summary>
        /// Returns elements with distinct property values from a sequence by using the default equality comparer to compare values.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source collection.</typeparam>
        /// <typeparam name="TKey">The type of the property key that will be used to find distinct values for.</typeparam>
        /// <param name="source">The sequence to remove duplicate elements from.</param>
        /// <param name="keySelector">The property key that will be used to find distinct values for.</param>
        /// <returns>An System.Collections.Generic.IEnumerable&lt;T&gt; that contains distinct elements from the source sequence.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown if the source collection is null.</exception>
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> keys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (keys.Add(keySelector(element))) yield return element;
            }
        }

        #endregion
    }
}