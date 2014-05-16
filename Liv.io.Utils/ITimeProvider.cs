using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Liv.io.Utils {
	/// <summary>
	/// Describes a interface which can be used to get the current UTC date and time from a central service.
	/// </summary>
	public interface ITimeProvider {
		/// <summary>
		/// Gets the current UTC date and time from a service.
		/// </summary>
		/// <returns></returns>
		DateTime GetCurrentDateTimeUtc();
	}
}