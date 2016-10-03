using System.Collections.Generic;
using System.Collections.ObjectModel;
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

	public interface ILine { }

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

	public sealed class TabLine : ILine
	{
		public TabLine()
		{
			Text = string.Empty;
		}

		public TabLine(string text)
		{
			Text = text;
		}

		public string Text { get; set; }

		public override string ToString()
		{
			return Text;
		}
	}

	public abstract class Block { }

	public sealed class Chord : Block
	{
		public Chord(string text)
		{
			Text = text;
		}

		public string Text { get; set; }

		public override string ToString()
		{
			return string.IsNullOrEmpty(Text) ? string.Empty : $"[{Text}]";
		}
	}

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

	public sealed class Syllable
	{
		public Syllable(Chord chord, string text)
		{
			Chord = chord;
			Text = text;
		}
		public Chord Chord { get; set; }
		public string Text { get; set; }

		public override string ToString()
		{
			return $"{Chord?.ToString()}{Text}";
		}
	}


	public abstract class Directive : ILine
	{
	}

	public class NewSongDirective : Directive { }

	public sealed class TitleDirective : Directive
	{
		public TitleDirective(string text)
		{
			Text = text;
		}

		public string Text { get; set; }
	}

	public sealed class SubtitleDirective : Directive
	{
		public SubtitleDirective(string text)
		{
			Text = text;
		}

		public string Text { get; set; }
	}

	public sealed class CommentDirective : Directive
	{
		public CommentDirective(string text)
		{
			Text = text;
		}

		public string Text { get; set; }
	}

	public sealed class CommentItalicDirective : Directive
	{
		public CommentItalicDirective(string text)
		{
			Text = text;
		}

		public string Text { get; set; }
	}

	public sealed class CommentBoxDirective : Directive
	{
		public CommentBoxDirective(string text)
		{
			Text = text;
		}

		public string Text { get; set; }
	}

	public sealed class StartOfChorusDirective : Directive { }

	public sealed class EndOfChorusDirective : Directive { }

	public sealed class StartOfTabDirective : Directive { }

	public sealed class EndOfTabDirective : Directive { }

	public sealed class DefineDirective : Directive
	{
		public DefineDirective(string chord, string definition)
		{
			Chord = chord;
			Definition = definition;
		}

		public string Chord { get; set; }
		public string Definition { get; set; }
	}

	public sealed class TextFontDirective : Directive
	{
		public TextFontDirective(string fontFamily)
		{
			FontFamily = fontFamily;
		}

		public string FontFamily { get; set; }
	}

	public sealed class TextSizeDirective : Directive
	{
		public TextSizeDirective(int fontSize)
		{
			FontSize = fontSize;
		}

		public int FontSize { get; set; }
	}

	public sealed class ChordFontDirective : Directive
	{
		public ChordFontDirective(string fontFamily)
		{
			FontFamily = fontFamily;
		}

		public string FontFamily { get; set; }
	}

	public sealed class ChordSizeDirective : Directive
	{
		public ChordSizeDirective(int fontSize)
		{
			FontSize = fontSize;
		}

		public int FontSize { get; set; }
	}

	public sealed class ChordColourDirective : Directive
	{
		public ChordColourDirective(string colour)
		{
			Colour = colour;
		}

		public string Colour { get; set; }
	}

	public sealed class NoGridDirective : Directive { }

	public sealed class GridDirective : Directive { }

	public sealed class TitlesDirective : Directive
	{
		public TitlesDirective(Alignment flush)
		{
			Flush = flush;
		}

		public Alignment Flush { get; set; }
	}

	public sealed class NewPageDirective : Directive { }

	public sealed class NewPhysicalPageDirective : Directive { }

	public sealed class ColumnsDirective : Directive
	{
		public ColumnsDirective(int number)
		{
			Number = number;
		}

		public int Number { get; set; }
	}

	public sealed class PageTypeDirective : Directive
	{
		public PageTypeDirective(PageType pageType)
		{
			PageType = pageType;
		}

		public PageType PageType { get; set; }
	}

	public sealed class ColumnBreakDirective : Directive { }

	public enum Alignment
	{
		Center,
		Left
	}

	public enum PageType
	{
		A4,
		Letter
	}
}
