using System;
using System.Runtime.Serialization;

namespace Liv.io.GraphCrawler.Interface
{
	[DataContract]
	public class Node : ApiResponse
	{
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
		public string Type {
			get;
			set;
		}
	}
}