using System;
using System.Collections.Generic;

namespace Liv.io.TypeSystem
{
	public class TComposition : T
	{
		public TComposition (Context context)
			:base(context)
		{
			InnerT = new List<T> ();
		}

		public TComposition (Context context, string name)
			:base(context,name)
		{
			InnerT = new List<T> ();
		}

		public TComposition (Context context, string name, Guid guid)
			:base(context, name, guid)
		{
			InnerT = new List<T> ();
		}

		public List<T> InnerT {
			get;
			set;
		}

		public override string ToString ()
		{
			return string.Format ("[TComposition: Guid={0}, Name={1}, InnerT={2}]", Guid, Name, InnerT.Count);
		}
	}
}