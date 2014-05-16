using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace Liv.io.Utils {

	public static class CsvWriter {
		public static void WriteToStream(TextWriter stream, DataTable table, bool header, bool quoteall, char seperator) {
			if (header) {
				for (int i = 0; i < table.Columns.Count; i++) {
					WriteItem(stream, table.Columns[i].Caption, quoteall, seperator);
					if (i < table.Columns.Count - 1)
						stream.Write(seperator);
					else
						stream.Write('\n');
				}
			}

			foreach (DataRow row in table.Rows) {
				for (int i = 0; i < table.Columns.Count; i++) {
					WriteItem(stream, row[i], quoteall, seperator);
					if (i < table.Columns.Count - 1)
						stream.Write(seperator);
					else
						stream.Write('\n');
				}
			}
			stream.Flush();
			stream.Close();
		}


		private static void WriteItem(TextWriter stream, object item, bool quoteall, char seperator) {
			if (item == null)
				return;

			string s = item.ToString();
			if (quoteall || s.IndexOfAny(("\"" + seperator.ToString() + "\x0A\x0D").ToCharArray()) > -1)
				stream.Write("\"" + s.Replace("\"", "\"\"") + "\"");
			else
				stream.Write(s);
			stream.Flush();
		}
	}
}