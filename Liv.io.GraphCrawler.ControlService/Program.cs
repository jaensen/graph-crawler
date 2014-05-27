using System;
using System.IO;
using System.ServiceModel.Web;

namespace Liv.io.GraphCrawler.ControlService
{
	public static class Program
	{
		public static void Main ()
		{
//			byte[] decoded = Convert.FromBase64String (File.ReadAllText ("test_base64.txt").Replace("\r","").Replace("\n",""));
//			using (FileStream fs = File.Create ("decoded.bin")) {
//				fs.Write (decoded, 0, decoded.Length);
//			}

			// *** Jon's JSON Graph Stuff for import into CouchDB ***
			// 
			// Stuff.CrawlGraphJson crawlJson = new Stuff.CrawlGraphJson ();
			// crawlJson.Crawl (File.ReadAllText ("/home/daniel/Liv.io.GraphCrawler/Stuff/pblcspcinvdrs.json"));
			//
			StartService ();
		}

		public static void StartService ()
		{
			try {
				Uri uri = new Uri ("http://localhost:1212/ctrlService");
				var _serviceHost = new WebServiceHost (typeof(CrawlerCtrlService), uri);

				_serviceHost.Open ();

			} catch (Exception) {

				throw;
			}
		}
	}
}