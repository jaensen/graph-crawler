using System;
using System.Collections.Generic;

namespace Liv.io.TypeSystem
{
	public class TComposition : T
	{
		public TComposition ()
			:base()
		{
			InnerT = new List<T> ();
		}

		public TComposition (string name)
			:base(name)
		{
		}

		public TComposition (string name, Guid guid)
			:base(name, guid)
		{
		}

		public List<T> InnerT {
			get;
			set;
		}
	}
}