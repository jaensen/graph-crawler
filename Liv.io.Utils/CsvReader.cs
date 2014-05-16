using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace Liv.io.Utils {
	/// <summary>
	/// Stellt Methoden bereit um eine CSV-Datei in eine <see cref="System.Data.DataTable"/> einzulesen.
	/// </summary>
	public static class CsvReader {
		/// <summary>
		/// Liest die angegebene CSV-Datei in den Speicher und stellt sie als <see cref="System.Data.DataTable"/> bereit.
		/// </summary>
		/// <param name="filename">Pfad zur CSV-Datei</param>
		/// <param name="seperator">Das verwendete Trennzeichen</param>
		/// <param name="firstLineIsHeader">Gibt an ob die erste Zeile eine Spaltenbeschriftung enthält</param>
		/// <returns>Das gefüllte DataTable</returns>
		public static DataTable Read(string filename, char seperator, bool firstLineIsHeader) {
			DataTable dt = new DataTable();
			char[] sep = new char[] { seperator };
			bool columnsBuilt = false;

			// Datei zum lesen öffnen
			using (StreamReader sr = File.OpenText(filename)) {
				string current = null;
				while ((current = sr.ReadLine()) != null) {
					if (columnsBuilt)
						firstLineIsHeader = false;

					if (firstLineIsHeader && !columnsBuilt) {
						// Erst den header lesen um die DataTable zu initialisieren
						string[] colHeaders = current.Split(sep);
						foreach (string s in colHeaders) {
							dt.Columns.Add(s, typeof(string)); // Nur strings
						}
						columnsBuilt = true;
					} else if (!columnsBuilt) {
						// O.k. dann musst du halt über den Index zugreifen ;-)
						string[] colHeaders = current.Split(sep);
						for (int i = 0; i < colHeaders.Length; i++) {
							dt.Columns.Add();
						}
						columnsBuilt = true;
					}

					if (columnsBuilt && !firstLineIsHeader) {
						// Normale Datensätze
						string[] cells = current.Split(sep);
						DataRow row = dt.NewRow();

						for (int i = 0; i < cells.Length; i++) {
							row[i] = cells[i];
						}
						dt.Rows.Add(row);
					}
				}
			}

			return dt;
		}
	}
}