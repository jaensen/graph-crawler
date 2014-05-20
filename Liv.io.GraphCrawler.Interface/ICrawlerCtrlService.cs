using System;
using System.ServiceModel;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.ServiceModel.Web;
using System.IO;

namespace Liv.io.GraphCrawler.Interface
{
	[ServiceContract]
	public interface ICrawlerCtrlService
	{
		[OperationContract]
		[WebGet(UriTemplate = "crawl?url={url}", ResponseFormat = WebMessageFormat.Json)]
		Resource Crawl (string url);

		//  http://localhost:1212/ctrlService/streamResource?uri=http://en.wikipedia.org/wiki/Social_peer-to-peer_processes
		[OperationContract]
		[WebGet(UriTemplate = "streamResource?uri={uri}", ResponseFormat = WebMessageFormat.Json)]
		Stream StreamResource (string uri) ;

		[OperationContract]
		[WebGet(UriTemplate = "addNode?label={label}&type={type}", ResponseFormat = WebMessageFormat.Json)]
		Node AddNode (string label, string type);

		[OperationContract]
		[WebGet(UriTemplate = "removeNode?node={node}", ResponseFormat = WebMessageFormat.Json)]
		ApiResponse RemoveNode (string node);

		[OperationContract]
		[WebGet(UriTemplate = "addEdge?fromNode={fromNode}&toNode={toNode}&predicate={predicate}", ResponseFormat = WebMessageFormat.Json)]
		Edge AddEdge (string fromNode, string toNode, string predicate);

		[OperationContract]
		[WebGet(UriTemplate = "removeEdge?edge={edge}", ResponseFormat = WebMessageFormat.Json)]
		ApiResponse RemoveEdge (string edge);

		[OperationContract]
		[WebGet(UriTemplate = "findNodes?labelStartsWith={labelStartsWith}", ResponseFormat = WebMessageFormat.Json)]
		Node[] FindNodes (string labelStartsWith);

		[OperationContract]
		[WebGet(UriTemplate = "findEdges?labelStartsWith={labelStartsWith}", ResponseFormat = WebMessageFormat.Json)]
		Edge[] FindEdges (string labelStartsWith);

		[OperationContract]
		[WebGet(UriTemplate = "findEdgesFromNode?fromNode={fromNode}", ResponseFormat = WebMessageFormat.Json)]
		Edge[] FindEdgesFromNode (string fromNode);

		[OperationContract]
		[WebGet(UriTemplate = "findEdgesToNode?toNode={toNode}", ResponseFormat = WebMessageFormat.Json)]
		Edge[] FindEdgesToNode (string toNode);

		[OperationContract]
		[WebGet(UriTemplate = "loadNodes?nodes={nodes}", ResponseFormat = WebMessageFormat.Json)]
		Node[] LoadNodes (string[] nodes);

		[OperationContract]
		[WebGet(UriTemplate = "loadEdges?edges={edges}", ResponseFormat = WebMessageFormat.Json)]
		Edge[] LoadEdges (string[] edges);

		[OperationContract]
		[WebGet(UriTemplate = "loadEdgeTypes", ResponseFormat = WebMessageFormat.Json)]
		string[] LoadEdgeTypes ();

		[OperationContract]
		[WebGet(UriTemplate = "loadClassNodes", ResponseFormat = WebMessageFormat.Json)]
		Node[] LoadClassNodes ();

		[OperationContract]
		[WebGet(UriTemplate = "loadInstanceNodes", ResponseFormat = WebMessageFormat.Json)]
		Node[] LoadInstanceNodes ();

		[OperationContract]
		[WebGet(UriTemplate = "loadClientCode/apps/{app}")]
		Stream LoadClientCode (string app);

		[OperationContract]
		[WebGet(UriTemplate = "load")]
		void Load ();

		[OperationContract]
		[WebGet(UriTemplate = "save")]
		void Save ();
	}
}