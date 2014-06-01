using System;
using System.Collections.Generic;

namespace Liv.io.TypeSystem
{
	public class T
	{
		public Guid Guid {
			get;
			private set;
		}

		public string Name {
			get;
			set;
		}

		public List<T> SameAs {
			get;
			private set;
		}

		public Context Context {
			get;
			private set;
		}

		internal T (Context context)
		{
			if (context == null)
				throw new ArgumentNullException ("context");

			Context = context;
			Guid = Guid.NewGuid ();
			SameAs = new List<T> ();
		}

		internal T (Context context, string name)
			:this(context)
		{
			Name = name;
		}

		internal T (Context context, string name, Guid guid)
		{
			if (context == null)
				throw new ArgumentNullException ("context");

			Context = context;
			Name = name;
			Guid = guid;
		}

		public override bool Equals (object obj)
		{
			if (obj == null)
				throw new ArgumentNullException ("obj");

			T casted = obj as T;

			if (casted == null)
				return false;

			return casted.Guid == Guid;
		}

		public override int GetHashCode ()
		{
			return Guid.GetHashCode ();
		}

		public override string ToString ()
		{
			return string.Format ("[T: Guid={0}, Name={1}]", Guid, Name);
		}
	}
}