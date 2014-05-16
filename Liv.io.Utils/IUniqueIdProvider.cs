using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Liv.io.Utils {
	/// <summary>
	/// Describes a interface for a unique id generator which is able to 
	/// provide IDs which are unique across different machines in a distributed environment.
	/// </summary>
	public interface IUniqueIdProvider {
		/// <summary>
		/// Gets a system wide unique id.
		/// </summary>
		string GetUniqueId();
	}
}