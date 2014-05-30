using System;
using System.Collections.Generic;

namespace Liv.io.Utils
{
	public class IntUniqueIdProvider : IUniqueIdProvider
	{
		public FilesystemIntUniqueIdBlockProvider IdBlockProvider {
			get;
			set;
		}

		 Queue<string> _remainingIdQueue;

		public IntUniqueIdProvider ()
		{
			IdBlockProvider = new FilesystemIntUniqueIdBlockProvider ();
		}

		#region IUniqueIdProvider implementation

		public string GetUniqueId ()
		{
			lock (_remainingIdQueue) {
				if (_remainingIdQueue == null || _remainingIdQueue.Count == 0)
					_remainingIdQueue = IdBlockProvider.GetUniqueIdBlock ();

				return _remainingIdQueue.Dequeue ();
			}
		}

		#endregion
	}
}