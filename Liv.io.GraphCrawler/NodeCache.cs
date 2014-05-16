using System;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Linq;
using Liv.io.Utils;
using System.Text;

namespace Liv.io.GraphCrawler
{
	public class NodeCache
	{
		public DataTable NodesTable {
			get;
			set;
		}

		public DataView Classes {
			get {
				if (_classes == null) {
					_classes = new DataView (NodesTable);
					_classes.RowFilter = "Type = 'Class'";
				}
				return _classes;
			}
		}

		private DataView _classes;

		public DataView Objects {
			get {
				if (_objects == null) {
					_objects = new DataView (NodesTable);
					_objects.RowFilter = "Type = 'Instance'";
				}
				return _objects;
			}
		}

		private DataView _objects;

		public NodeCache ()
		{
			Init ();
		}

		public void Init ()
		{
			NodesTable = new DataTable ();

			NodesTable.Columns.Add ("Id", typeof(string));
			NodesTable.Columns.Add ("Label", typeof(string));
			NodesTable.Columns.Add ("Type", typeof(string));
		}

		public DataRow GetNodeByLabel (string label)
		{
			return NodesTable.Select (string.Format ("Label = '{0}'", label)).FirstOrDefault ();
		}

		public DataRow GetNodeById (string id)
		{
			return NodesTable.Select (string.Format ("Id = '{0}'", id)).FirstOrDefault ();
		}

		/// <summary>
		/// Finds the nodes by matching the begin of the label..
		/// </summary>
		/// <param name="label">The label or a part of it</param>
		public DataRow[] FindNodesByLabel (string label)
		{
			return NodesTable.Select (string.Format ("Label LIKE '{0}%'", label));
		}

		public DataRow AddNode (string id, string label, string type)
		{
			DataRow newNode = NodesTable.NewRow ();
			newNode ["Id"] = id;
			newNode ["Label"] = label;
			newNode ["Type"] = type;

			NodesTable.Rows.Add (newNode);
			return newNode;
		}

		public void RemoveNode (string id)
		{
			var node = GetNodeById (id);
			if (node == null)
				throw new Exception (string.Format ("Could not find the node with the id {0} for deletion.", id));

			NodesTable.Rows.Remove (node);
		}

		public DataRow[] LoadNodes (string[] nodes)
		{
			StringBuilder inQueryBuilder = new StringBuilder ();

			for (int i = 0; i < nodes.Length; i++) {
				inQueryBuilder.Append ("'" + nodes [i] + "'" + (i < nodes.Length - 1 ? "," : ""));
			}

			return NodesTable.Select (string.Format ("Id IN ({0})", inQueryBuilder.ToString ()));
		}

		public void Load (string source)
		{
			Init ();

			DataTable nodesTableTemp = CsvReader.Read (source, '|', true);

			foreach (DataRow row in nodesTableTemp.Rows) {				
				string nodeId = row [0] as string;
				string nodeLabel = row [1] as string;
				string nodeType = row [2] as string;

				AddNode (nodeId, nodeLabel, nodeType);
			}
		}

		public void Save (string destination)
		{
			using (TextWriter textWriter = File.CreateText(destination.ToString())) {
				CsvWriter.WriteToStream (textWriter, NodesTable, true, false, '|');
			}
		}
	}
}