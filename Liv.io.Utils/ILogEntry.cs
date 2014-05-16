using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Liv.io.Utils {
	/// <summary>
	/// Describes the interface for a log-entry.
	/// </summary>
	public interface ILogEntry {
		/// <summary>
		/// The unique id of the log entry.
		/// </summary>
		string Id { get; set; }

		/// <summary>
		/// The date and time when the log entry was written.
		/// </summary>
		DateTime Timestamp { get; set; }

		/// <summary>
		/// The name or ip of the host which sent the request which caused the creation of this log entry.
		/// </summary>
		string RequestingHost { get; set; }
		/// <summary>
		/// The name or ip of the host which processed the request and caused the creation of this log entry.
		/// </summary>
		string ProcessingHost { get; set; }

		/// <summary>
		/// The severyity.
		/// </summary>
		int Severity { get; set; }

		/// <summary>
		/// The action which caused this log entry (e.g. a method name, module name etc..)
		/// </summary>
		string Action { get; set; }

		/// <summary>
		/// The log message
		/// </summary>
		string Message { get; set; }
	}
}