using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Liv.io.GraphCrawler.Interface;
using System.Text.RegularExpressions;
using System.Xml;

namespace Liv.io.GraphCrawler
{
	public class CrawlerProto
	{
		public NodeCache NodesCache {
			get;
			set;
		}

		public EdgeCache EdgesCache {
			get;
			set;
		}

		public ResourceCache ResourceCache {
			get;
			set;
		}

		public string Url {
			get;
			set;
		}

		public string FileLocation {
			get;
			set;
		}

		public string File {
			get;
			set;
		}

		public string FullPath {
			get {
				return Path.Combine (FileLocation, File);
			}
		}

		public CrawlerProto (string url, string fileLocation, NodeCache nodesCache, EdgeCache edgesCache, ResourceCache resourceCache)
		{
			FileLocation = fileLocation;
			Url = url;

			NodesCache = nodesCache;
			EdgesCache = edgesCache;
			ResourceCache = resourceCache;
		}

		public Resource Process ()
		{
			Resource res = Download ();
			return Parse (res);
		}

		protected Resource Download ()
		{
			ServicePointManager.ServerCertificateValidationCallback += (o, certificate, chain, errors) => true;
			HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create (Url);
			
			File = Guid.NewGuid ().ToString ();

			WebResponse response = request.GetResponse ();

			using (Stream responseStream = response.GetResponseStream()) 
			using (StreamReader streamReader = new StreamReader(responseStream))
			using (FileStream fileStream = System.IO.File.Create(FullPath))
			using (StreamWriter streamWriter = new StreamWriter(fileStream)) {
				streamWriter.Write (streamReader.ReadToEnd ());
			}

			Resource res = new Resource () {
				Uri = new Uri(Url),
				FilesystemLocation = File,
				Title = "",
				ContentType = response.ContentType
			};

			return res;
		}

		protected Resource Parse (Resource resource)
		{
			if (resource.ContentType.ToLower ().Contains ("html")) {

				using (Stream stream = System.IO.File.OpenRead(FullPath)) 
				using (StreamReader streamReader = new StreamReader(stream)) {

					SgmlParser parser = new SgmlParser ();
					XmlDocument xmlDom = parser.ParseSgml (streamReader);

					Stack<XmlNode> nodes = new Stack<XmlNode> ();

					foreach (XmlNode node in xmlDom.ChildNodes) {
						nodes.Push (node);
					}

					while (nodes.Count > 0) {
						var node = nodes.Pop ();

						switch (node.NodeType) {
						case XmlNodeType.None:
						case XmlNodeType.XmlDeclaration:
						case XmlNodeType.ProcessingInstruction:
						case XmlNodeType.DocumentFragment:
						case XmlNodeType.Whitespace:
						case XmlNodeType.Entity:
						case XmlNodeType.DocumentType:
						case XmlNodeType.Document:
						case XmlNodeType.Comment:
						case XmlNodeType.Notation:
						case XmlNodeType.SignificantWhitespace:
						case XmlNodeType.EndElement:
						case XmlNodeType.EndEntity:
						case XmlNodeType.EntityReference:
						case XmlNodeType.Attribute:
						default:
							continue;
					
						case XmlNodeType.Element:

							// split into words and get the x-path to the node
							if (node.Value == null)
								break;

							string[] words = node.Value.Split (new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
							string xpath = GetXPathToNode (node);

							System.Diagnostics.Debug.WriteLine (xpath + string.Join ("-", words));

							break;
						case XmlNodeType.Text:
							break;
						case XmlNodeType.CDATA:
							break;
						}

						foreach (XmlNode innerNode in node.ChildNodes) {
							nodes.Push (innerNode);
						}
					}
				}
			} else {

			}


			#region Old?!
//
//
//			// Look for URIs which are already crawled
//			foreach (DataRow row in ResourceCache.Resources.Rows) { 
//				
//				if (!Regex.IsMatch (content, (row ["Uri"] as string) + "\\W"))
//					continue;
//
//				if (!resource.ReferencedResources.Contains (row ["Uri"]as string)) 
//					resource.ReferencedResources.Add (row ["Uri"] as string);
//			}
//
//			// Look for already crawled class- and instance-names 
//			foreach (DataRow row in NodesCache.NodesTable.Rows) { 
//
//				if (!Regex.IsMatch (content, (row ["Label"] as string) + "\\W"))
//					continue;
//
//				if (row ["Type"] as string == "Class") 
//					resource.ReferencedClasses.Add (row ["Id"] as string);
//				else if (row ["Type"] as string == "Instance") 
//					resource.ReferencedObjects.Add (row ["Id"] as string);				
//			}

			#endregion

			return resource;
		}

		/// <summary>
		/// Gets the X-Path to a given Node
		/// </summary>
		/// <param name="node">The Node to get the X-Path from</param>
		/// <returns>The X-Path of the Node</returns>
		public string GetXPathToNode (XmlNode node)
		{
			if (node.NodeType == XmlNodeType.Attribute)
				return String.Format ("{0}/@{1}", GetXPathToNode (((XmlAttribute)node).OwnerElement), node.Name);

			if (node.ParentNode == null)
				return "";

			int indexInParent = 1;
			XmlNode siblingNode = node.PreviousSibling;

			while (siblingNode != null) {
				if (siblingNode.Name == node.Name)
					indexInParent++;

				siblingNode = siblingNode.PreviousSibling;
			}

			return String.Format ("{0}/{1}[{2}]", GetXPathToNode (node.ParentNode), node.Name, indexInParent);
		}
	}
}