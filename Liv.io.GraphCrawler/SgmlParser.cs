using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Xml;
using System.IO;

namespace Liv.io.GraphCrawler
{
	public class SgmlParser
	{
		public XmlDocument ParseSgml(TextReader textReader) {

			if (textReader == null)
				throw new ArgumentNullException ("textReader");

			Sgml.SgmlReader sgmlReader = new Sgml.SgmlReader();
			sgmlReader.DocType = "HTML";
			sgmlReader.WhitespaceHandling = WhitespaceHandling.All;
			sgmlReader.CaseFolding = Sgml.CaseFolding.ToLower;
			sgmlReader.InputStream = textReader;

			XmlDocument doc = new XmlDocument();
			doc.PreserveWhitespace = true;
			doc.XmlResolver = null;
			doc.Load(sgmlReader);

			return doc;
		}
	}
}