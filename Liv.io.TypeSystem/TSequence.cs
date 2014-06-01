using System;

namespace Liv.io.TypeSystem
{
//	public class TSequence : T
//	{
//		public T OfType {
//			get;
//			set;
//		}
//
//		public TSequence (T ofType)
//			:base()
//		{
//			OfType = ofType;
//		}
//
//		public TSequence (T ofType, string name)
//			:base(name)
//		{
//			OfType = ofType;
//		}
//
//		public TSequence (T ofType, string name, Guid guid)
//			:base(name, guid)
//		{
//			OfType = ofType;
//		}
//
//		public override string ToString ()
//		{
//			return string.Format ("[TSequence: Guid={0}, Name={1}, OfType={2}]", Guid, Name, OfType);
//		}
//	}


	public class TSequence : TComposition
	{
		public TSequence (Context context)
			:base(context)
		{
		}

		public TSequence (Context context, string name)
			:base(context, name)
		{
		}

		public TSequence (Context context, string name, Guid guid)
			:base(context, name, guid)
		{
		}

		public override string ToString ()
		{
			return string.Format ("[TSequence: Guid={0}, Name={1}]", Guid, Name);
		}
	}

}