using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Liv.io.Utils {
	/// <summary>
	/// Extensions for the <see cref="IDictionary&lt;TKey,TValue&gt;"/> interface
	/// </summary>
	public static class DictionaryExtensions {
		/// <summary>
		/// Tries to add a value to the dictionary if the value does not yet exist within the dictionary
		/// </summary>
		/// <typeparam name="TKey">The type of the dictionaries keys</typeparam>
		/// <typeparam name="TValue">The type of the dictionaries values</typeparam>
		/// <param name="dict">The dictionary</param>
		/// <param name="key">The key to be added</param>
		/// <param name="value">The value to be added for the key</param>
		/// <returns>True if the key/value pair was added; otherwise false</returns>
		public static bool TryAdd<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, TValue value) {
			if (dict.ContainsKey(key))
				return false;
			dict.Add(key, value);
			return true;
		}

		/// <summary>
		/// Gets an item from the dictionary. If the item was not yet present within the dictionary
		/// the specified factory method will be used to create the value
		/// </summary>
		/// <typeparam name="TKey">The type of the dictionaries keys</typeparam>
		/// <typeparam name="TValue">The type of the dictionaries values</typeparam>
		/// <param name="dict">The dictionary</param>
		/// <param name="key">The key to get a value for</param>
		/// <param name="factory">The factory to create a new value</param>
		/// <returns>The value</returns>
		public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, Func<TKey, TValue> factory) {
			TValue value;
			if (dict.TryGetValue(key, out value))
				return value;
			value = factory(key);
			dict.Add(key, value);
			return value;
		}

		/// <summary>
		/// Tries to remove a value from the dictionary
		/// </summary>
		/// <typeparam name="TKey">The type of the dictionaries keys</typeparam>
		/// <typeparam name="TValue">The type of the dictionaries values</typeparam>
		/// <param name="dict">The dictionary</param>
		/// <param name="key">The key to remove the value for</param>
		/// <param name="value">The removed value</param>
		/// <returns>True if the key was present in the dictionary; otherwise false</returns>
		public static bool TryRemove<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, out TValue value) {
			if (dict.TryGetValue(key, out value)) {
				dict.Remove(key);
				return true;
			}
			return false;
		}
	}
}