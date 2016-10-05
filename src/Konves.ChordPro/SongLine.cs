using System.Collections.Generic;
using System.Linq;

namespace Konves.ChordPro
{
	public sealed class SongLine : ILine
	{
		public SongLine()
		{
			Blocks = new List<Block>();
		}

		public SongLine(IEnumerable<Block> blocks)
		{
			Blocks = blocks as List<Block> ?? blocks.ToList();
		}

		public List<Block> Blocks { get; set; }

		public override string ToString()
		{
			return string.Join("   ", Blocks?.Select(s => s.ToString()) ?? Enumerable.Empty<string>());
		}
	}
}
