using System.Collections.Generic;
using System.Linq;

namespace Konves.ChordPro
{
	public sealed class Document
	{
		public Document(IEnumerable<ILine> lines)
		{
			Lines = lines as List<ILine> ?? lines.ToList();
		}

		public List<ILine> Lines { get; set; }
	}
}
