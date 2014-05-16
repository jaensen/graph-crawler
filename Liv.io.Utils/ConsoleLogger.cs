using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Liv.io.Utils {
	public class ConsoleLogger : ILogger {

		public IUniqueIdProvider UniqueIdProvider {
			get;
			set;
		}

		public ITimeProvider TimeProvider {
			get;
			set;
		}

		public ILogEntry Log(int severity, string message) {
			return Log(severity, "", Environment.MachineName, message);
		}

		public ILogEntry Log(int severity, string action, string message) {
			return Log(severity, "", Environment.MachineName, message);
		}

		public ILogEntry Log(int severity, string requestingHost, string processingHost, string message) {
			if (string.IsNullOrEmpty(processingHost))
				throw new ArgumentException("The processing host can not be null or whitespace!", "processingHost");
			return Log(severity, requestingHost, processingHost, "", message);
		}

		public ILogEntry Log(int severity, string requestingHost, string processingHost, string action, string message) {
			Console.WriteLine(string.Format(
				"{0} - {1} - {2}",
				DateTime.Now,
				severity,
				message));

			return null;
		}

		public ILogEntry Log(Exception ex) {
			return Log(ex, "", Environment.MachineName, "");
		}

		public ILogEntry Log(Exception ex, string requestingHost) {
			return Log(ex, requestingHost, Environment.MachineName, "");
		}

		public ILogEntry Log(Exception ex, string action, string message) {
			return Log(ex, "", Environment.MachineName, action, message);
		}

		public ILogEntry Log(Exception ex, string requestingHost, string processingHost, string message) {
			if (string.IsNullOrEmpty(processingHost))
				throw new ArgumentException("The processing host can not be null or whitespace.", "processingHost");

			return Log(ex, requestingHost, processingHost, "", message);
		}

		public ILogEntry Log(Exception ex, string requestingHost, string processingHost, string action, string message) {
			if (string.IsNullOrEmpty(processingHost))
				throw new ArgumentException("The processing host can not be null or whitespace.", "processingHost");

			ExceptionMessageBuilder exMessageBuilder = new ExceptionMessageBuilder();

			using (StringWriter messageWriter = new StringWriter()) {
				messageWriter.WriteLine("Message: " + message ?? "");
				exMessageBuilder.BuildDetailedExceptionText(ex, messageWriter, false);
				return Log(999, requestingHost, processingHost, action, messageWriter.ToString());
			}
		}
	}
}