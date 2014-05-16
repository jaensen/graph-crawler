using System;
using System.Runtime.Serialization;

namespace Liv.io.GraphCrawler.Interface
{
	[DataContract]
	public class Edge : ApiResponse
	{
		[DataMember]
		public string Source {
			get;
			set;
		}

		[DataMember]
		public string Target {
			get;
			set;
		}

		[DataMember]
		public string Id {
			get;
			set;
		}

		[DataMember]
		public string Label {
			get;
			set;
		}

		[DataMember]
		public float Weight {
			get;
			set;
		}
	}
}