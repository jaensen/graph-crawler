using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Liv.io.GraphCrawler.Interface;
using System.Text.RegularExpressions;

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
			};

			return res;
		}

		protected Resource Parse (Resource resource)
		{			
			string content = System.IO.File.ReadAllText (FullPath);

			foreach (DataRow row in ResourceCache.Resources.Rows) { 
				
				if (!Regex.IsMatch (content, (row ["Uri"] as string) + "\\W"))
					continue;

				if (!resource.ReferencedResources.Contains (row ["Uri"]as string)) 
					resource.ReferencedResources.Add (row ["Uri"] as string);
			}

			foreach (DataRow row in NodesCache.NodesTable.Rows) { 

				if (!Regex.IsMatch (content, (row ["Label"] as string) + "\\W"))
					continue;

				if (row ["Type"] as string == "Class") 
					resource.ReferencedClasses.Add (row ["Id"] as string);
				else if (row ["Type"] as string == "Instance") 
					resource.ReferencedObjects.Add (row ["Id"] as string);				
			}

			return resource;
		}
	}
}