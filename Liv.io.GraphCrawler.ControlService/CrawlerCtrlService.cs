using System;
using System.Threading;
using System.ServiceModel;
using Liv.io.GraphCrawler;
using Liv.io.GraphCrawler.Interface;
using Liv.io.Utils;
using System.ServiceModel.Description;
using System.Collections.Concurrent;
using System.ServiceModel.Web;
using System.Data;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace Liv.io.GraphCrawler.ControlService
{
	public class CrawlerCtrlService : ICrawlerCtrlService
	{
		private ILogger _logger;
		private IUniqueIdProvider _uniqueIdProvider;
		private NodeCache _nodes;
		private EdgeCache _edges;
		private ResourceCache _resources;
		private string _dataDirectory;

		public string NodesFilename {
			get;
			set;
		}

		public string EdgesFilename {
			get;
			set;
		}

		public string ResourcesTableFilename {
			get;
			set;
		}

		public string ResourcesFolder {
			get;
			set;
		}

		public string ClientCodeFolder {
			get;
			set;
		}

		public CrawlerCtrlService ()
		{
			_logger = new ConsoleLogger ();
			_uniqueIdProvider = new IntUniqueIdProvider ();
			_dataDirectory = Properties.Settings.Default.DataDirectory;

			_nodes = new NodeCache ();
			_edges = new EdgeCache ();
			_resources = new ResourceCache ();
			
			EdgesFilename = Properties.Settings.Default.EdgesFilename;
			NodesFilename = Properties.Settings.Default.NodesFilename;
			ResourcesTableFilename = Properties.Settings.Default.ResourcesTableFilename;
			ResourcesFolder = Properties.Settings.Default.ResourcesFolder;
			ClientCodeFolder = Properties.Settings.Default.ClientCodeFolder;

			Load ();
		}

		public void Load ()
		{
			_logger.Log (1, "CrawlerCtrlService", "Load()");
			try {
				_nodes.Load (Path.Combine (_dataDirectory, NodesFilename));
			} catch (FileNotFoundException e) {
				_logger.Log (99, "CrawlerCtrlService", "Error");
				_logger.Log (e);
			}
			try {
				_edges.Load (Path.Combine (_dataDirectory, EdgesFilename));
			} catch (FileNotFoundException e) {
				_logger.Log (99, "CrawlerCtrlService", "Error");
				_logger.Log (e);
			}
			try {
				_resources.Load (Path.Combine (_dataDirectory, ResourcesTableFilename), Path.Combine(_dataDirectory, ResourcesFolder));
			} catch (FileNotFoundException e) {
				_logger.Log (99, "CrawlerCtrlService", "Error");
				_logger.Log (e);
			}
		}

		public void Save ()
		{
			_logger.Log (1, "CrawlerCtrlService", "Save()");
			try {
				_logger.Log (1, "CrawlerCtrlService", "Nodes");
				_nodes.Save (Path.Combine (_dataDirectory, NodesFilename));
			} catch (Exception e) {
				_logger.Log (e);
				throw;
			}
			try {
				_logger.Log (1, "CrawlerCtrlService", "Edges");
				_edges.Save (Path.Combine (_dataDirectory, EdgesFilename));
			} catch (Exception e) {
				_logger.Log (e);
				throw;
			}
			try {
				_logger.Log (1, "CrawlerCtrlService", "Resources");
				_resources.Save (Path.Combine (_dataDirectory, ResourcesTableFilename));
			} catch (Exception e) {
				_logger.Log (e);
				throw;
			}
		}

		public Resource Crawl (string uri)
		{
			DataRow row = null;
			if (_resources.UriToResources.TryGetValue (uri, out row))
				return new Resource () {
					Message = "Already indexed.",
					Success = true,
					Uri = new Uri(row["Uri"] as string)
				};

			_logger.Log (1, "CrawlerCtrlService", string.Format ("Crawl({0})", uri));

			try {
				CrawlerProto crawler = new CrawlerProto (
					uri,
					Path.Combine (_dataDirectory, ResourcesFolder),
					_nodes,
					_edges,
					_resources);

				Resource resource = crawler.Process ();

				_resources.AddResource(resource);

				CreateNodeForResourceAndStateIsWebsite (resource);

				return resource;

			} catch (Exception ex) {
				_logger.Log (ex);
				throw;
			}
		}

		private void CreateNodeForResourceAndStateIsWebsite (Resource resource)
		{
			Node websiteNode = AddNode (resource.Uri.ToString (), "Instance");
			Node webpageClassNode = FindNodes ("Webpage").FirstOrDefault ();
			Edge crawledSiteIsAWebpage = AddEdge (websiteNode.Id, webpageClassNode.Id, "is-a");
		}

		#region ICrawlerCtrlService implementation

		private static Node RowToNode (DataRow nodeRow)
		{
			return new Node () {
				Id = nodeRow ["Id"] as string,
				Label = nodeRow ["Label"] as string,
				Type = nodeRow ["Type"] as string,
				Success = true,
				Message = ""
			};
		}

		public Stream StreamResource (string uri)
		{
			OutgoingWebResponseContext context = WebOperationContext.Current.OutgoingResponse;
			context.ContentType = "text";

			return _resources.StreamResource (uri);
		}

		public Node AddNode (string label, string type)
		{
			try {
				DataRow nodeRow = _nodes.AddNode (_uniqueIdProvider.GetUniqueId (),
				                                  label,
				                                  type);

				return RowToNode (nodeRow);
			} catch (Exception ex) {
				_logger.Log (ex);
				throw;
			}
		}

		private static Edge RowToEdge (DataRow edgeRow)
		{
			return new Edge () {
				Id = edgeRow ["Id"] as string,
				Source = edgeRow ["Source"] as string,
				Target = edgeRow ["Target"] as string,
				Label = edgeRow ["Label"] as string,
				Success = true,
				Message = ""
			};
		}

		public Edge AddEdge (string fromNode, string toNode, string predicate)
		{
			try {
				string uniqueId = _uniqueIdProvider.GetUniqueId ();

				DataRow edgeRow = _edges.AddEdge (fromNode,
				                                  toNode,
				                                  uniqueId,
				                                  "",
				                                  predicate,
				                                  1);

				return RowToEdge (edgeRow);
			} catch (Exception ex) {
				_logger.Log (ex);
				throw;
			}
		}

		public Node[] FindNodes (string labelStartsWith)
		{
			try {
				List<Node> foundNodes = new List<Node> ();

				foreach (DataRow nodeRow in  _nodes.FindNodesByLabel(labelStartsWith)) {
					foundNodes.Add (RowToNode (nodeRow));
				}

				return foundNodes.ToArray ();
			} catch (Exception ex) {
				_logger.Log (ex);
				throw;
			}
		}

		public Edge[] FindEdges (string labelStartsWith)
		{
			try {
				List<Edge> foundEdges = new List<Edge> ();

				foreach (var edgeRow in _edges.FindEdges(labelStartsWith)) {
					foundEdges.Add (RowToEdge (edgeRow));
				}

				return foundEdges.ToArray ();
			} catch (Exception ex) {
				_logger.Log (ex);
				throw;
			}
		}

		public Edge[] FindEdgesFromNode (string fromNode)
		{
			List<string> ids = null;
			if (!_edges.FromIdMap.TryGetValue (fromNode, out ids))
				return null;

			return LoadEdges (ids.ToArray ());
		}

		public Edge[] FindEdgesToNode (string toNode)
		{
			List<string> ids = null;
			if (!_edges.ToIdMap.TryGetValue (toNode, out ids))
				return null;

			return LoadEdges (ids.ToArray ());
		}

		public Node[] LoadNodes (string[] nodes)
		{
			try {
				List<Node> foundNodes = new List<Node> ();

				foreach (var nodeRow in _nodes.LoadNodes(nodes)) {
					foundNodes.Add (RowToNode (nodeRow));
				}

				return foundNodes.ToArray ();
			} catch (Exception ex) {
				_logger.Log (ex);
				throw;
			}
		}

		public Edge[] LoadEdges (string[] edges)
		{
			try {
				List<Edge> foundEdges = new List<Edge> ();

				foreach (var edgeRow in _edges.LoadEdges(edges)) {
					foundEdges.Add (RowToEdge (edgeRow));
				}

				return foundEdges.ToArray ();
			} catch (Exception ex) {
				_logger.Log (ex);
				throw;
			}
		}

		public Stream LoadClientCode (string app)
		{
			try {
				string clientCodeDirectory = Path.Combine (_dataDirectory, ClientCodeFolder);
				string clientCodeFile = Path.Combine (clientCodeDirectory, app);

				if (!File.Exists (clientCodeFile))
					return new System.IO.MemoryStream (UTF8Encoding.Default.GetBytes ("Error! Could not find the specified client code."));

				string contents = File.ReadAllText (clientCodeFile);

				OutgoingWebResponseContext context = WebOperationContext.Current.OutgoingResponse;
				context.ContentType = "text/html";

				return new System.IO.MemoryStream (UTF8Encoding.Default.GetBytes (contents));
			} catch (Exception ex) {
				_logger.Log (ex);
				throw;
			}
		}

		public ApiResponse RemoveNode (string node)
		{
			try {
				_nodes.RemoveNode (node);

				return new ApiResponse () {
					Message = "",
					Success = true
				};
			} catch (Exception ex) {
				_logger.Log (ex);
				throw;
			}
		}

		public ApiResponse RemoveEdge (string edge)
		{
			try {
				_edges.RemoveEdge (edge);

				return new ApiResponse () {
					Message = "",
					Success = true
				};
			} catch (Exception ex) {
				_logger.Log (ex);
				throw;
			}
		}

		public string[] LoadEdgeTypes ()
		{
			return _edges.GroupedEdgeTypes.Keys.ToArray ();
		}

		public Node[] LoadClassNodes ()
		{
			List<Node> classesList = new List<Node> (_nodes.Classes.Count);

			foreach (DataRow classNode in _nodes.Classes.ToTable().Rows) {
				classesList.Add(RowToNode(classNode));
			}

			return classesList.ToArray ();
		}

		public Node[] LoadInstanceNodes ()
		{
			List<Node> instanceList = new List<Node> (_nodes.Objects.Count);

			foreach (DataRow instanceNode in _nodes.Objects.ToTable().Rows) {
				instanceList.Add(RowToNode(instanceNode));
			}

			return instanceList.ToArray ();
		}

		#endregion
	}
}