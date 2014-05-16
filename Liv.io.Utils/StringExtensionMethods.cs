using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Liv.io.Utils {
	public static class StringExtensionMethods {

		public static string Left(this string s, int length) {
			if (s == null)
				throw new NullReferenceException();

			length = Math.Min(s.Length, length);
			return s.Substring(0, length);
		}

		public static string Right(this string s, int length) {
			if (s == null)
				throw new NullReferenceException();

			length = Math.Min(s.Length, length);
			return s.Substring(s.Length - length, length);
		}

		public static string ReplaceAll(this string str, string[] search, string replace) {
			if (str == null)
				throw new NullReferenceException();
			if (search == null)
				throw new ArgumentNullException("search");
			if (replace == null)
				throw new ArgumentNullException("replace");

			foreach (var searchString in search) {
				if (string.IsNullOrWhiteSpace(searchString))
					continue;
				str = str.Replace(searchString, replace);
			}

			return str;
		}

		#region WordWrap

		public static string[] WordWrap(this string str, int width, string[] seperators) {

			if (str == null)
				throw new NullReferenceException();
			if (seperators == null)
				throw new ArgumentNullException("seperators", "The seperators array can not be null");
			if (seperators.Length == 0)
				throw new ArgumentException("The seperators array can not be empty", "seperators");
			if (width <= 0)
				throw new ArgumentException("The width must be greater than zero.", "width");

			StringBuilder lineBuilder = new StringBuilder(width);
			List<string> wrappedLines = new List<string>();

			string[] words = str.Split(seperators, StringSplitOptions.RemoveEmptyEntries);

			foreach (string word in words) {

				bool wordWritten = false;

				while (!wordWritten) {

					if (lineBuilder.Length > 0) {

						if (lineBuilder.Length + word.Length + 1 <= width) {

							lineBuilder.Append(" " + word);
							wordWritten = true;
						} else {

							wrappedLines.Add(lineBuilder.ToString());
							lineBuilder.Length = 0;
						}
					} else {

						if (word.Length <= width) {

							lineBuilder.Append(word);
							wordWritten = true;
						} else {

							string tooLongWordRemains = word;
							while (tooLongWordRemains.Length > width) {

								int length = tooLongWordRemains.Length < width ? tooLongWordRemains.Length : width;

								lineBuilder.Append(tooLongWordRemains.Substring(0, length));
								wrappedLines.Add(lineBuilder.ToString());
								lineBuilder.Length = 0;

								tooLongWordRemains = tooLongWordRemains.Substring(length, tooLongWordRemains.Length - length);
							}
							lineBuilder.Append(tooLongWordRemains);
							wordWritten = true;
						}
					}
				}
			}

			if (lineBuilder.Length > 0)
				wrappedLines.Add(lineBuilder.ToString());

			return wrappedLines.ToArray();
		}

		public static string[] WordWrap(this string str, int width) {
			if (str == null)
				throw new NullReferenceException();

			return str.WordWrap(width, new string[] { " ", "\t", "\r", "\n" });
		}

		#endregion

		#region Search algorithmns

		private const int MaxChars = Char.MaxValue + 1;

		/// <summary>
		/// Boyer-Moore-Horspool search.
		/// </summary>
		/// <param name="str">The string instance</param>
		/// <param name="pattern">The search pattern</param>
		/// <param name="onHit">Calback which will be called when the pattern is found.</param>
		static public void BMHSearch(this string str, string pattern, Action<int> onHit) {
			if (str == null)
				throw new NullReferenceException();

			int n = str.Length;
			int m = pattern.Length;
			int m1 = m - 1;

			int[] skip = CalculateSkip(pattern);

			int s = m1;
			while (s < n) {
				int q = m1;
				while (q >= 0 && s < n) {
					int s2 = s;
					while (q >= 0 && pattern[q] == str[s2]) {
						q--;
						s2--;
					}
					if (q >= 0) {
						s += skip[str[s]];
						q = m1;
					}
				}

				if (q < 0) {
					onHit(s - m + 1);
					s++;
				}
			}
		}

		static int[] CalculateSkip(string pattern) {
			int[] skipP = new int[MaxChars];
			int m = pattern.Length;

			for (int i = 0; i < MaxChars; i++)
				skipP[i] = m;

			for (int i = m - 1, j = 0; i > 0; j++, i--)
				skipP[pattern[j]] = i;

			return skipP;
		}

		public static void NaiveSearch(this string str, string pattern, Action<int> onHit) {
			if (str == null)
				throw new NullReferenceException();

			int max = str.Length - pattern.Length;
			for (int s = 0; s <= max; s++)
				if (str.Substring(s, pattern.Length) == pattern)
					onHit(s);
		}

		static int[] CalculateDelta(string pattern) {
			int m = pattern.Length;
			pattern += '\0';
			int[] flag = new int[MaxChars];
			int[] delta = new int[MaxChars * (m + 1)];

			for (int j = 0; j < m; j++)
				flag[pattern[j]] = 1;

			int d = 0;
			for (int j = 0; j < MaxChars; j++) {
				if (flag[j] > 0) {
					for (int i = 0; i <= m; i++) {
						int k = i;
						for (int q = i, s = 0; k >= 0; q--, s++, k--)
							if (pattern[q] == j && pattern.Substring(s, k) == pattern.Substring(0, k))
								break;

						delta[d++] = k + 1;
					}
				} else
					d += m + 1;
			}

			return delta;
		}

		/// <summary>
		/// Deterministic finite automat search.
		/// </summary>
		/// <param name="str"></param>
		/// <param name="pattern"></param>
		/// <param name="onHit"></param>
		public static void DFASearch(this string str, string pattern, Action<int> onHit) {
			if (str == null)
				throw new NullReferenceException();

			int n = str.Length;
			int m = pattern.Length;

			int[] delta = CalculateDelta(pattern);

			int s = 0;
			int j = 0;
			while (s < n) {
				j = delta[str[s] * (m + 1) + j];
				if (j == m)
					onHit(s - m + 1);
				s++;
			}
		}


		#endregion
	}
}