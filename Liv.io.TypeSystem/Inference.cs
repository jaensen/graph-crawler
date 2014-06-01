using System;
using System.Linq;
using System.Collections.Generic;
using Liv.io.Utils;

namespace Liv.io.TypeSystem
{
	public class Inference
	{
		public Dictionary<T,Obj> Prototypes {
			get;
			private set;
		}

		public Dictionary<Obj,List<T>> ObjectMetadata {
			get;
			private set;
		}

		public Context Context {
			get;
			private set;
		}

		internal Inference (Context context)
		{
			if (context == null)
				throw new ArgumentNullException ("context");

			Context = context;
			Prototypes = new Dictionary<T, Obj> ();
			ObjectMetadata = new Dictionary<Obj, List<T>> ();
		}

		public void Name (Obj obj, T isA)
		{
			if (obj == null)
				throw new ArgumentNullException ("obj");
			if (isA == null)
				throw new ArgumentNullException ("isA");

			Prototypes.AddOrUpdate (isA, obj);
		}

		public T CreateType (Obj obj)
		{
			if (obj == null)
				throw new ArgumentNullException ("obj");

			Obj first = new Obj ();

			var groupedInputProperties = (
				obj
				.GroupBy (p => p.Key.Guid)
				.Select (g => {
				T t = g.First ().Key;
				if (g.Count () > 1) {
					TSequence sequence = new TSequence (Context);
					foreach (var innerT in g) 
						sequence.InnerT.Add (innerT.Key);
					return sequence;
				} else {
					return t;
				}
			}));

			foreach (var propertyType in groupedInputProperties)
				first.Add (propertyType, null);

			Obj second = new Obj ();

			var orderedTypesAndSequences = (
				first
				.GroupBy (p => p.Key is TSequence)
				.OrderBy (g => g.Key)
				.Select (g => {
				if (g.Key) 
					return g.First ().Key;
				else 
					return new TComposition (Context) {
						InnerT = new List<T>(g.Select(kvp => kvp.Key))
					};
			}));

			TComposition zipped = new TComposition (Context);

			foreach (var propertyType in orderedTypesAndSequences) {
				second.Add (propertyType, null);
				zipped.InnerT.Add (propertyType);
			}

			Obj third = new Obj ();
			third.Add (zipped, second);

			return zipped;
		}
	}
}