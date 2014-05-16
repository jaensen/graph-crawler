using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Liv.io.Utils {
	/// <summary>
	/// Describes the service interface for a logger.
	/// </summary>
	public interface ILogger {

		/// <summary>
		/// Gets or sets the unique ID provider which should be used in distributed environments.
		/// </summary>
		IUniqueIdProvider UniqueIdProvider { get; set; }

		/// <summary>
		/// Gets or sets the time provider which should be used in distributed environments.
		/// </summary>
		ITimeProvider TimeProvider { get; set; }

		/// <summary>
		/// Writes a new log entry.
		/// </summary>
		/// <param name="severity">The severity</param>
		/// <param name="message">The log message</param>
		/// <remarks>
		/// The ILogEntry interface specifies some mandatory fields which are not covered by this method overload:
		/// - ProcessingHost: Will be the name of the machine on which this method executes
		/// </remarks>
		ILogEntry Log(int severity, string message);

		ILogEntry Log(int severity, string action, string message);

		ILogEntry Log(int severity, string requestingHost, string processingHost, string message);

		ILogEntry Log(int severity, string requestingHost, string processingHost, string action, string message);

		ILogEntry Log(Exception ex);

		ILogEntry Log(Exception ex, string requestingHost);

		ILogEntry Log(Exception ex, string action, string message);

		ILogEntry Log(Exception ex, string requestingHost, string processingHost, string message);

		ILogEntry Log(Exception ex, string requestingHost, string processingHost, string action, string message);
	}
}