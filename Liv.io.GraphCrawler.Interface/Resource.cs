using System;
using System.Runtime.Serialization;
using System.Collections.Generic;

namespace Liv.io.GraphCrawler.Interface
{
	[DataContract]
	public class Resource : ApiResponse
	{
		public Resource ()
		{
			ReferencedClasses = new List<string> ();
			ReferencedObjects = new List<string> ();
			ReferencedResources = new List<string> ();
		}

		[DataMember]
		public string ContentType {
			get;
			set;
		}

		[DataMember]
		public List<string> ReferencedResources {
			get;
			set;
		}

		[DataMember]
		public Uri Uri {
			get;
			set;
		}

		[DataMember]
		public List<string> ReferencedClasses {
			get;
			set;
		}

		[DataMember]
		public List<string> ReferencedObjects {
			get;
			set;
		}

		[DataMember]
		public string FilesystemLocation {
			get;
			set;
		}

		[DataMember]
		public string Title {
			get;
			set;
		}
	}
}