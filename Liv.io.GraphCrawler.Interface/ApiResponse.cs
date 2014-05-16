using System;
using System.Runtime.Serialization;

namespace Liv.io.GraphCrawler.Interface
{
	[DataContract]
	public class ApiResponse
	{
		[DataMember]
		public bool Success {
			get;
			set;
		}

		[DataMember]
		public string Message {
			get;
			set;
		}
	}
}