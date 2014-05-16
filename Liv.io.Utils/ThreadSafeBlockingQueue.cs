using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

namespace Liv.io.Utils {
	public class ThreadsafeBlockingQueue<T> {

		private Queue<T> _q = new Queue<T>();

		/// <summary>Trägt einen Eintrag (ohne zu warten) ein</summary>
		public void Enqueue(T tItem) {
			lock (this) {
				_q.Enqueue(tItem);
				Monitor.Pulse(this);
			}
		}

		/// <summary>
		///    Holt einen Eintrag aus der Queue heraus und wartet dabei nötigenfalls
		///    solange bis wieder ein Eintrag vorhanden ist.
		/// </summary>
		public T Dequeue() {
			lock (this) {
				while (_q.Count == 0) {
					Monitor.Wait(this);
				}
				return _q.Dequeue();
			}
		}
	}
}