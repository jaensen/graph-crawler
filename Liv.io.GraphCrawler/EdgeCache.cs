using System;
using System.Data;
using System.IO;
using Liv.io.Utils;
using System.Collections.Generic;
using System.Text;

namespace Liv.io.GraphCrawler
{
	public class EdgeCache
	{
		public DataTable EdgesTable {
			get;
			set;
		}

		public EdgeCache ()
		{
			Init ();
		}

		public void Init ()
		{
			EdgesTable = new DataTable ();

			EdgesTable.Columns.Add ("Source", typeof(string));
			EdgesTable.Columns.Add ("Target", typeof(string));
			EdgesTable.Columns.Add ("Id", typeof(string));
			EdgesTable.Columns.Add ("Type", typeof(string));
			EdgesTable.Columns.Add ("Label", typeof(string));
			EdgesTable.Columns.Add ("Weight", typeof(float));

			_fromIdMap = new Dictionary<string, List<string>> ();
			_toIdMap = new Dictionary<string, List<string>> ();
			_idRowMap = new Dictionary<string, DataRow> ();
			_groupedEdgeTypes = new Dictionary<string, int> ();
		}

		public Dictionary<string,DataRow> IdRowMap {
			get {
				if (_idRowMap == null) {
					_idRowMap = new Dictionary<string, DataRow> ();
					
					foreach (DataRow row in EdgesTable.Rows) {
						_idRowMap.Add ((string)row ["Id"], row);
					}
				}

				return _idRowMap;
			}
		}

		public Dictionary<string, DataRow> _idRowMap;

		public Dictionary<string,List<string>> FromIdMap {
			get {
				if (_fromIdMap == null) {
					_fromIdMap = new Dictionary<string,List<string>> ();
										
					foreach (DataRow row in EdgesTable.Rows) {						
						List<string> edgeIDs = _fromIdMap.GetOrAdd (row ["Source"] as string, (s) => {
							return new List<string> ();
						});
						edgeIDs.Add (row ["Id"] as string);
					}
				}

				return _fromIdMap;
			}
		}

		private Dictionary<string,List<string>> _fromIdMap;

		public Dictionary<string,List<string>> ToIdMap {
			get {
				if (_toIdMap == null) {
					_toIdMap = new Dictionary<string,List<string>> ();

					foreach (DataRow row in EdgesTable.Rows) {						
						List<string> edgeIDs = _toIdMap.GetOrAdd (row ["Target"] as string, (s) => {
							return new List<string> ();
						});
						edgeIDs.Add (row ["Id"] as string);
					}
				}

				return _toIdMap;
			}
		}

		public Dictionary<string, int> GroupedEdgeTypes {
			get {
				return _groupedEdgeTypes;
			}
		}

		private  Dictionary<string, int> _groupedEdgeTypes = new  Dictionary<string, int> ();
		private Dictionary<string,List<string>> _toIdMap;

		public DataRow AddEdge (string source,
		                        string target,
		                        string id,
		                        string type,
		                        string label,
		                        float weight)
		{
			DataRow row = EdgesTable.NewRow ();

			row ["Source"] = source;
			row ["Target"] = target;
			row ["Id"] = id;
			row ["Type"] = type;
			row ["Label"] = label;
			row ["Weight"] = weight;

			IdRowMap.Add (id, row);

			EdgesTable.Rows.Add (row);

			List<string> fromIdMapValues = FromIdMap.GetOrAdd (source, (s) => new List<string> ());
			fromIdMapValues.Add (id);

			List<string> toIdMapValues = ToIdMap.GetOrAdd (source, (s) => new List<string> ());
			toIdMapValues.Add (id);

			int labelOccurences = 0;
			if (!GroupedEdgeTypes.TryGetValue (label, out labelOccurences)) 
				GroupedEdgeTypes.Add (label, labelOccurences++);
			else 
				GroupedEdgeTypes [label] = labelOccurences++;

			return row;
		}

		public DataRow[] FindEdges (string labelStartsWith)
		{
			return EdgesTable.Select (string.Format ("Label LIKE '{0}%'", labelStartsWith));
		}

		public void RemoveEdge (string edgeId)
		{
			if (!_idRowMap.ContainsKey (edgeId))
				throw new Exception (string.Format ("The edge with the supplied id {0} could not be found for deletion.", edgeId));

			var edgeToRemove = _idRowMap [edgeId];

			_idRowMap.Remove (edgeId);

			string toNodeId = edgeToRemove ["Target"] as string;
			string fromNodeId = edgeToRemove ["Source"] as string;
			string edgeLabel = edgeToRemove ["Label"] as string;

			var fromNodeIdList = _fromIdMap [fromNodeId];
			fromNodeIdList.Remove (edgeId);

			var toNodeIdList = _toIdMap [toNodeId];
			toNodeIdList.Remove (edgeId);

			int labelOccurences = GroupedEdgeTypes [edgeLabel];
			if (labelOccurences-- == 0)
				GroupedEdgeTypes.Remove (edgeLabel);
			else  
				GroupedEdgeTypes [edgeLabel] = labelOccurences;

			EdgesTable.Rows.Remove (edgeToRemove);
		}

		public DataRow[] LoadEdges (string[] edges)
		{
			StringBuilder inQueryBuilder = new StringBuilder ();

			for (int i = 0; i < edges.Length; i++) {
				inQueryBuilder.Append ("'" + edges [i] + "'" + (i < edges.Length - 1 ? "," : ""));
			}

			return EdgesTable.Select (string.Format ("Id IN ({0})", inQueryBuilder.ToString ()));
		}

		public void Load (string source)
		{
			Init ();

			DataTable tempEdgesTable = CsvReader.Read (source, '|', true);

			foreach (DataRow row in tempEdgesTable.Rows) {

				float weight = 0;
				float.TryParse (row ["Weight"] as string, out weight);

				AddEdge (
					row ["Source"] as string,
					row ["Target"] as string,
					row ["Id"] as string,
					row ["Type"]  as string,
					row ["Label"] as string,
					weight
				);
			}
		}

		public void Save (string destinationFile)
		{
			using (TextWriter textWriter = File.CreateText(destinationFile)) {
				CsvWriter.WriteToStream (textWriter, EdgesTable, true, false, '|');
			}
		}
	}
}