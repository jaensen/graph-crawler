using System;
using Liv.io.GraphCrawler.Interface;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json.Linq;

namespace Stuff
{
	public class CrawlGraphJson
	{
		public void Crawl (string jsonString)
		{
			_nodes = new List<Node> ();
			_edges = new List<Edge> ();

			dynamic d = JObject.Parse (jsonString);
			
			foreach (var node in d["nodes"]) {
				_nodes.Add (new Node () {
					Id = node["id"].ToString(),
					Label = node["data"]["name"].ToString(),
					Type = node["type"].ToString()
				});
			}

			int idN = 0;
			string idT = "IE_";

			foreach (var edge in d["edges"]) {
				_edges.Add (new Edge () {
					Id = string.Format("{0}{1}", idT, idN++),
					Label = "",
					Source = edge[0].ToString(),
					Target = edge[1].ToString()
				});
			}
		}

		public Node[] Nodes {
			get {
				return _nodes.ToArray ();
			}
		}

		private List<Node> _nodes;

		public Edge[] Edges {
			get {
				return _edges.ToArray ();
			}
		}

		private List<Edge> _edges;
	}
}