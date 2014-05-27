using System;
using System.IO;
using System.Collections.Generic;

namespace Liv.io.Utils
{
	public class FilesystemIntUniqueIdBlockProvider : IUniqueIdBlockProvider
	{
		public string CounterFile {
			get;
			set;
		}

		public int BlockSize {
			get;
			set;
		}

		public FilesystemIntUniqueIdBlockProvider (int blockSize = 500)
		{
			BlockSize = blockSize;

			CounterFile = Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.Personal), ".Liv.io.Utils.FilesystemIntUniqueIdBlockProvider");
			
			EnsureCounterFile ();
		}

		public FilesystemIntUniqueIdBlockProvider (string counterFile, int blockSize = 500)
			:this(blockSize)
		{
			if (string.IsNullOrWhiteSpace (counterFile))
				throw new ArgumentNullException ("The counter file path is not allowed to be null or whitespace");

			CounterFile = counterFile;
		}

		private void EnsureCounterFile ()
		{
			if (!File.Exists (CounterFile))
				File.WriteAllText (CounterFile, "0");
		}

		private readonly object _sync = new object ();

		public Queue<string> GetUniqueIdBlock ()
		{
			lock (_sync) {
				string currentCounterContent = File.ReadAllText (CounterFile);
				int currentCounterInt = 0;

				if (!int.TryParse (currentCounterContent, out currentCounterInt))
					throw new Exception (string.Format ("The {0} file does not contain a valid integer.", CounterFile));

				Queue<string> block = new Queue<string> (BlockSize);
				int to = currentCounterInt + BlockSize;
				for (int i = currentCounterInt; i <  to; i++) 
					block.Enqueue ((currentCounterInt++).ToString ());

				File.WriteAllText (CounterFile, currentCounterInt.ToString ());

				return block;
			}
		}
	}
}