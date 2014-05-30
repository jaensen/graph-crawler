using System;
using System.ServiceModel.Web;
using Liv.io.GraphCrawler.ControlService;
using System.Threading;
using System.IO;
using Liv.io.Utils;

namespace Liv.io.Host
{
	class MainClass
	{
		static void Main (string[] args)
		{
			ManualResetEvent quitEvent = new ManualResetEvent (false);

			StartControlService (quitEvent);
			StartWebserver ();
			
			System.Diagnostics.Debug.WriteLine ("All services running. Use the ?quit method of the control service to close the application.");

			quitEvent.WaitOne ();

			System.Diagnostics.Debug.WriteLine ("Quit");
		}

		static void StartControlService (ManualResetEvent quitEvent)
		{
			try {

				string uriStr = string.Format ("http://localhost:{0}/{1}"
				                              , Properties.Settings.Default.ControlServicePort
				                              , Properties.Settings.Default.ControlServicePath);

				System.Diagnostics.Debug.WriteLine (string.Format ("Starting control service ({0}) ..."), uriStr);

				ControlService crawlerControlService = new ControlService ();
				crawlerControlService.QuitEvent = quitEvent;

				var serviceHost = new WebServiceHost (crawlerControlService, new Uri (uriStr));

				serviceHost.Open ();

				System.Diagnostics.Debug.WriteLine (string.Format ("Control service running."));

			} catch (Exception ex) {

				LogEx (ex);
				throw;
			}
		}

		static void StartWebserver ()
		{
			try {
				System.Diagnostics.Debug.WriteLine (string.Format ("Starting webserver on port {0} ..."), Properties.Settings.Default.WebserverPort);

				Webserver proxyHost = new Webserver (Properties.Settings.Default.WebserverPort);
				proxyHost.Start ();

				System.Diagnostics.Debug.WriteLine (string.Format ("Webserver running."));

			} catch (Exception ex) {
				
				LogEx (ex);
				throw;
			}
		}

		static void LogEx (Exception ex)
		{
			using (StringWriter stringWriter = new StringWriter ()) {
				new ExceptionMessageBuilder ().BuildDetailedExceptionText (ex, stringWriter, false);
				System.Diagnostics.Debug.WriteLine (stringWriter.ToString ());
			}
		}
	}
}