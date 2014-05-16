using System;
using System.Collections.Generic;

namespace Liv.io.GraphCrawler
{
	public class IndexedResource
	{
		public string Uri {
			get;
			set;
		}

		public List<int> ReferencedClasses {
			get;
			set;
		}

		public List<int> ReferencedObjects {
			get;
			set;
		}

		public IndexedResource ()
		{
			ReferencedClasses = new List<int> ();
			ReferencedObjects = new List<int> ();
		}
	}
}