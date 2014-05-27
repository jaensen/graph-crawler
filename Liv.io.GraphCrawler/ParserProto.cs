using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace Liv.io.GraphCrawler
{
	public class ParserProto
	{
		public ParserProto ()
		{
		}

		public string StripTags(string input) {
			return Regex.Replace (input, "<.*?>", "");
		}

		public  IEnumerable<string> GetWords(string input) {
			foreach (var word in Regex.Split(input, "\\W")) {
				yield return word.Trim();				
			}
		}
	}
}