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

		public Inference ()
		{
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

		public bool Test ()
		{
			
			Obj objDuck = new Obj ();
			objDuck.Add (new KeyValuePair<T, Obj> (new T ("Walk"), null));
			objDuck.Add (new KeyValuePair<T, Obj> (new T ("Quack"), null));
			
			Obj objDog = new Obj ();
			objDuck.Add (new KeyValuePair<T, Obj> (new T ("Walk"), null));
			objDuck.Add (new KeyValuePair<T, Obj> (new T ("Bark"), null));
			
			Obj objHuman1 = new Obj ();
			objHuman1.Add (new KeyValuePair<T, Obj> (new T ("Walk"), null));
			objHuman1.Add (new KeyValuePair<T, Obj> (new T ("Speak"), null));

			Obj objHuman2 = new Obj ();
			objHuman2.Add (new KeyValuePair<T, Obj> (new T ("Walk"), null));
			objHuman2.Add (new KeyValuePair<T, Obj> (new T ("Speak"), null));
			
			T tDuck = new T ("Duck");
			T tDog = new T ("Dog");
			T tHuman = new T ("Human");

			Obj lifeform = new Obj ();
			lifeform.Add (new KeyValuePair<T, Obj> (tDuck, objDog));
			lifeform.Add (new KeyValuePair<T, Obj> (tDog, objDog));
			lifeform.Add (new KeyValuePair<T, Obj> (tHuman, objHuman1));
			lifeform.Add (new KeyValuePair<T, Obj> (tHuman, objHuman2));

			T Tlifeform = Zip (lifeform);

			return Tlifeform != null;
		}

		public bool IsA (Obj obj, T isA)
		{
			return false;
		}

		public T Zip (Obj obj)
		{
			if (obj == null)
				throw new ArgumentNullException ("obj");

			Obj first = new Obj ();

			var groupedInputProperties = (
				obj
				.GroupBy (p => p.Key.Guid)
				.Select (g => {
				T t = g.First ().Key;
				return  g.Count () > 1 
						? new TSequence (t) 
						: t;
			}));

			foreach (var propertyType in groupedInputProperties)
				first.Add (new KeyValuePair<T, Obj> (propertyType, null));

			Obj second = new Obj ();

			var orderedTypesAndSequences = (
				first
				.GroupBy (p => p.Key is TSequence)
				.OrderBy (g => g.Key)
				.Select (g => {
				if (g.Key) 
					return g.First ().Key;
				else 
					return new TComposition () {
						InnerT = new List<T>(g.Select(kvp => kvp.Key))
					};
			}));

			TComposition zipped = new TComposition ();

			foreach (var propertyType in orderedTypesAndSequences) {
				second.Add (new KeyValuePair<T, Obj> (propertyType, null));
				zipped.InnerT.Add (propertyType);
			}

			Obj third = new Obj ();
			third.Add (new KeyValuePair<T, Obj> (zipped, second));

			return zipped;
		}
	}
}