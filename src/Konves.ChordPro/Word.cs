using System.Collections.Generic;
using System.Linq;

namespace Konves.ChordPro
{
	public sealed class Word : Block
	{
		public Word(string text)
		{
			Syllables = new List<Syllable> { new Syllable(null, text) };
		}

		public Word(IEnumerable<Syllable> syllables)
		{
			Syllables = syllables as List<Syllable> ?? syllables.ToList();
		}

		public List<Syllable> Syllables { get; set; }

		public override string ToString()
		{
			return string.Join("", Syllables?.Select(s => s.ToString()) ?? Enumerable.Empty<string>());
		}
	}
}
