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

		/// <summary>
		/// Gets or sets a value indicating whether this edge is a schema edge.
		/// </summary>
		/// <remarks>
		/// If the edge is a schema edge, all constraint properties become active.
		/// Constraint properties are: Cardinality, MinOccurence and MaxOccurence.
		/// 
		/// !! Schema edges only can connect class nodes with each other !!
		/// </remarks>
		/// <value><c>true</c> if this edge is a schema; otherwise, <c>false</c>.</value>
		[DataMember]
		public bool IsSchema {
			get;
			set;
		}

		/// <summary>
		/// Defines the cardinality of the Target.
		/// </summary>
		/// <value>The cardinality.</value>
		[DataMember]
		public Cardinality Cardinality {
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the minimum occurence of the Target (0-*).
		/// </summary>
		[DataMember]
		public int MinOccurence {
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the maximum occurence. 0 = unbounded
		/// </summary>
		[DataMember]
		public int MaxOccurence {
			get;
			set;
		}
	}

	/// <summary>
	/// Defines a cardinality
	/// </summary>
	[DataContract]
	public enum Cardinality {
		One,
		Many
	}
}