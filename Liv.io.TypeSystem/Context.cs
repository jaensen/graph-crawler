using System;
using System.Collections.Generic;

namespace Liv.io.TypeSystem
{
	/// <summary>
	/// Types are created in a specific context and always maintain a relation to this context.
	/// Types withtin a context are refered by their names. When references outside of the creation context
	/// they have to be referenced by their GUIDs.
	/// </summary>
	public sealed class Context
	{
		public static Context SystemContext {
			get {
				if (_systemContext == null) {
					_systemContext = new Context ("System", new Guid ("ec5ca7bc-bac4-4270-b121-2dcf7f49142b"));
				}

				return _systemContext;
			}
		}

		private static Context _systemContext;

		public Dictionary<T,Obj> Prototypes {
			get;
			private set;
		}

		public string Name {
			get;
			set;
		}

		public Guid Guid {
			get;
			private set;
		}

		public Context ()
		{
			Prototypes = new Dictionary<T, Obj> ();
			Guid = Guid.NewGuid ();
		}

		public Context (string name)
			:this()
		{
			Name = name;
		}

		public Context (string name, Guid guid)
		{
			Prototypes = new Dictionary<T, Obj> ();
			Name = name;
			Guid = guid;
		}

		public TYPE CreateType<TYPE> ()
			where TYPE : T
		{
			return (TYPE)new T (this);
		}

		public TYPE CreateType<TYPE> (string name)
			where TYPE : T
		{
			return (TYPE)new T (this, name);
		}

		public TYPE CreateType<TYPE> (string name, Guid guid)
			where TYPE : T
		{
			return (TYPE)new T (this, name, guid);
		}

		public Inference CreateInferencer ()
		{
			return new Inference (this);
		}
	}
}