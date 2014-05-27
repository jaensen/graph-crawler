using System;
using System.Collections.Generic;

namespace Liv.io.Utils
{
	public interface IUniqueIdBlockProvider
	{
		Queue<string> GetUniqueIdBlock();
	}
}