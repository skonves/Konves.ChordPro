using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konves.ChordPro
{
	public sealed class Document
	{
		public Document(IEnumerable<ILine> lines)
		{
			Lines = lines is ReadOnlyCollection<ILine> ? (ReadOnlyCollection<ILine>)lines : new ReadOnlyCollection<ILine>(lines.ToList());
		}

		public ReadOnlyCollection<ILine> Lines { get; }
	}

	public interface ILine { }

	public sealed class Line : ILine
	{
		public Line(IEnumerable<Block> blocks)
		{
			Blocks = blocks is ReadOnlyCollection<Block> ? (ReadOnlyCollection<Block>)blocks : new ReadOnlyCollection<Block>(blocks.ToList());
		}

		public ReadOnlyCollection<Block> Blocks { get; }
	}

	public abstract class Block
	{

	}

	public sealed class Chord : Block
	{
		public Chord(string text)
		{
			Text = text;
		}

		public string Text { get; }

		public override string ToString()
		{
			return string.IsNullOrEmpty(Text) ? string.Empty : $"[{Text}]";
		}
	}

	public sealed class Word : Block
	{
		public Word(string text)
		{
			Syllables = new ReadOnlyCollection<Syllable>(new[] { new Syllable(null, text) });
		}

		public Word(IEnumerable<Syllable> syllables)
		{
			Syllables =
				syllables is ReadOnlyCollection<Block> ?
				(ReadOnlyCollection<Syllable>)syllables :
				new ReadOnlyCollection<Syllable>(syllables is List<Syllable> ?
					(List<Syllable>)syllables :
					syllables.ToList());
		}

		public ReadOnlyCollection<Syllable> Syllables { get; }

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
		public Chord Chord { get; }
		public string Text { get; }

		public override string ToString()
		{
			return $"{Chord?.ToString()}{Text}";
		}
	}


	public interface IDirective : ILine
	{
		DirectiveType Type { get; }
	}

	public abstract class PreambleDirective : IDirective
	{
		public DirectiveType Type { get { return DirectiveType.Preamble; } }
	}

	public abstract class FormattingDirective : IDirective
	{
		public DirectiveType Type { get { return DirectiveType.Formatting; } }
	}

	public abstract class OutputDirective : IDirective
	{
		public DirectiveType Type { get { return DirectiveType.Output; } }
	}

	public class NewSongDirective : PreambleDirective { }

	public sealed class TitleDirective : PreambleDirective
	{
		public TitleDirective(string text)
		{
			Text = text;
		}

		public string Text { get; }
	}

	public sealed class SubtitleDirective : PreambleDirective
	{
		public SubtitleDirective(string text)
		{
			Text = text;
		}

		public string Text { get; }
	}

	public sealed class CommentDirective : FormattingDirective
	{
		public CommentDirective(string text)
		{
			Text = text;
		}

		public string Text { get; }
	}

	public sealed class CommentItalicDirective : FormattingDirective
	{
		public CommentItalicDirective(string text)
		{
			Text = text;
		}

		public string Text { get; }
	}

	public sealed class CommentBoxDirective : FormattingDirective
	{
		public CommentBoxDirective(string text)
		{
			Text = text;
		}

		public string Text { get; }
	}

	public sealed class StartOfChorusDirective : FormattingDirective { }

	public sealed class EndOfChorusDirective : FormattingDirective { }

	public sealed class StartOfTabDirective : FormattingDirective { }

	public sealed class EndOfTabDirective : FormattingDirective { }

	public sealed class DefineDirective : FormattingDirective
	{
		public DefineDirective(string definition)
		{
			Definition = definition;
		}

		public string Definition { get; }
	}

	public sealed class TextFontDirective : OutputDirective
	{
		public TextFontDirective(string fontFamily)
		{
			FontFamily = fontFamily;
		}

		public string FontFamily { get; }
	}

	public sealed class TextSizeDirective : OutputDirective
	{
		public TextSizeDirective(int fontSize)
		{
			FontSize = fontSize;
		}

		public int FontSize { get; }
	}

	public sealed class ChordFontDirective : OutputDirective
	{
		public ChordFontDirective(string fontFamily)
		{
			FontFamily = fontFamily;
		}

		public string FontFamily { get; }
	}

	public sealed class ChordSizeDirective : OutputDirective
	{
		public ChordSizeDirective(int fontSize)
		{
			FontSize = fontSize;
		}

		public int FontSize { get; }
	}

	public sealed class ChordColourDirective : OutputDirective
	{
		public ChordColourDirective(string colour)
		{
			Colour = colour;
		}

		public string Colour { get; }
	}

	public sealed class NoGridDirective : OutputDirective { }

	public sealed class GridDirective : OutputDirective { }

	public sealed class TitlesDirective : OutputDirective
	{
		public TitlesDirective(Alignment flush)
		{
			Flush = flush;
		}

		public Alignment Flush { get; }
	}

	public sealed class NewPageDirective : OutputDirective { }

	public sealed class NewPhysicalPageDirective : OutputDirective { }

	public sealed class ColumnsDirective : OutputDirective
	{
		public ColumnsDirective(int number)
		{
			Number = number;
		}

		public int Number { get; }
	}

	public sealed class PageTypeDirective : OutputDirective
	{
		public PageTypeDirective(PageType pageType)
		{
			PageType = pageType;
		}

		public PageType PageType { get; }
	}

	public sealed class ColumnBreakDirective : OutputDirective { }

	public enum DirectiveType
	{
		Preamble,
		Formatting,
		Output
	}

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

	//public enum DirectiveType
	//{
	//	NewSong,
	//	Title,
	//	Subtitle,
	//	Comment,
	//	CommentItalic,
	//	CommentBox,
	//	StartOfChorus,
	//	EndOfChorus,
	//	StartOfTab,
	//	EndOfTab,
	//	Define,
	//	TextFont,
	//	TextSize,
	//	ChordFont,
	//	ChordSize,
	//	ChordColour,
	//	NoGrid,
	//	Grid,
	//	Titles,
	//	NewPage,
	//	NewPhysicalPage,
	//	Columns,
	//	ColumnBreak,
	//	PageType,
	//}
}
