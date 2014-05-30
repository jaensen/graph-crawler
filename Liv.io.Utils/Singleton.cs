using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Liv.io.Utils {

	/// <summary>
	/// Thread safe singleton pattern implementation
	/// </summary>
	/// <typeparam name="T">The type which should be provided as singleton</typeparam>
	public sealed class Singleton<T> where T : class, new() {
		private static object _internalSyncObject;
		private static T _instance;

		private static Object InternalSyncObject {
			get {
				if (_internalSyncObject == null) {
					Object obj = new Object();
					Interlocked.CompareExchange(ref _internalSyncObject, obj, null);
				}
				return _internalSyncObject;
			}
		}

		public static T Instance {
			get {
				if (_instance == null) {
					lock (InternalSyncObject) {
						if (_instance == null) {
							T tmp = new T();
							_instance = tmp;
						}
					}
				}
				return _instance;
			}
		}
	}
}