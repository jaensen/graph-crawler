using System;
using System.Linq;
using System.Collections.Generic;
using Liv.io.Utils;

namespace Liv.io.TypeSystem
{
	public class Inference
	{
		public Context Context {
			get;
			private set;
		}

		internal Inference (Context context)
		{
			if (context == null)
				throw new ArgumentNullException ("context");

			Context = context;
		}

		/// <summary>
		/// Creates a type from the supplied object and registers the object as prototype for
		/// the created type.
		/// </summary>
		/// <returns>The newly created type.</returns>
		/// <param name="obj">The object which should become a prototype.</param>
		public T CreatePrototype (Obj obj)
		{
			if (obj == null)
				throw new ArgumentNullException ("obj");

			var groupedInputProperties = (
				obj
				.GroupBy (p => p.Key.Guid)
				.Select (g => {
				if (g.Count () > 1) 
					return new TSequence (Context) {
						InnerT = new List<T>(g.Select(kvp => kvp.Key))
					};
				else
					return g.First ().Key;
			}));

			var orderedTypesAndSequences = (
				groupedInputProperties
				.GroupBy (p => p is TSequence)
				.OrderBy (g => g)
				.Select (g => {
				if (g.Key) 
					return g.First ();
				else 
					return new TComposition (Context) {
						InnerT = new List<T>(g)
					};
			}));

			TComposition type = new TComposition (Context);
			type.InnerT.AddRange (orderedTypesAndSequences);

			Context.Prototypes.Add (type, obj);

			return type;
		}
	}
}