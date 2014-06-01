using System;

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

		internal T ()
		{
			Guid = Guid.NewGuid ();
		}

		internal T (string name)
			:this()
		{
			Name = name;
		}

		internal T (string name, Guid guid)
		{
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
	}
}