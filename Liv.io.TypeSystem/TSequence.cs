using System;

namespace Liv.io.TypeSystem
{
	public class TSequence : T
	{
		public T OfType {
			get;
			set;
		}

		public TSequence (T ofType)
			:base()
		{
			OfType = ofType;
		}

		public TSequence (T ofType, string name)
			:base(name)
		{
			OfType = ofType;
		}

		public TSequence (T ofType, string name, Guid guid)
			:base(name, guid)
		{
			OfType = ofType;
		}
	}
}