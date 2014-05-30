using System;
using Mono.WebServer;
using System.Net;
using System.IO;
using System.Threading;
using System.Reflection;

namespace Liv.io.Host
{
	/// <summary>
	/// Hosts a ASP.Net proxy server using Mono's XSP Webserver.
	/// </summary>
	public class Webserver
	{
		public int Port {
			get;
			set;
		}

		#pragma warning disable 0618
		XSPWebSource webSource;
		#pragma warning restore 0618

		ApplicationServer WebAppServer;

		public Webserver (int port = 80)
		{
			// I know, i know... bad style but I couldn't find a successor of the class.
			// This post in the mono mailing list stated that the class would be removed in 2010, now its 2014.
			// http://permalink.gmane.org/gmane.comp.gnome.mono.patches/172698
			// There's life in the old dog yet.
			#pragma warning disable 0618
			webSource = new XSPWebSource (IPAddress.Any, port);
			WebAppServer = new ApplicationServer (webSource);
			#pragma warning restore 0618

			string commandLine = string.Format ("{0}:/:{1}"
			                                   , port
			                                   , Path.GetDirectoryName (Assembly.GetExecutingAssembly ().Location));

			WebAppServer.AddApplicationsFromCommandLine (commandLine);
		}

		public void Start ()
		{
			WebAppServer.Start (true, 20);
		}

		public void Stop ()
		{
			WebAppServer.Stop ();
		}
	}
}