using System;
using System.Data;
using System.Linq;
using Liv.io.GraphCrawler.Interface;
using System.Collections.Generic;
using Liv.io.Utils;
using System.IO;

namespace Liv.io.GraphCrawler
{
	public class ResourceCache
	{
		public DataTable Resources {
			get;
			set;
		}

		public Dictionary<string,DataRow> UriToResources {
			get;
			set;
		}

		public ResourceCache ()
		{
			Init ();
		}

		public string ResourceFolder {
			get;
			set;
		}

		public void Load (string source, string resourceFolder)
		{
			ResourceFolder = resourceFolder;

			Init ();

			DataTable resourcesTempTable = CsvReader.Read (source, '|', true);

			foreach (DataRow row in resourcesTempTable.Rows) {			

				string uri = row ["Uri"] as string;
				string title = row ["Title"] as string;
				string location = row ["FilesystemLocation"] as string;

				AddResource (new Resource () {
					Uri = new Uri(uri),
					Title = title,
					FilesystemLocation = location
				});
			}
		}

		public void Save (string destination)
		{
			using (TextWriter textWriter = File.CreateText(destination.ToString())) {
				CsvWriter.WriteToStream (textWriter, Resources, true, false, '|');
			}
		}

		public void Init ()
		{		
			Resources = new DataTable ();
			
			Resources.Columns.Add ("Uri", typeof(string));
			Resources.Columns.Add ("Title", typeof(string));
			Resources.Columns.Add ("FilesystemLocation", typeof(string));

			UriToResources = new Dictionary<string, DataRow> ();
		}

		public DataRow AddResource (Resource resource)
		{
			DataRow newRow = Resources.NewRow ();

			string uri = resource.Uri.ToString ();

			newRow ["Uri"] = uri;
			newRow ["Title"] = resource.Title;
			newRow ["FilesystemLocation"] = resource.FilesystemLocation;

			Resources.Rows.Add (newRow);
			UriToResources.Add (uri, newRow);

			return newRow;
		}

		public Resource GetResource (string uri)
		{
			DataRow resourceRow = null;

			if (!UriToResources.TryGetValue (uri, out resourceRow)) {
				return null;
			}

			return new Resource () {
				Uri = new Uri(resourceRow["Uri"] as string),
				Title = resourceRow["Title"] as string,
				FilesystemLocation =  resourceRow["FilesystemLocation"] as string
			};
		}

		/// <summary>
		/// Streams a stored resource to the requesting client
		/// </summary>
		/// <returns>The resource stream.</returns>
		/// <param name="uri">The original uri</param>
		public Stream StreamResource (string uri)
		{
			Resource resource = GetResource (uri);

			string cachedResource = File.ReadAllText (Path.Combine (ResourceFolder, resource.FilesystemLocation));

			MemoryStream stream = new MemoryStream ();
			StreamWriter sw = new StreamWriter (stream);
			sw.Write (cachedResource);			
			stream.Position = 0;

			return stream;
		}

		public IEnumerable<Resource> FindResources (string uri)
		{
			DataRow[] rows = Resources.Select (string.Format ("Uri LIKE '{0}%'", uri));

			foreach (var row in rows) {

				yield return new Resource () {
					Uri = new Uri(row["Uri"] as string),
					Title = row["Title"] as string,
					FilesystemLocation =  row["FilesystemLocation"] as string
				};
			}
		}
	}
}