using System;

namespace Liv.io.Utils
{
	public class GuidUniqueIdProvider : IUniqueIdProvider
	{
		public GuidUniqueIdProvider ()
		{
		}
		#region IUniqueIdProvider implementation
		public string GetUniqueId ()
		{
			return Guid.NewGuid ().ToString ();
		}
		#endregion
	}
}