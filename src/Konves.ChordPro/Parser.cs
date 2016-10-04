using Konves.ChordPro.DirectiveParsers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Konves.ChordPro
{
	internal sealed class xParser
	{
		internal xParser(TextReader textReader) : this(textReader, null)
		{
		}

		internal xParser(TextReader textReader, IEnumerable<DirectiveParser> customParsers)
		{
			_textReader = textReader;
			_directiveParsers = GetDirectiveParserDictionary(customParsers);
		}

		internal static IReadOnlyDictionary<string, DirectiveParser> GetDirectiveParserDictionary(IEnumerable<DirectiveParser> customParsers)
		{
			Dictionary<string, DirectiveParser> index = new Dictionary<string, DirectiveParser>();

			foreach (DirectiveParser parser in _defaultDirectiveParsers ?? Enumerable.Empty<DirectiveParser>())
			{
				index[parser.LongName] = parser;
				index[parser.ShortName] = parser;
			}

			foreach (DirectiveParser parser in customParsers ?? Enumerable.Empty<DirectiveParser>())
			{
				index[parser.LongName] = parser;
				index[parser.ShortName] = parser;
			}

			return index;
		}

		internal IEnumerable<ILine> Parse()
		{
			string line;
			while ((line = _textReader.ReadLine()) != null)
			{
				if (_isInTab)
				{
					if (GetLineType(line) == LineType.Directive && ParseDirective(line) is EndOfTabDirective)
						yield return new EndOfTabDirective();
					else
						yield return new TabLine(line);
				}
				else
				{
					switch (GetLineType(line))
					{
						case LineType.Comment:
							// Do nothing
							break;
						case LineType.Directive:
							yield return ParseDirective(line);
							break;
						case LineType.Text:
							yield return ParseSongLine(line);
							break;
						case LineType.Whitespace:
							yield return new SongLine();
							break;
					}
				}

				_lineNumber++;
			}
		}

		internal Directive ParseDirective(string line)
		{
			DirectiveComponents components;
			DirectiveParser parser;
			Directive directive;
			if (DirectiveComponents.TryParse(line, out components) && _directiveParsers.TryGetValue(components.Key, out parser) && parser.TryParse(components, out directive))
			{
				if (directive is StartOfTabDirective)
					_isInTab = true;

				if (directive is EndOfTabDirective)
					_isInTab = false;

				return directive;
			}
			else
			{
				throw new FormatException($"Invalid directive at line {_lineNumber}.");
			}
		}

		internal SongLine ParseSongLine(string line)
		{
			return new SongLine(SplitIntoBlocks(line).Select(ParseBlock));
		}

		internal static IEnumerable<string> SplitIntoBlocks(string line)
		{
			int start = 0;
			bool isInBlock = false;
			bool isInChord = false;
			for (int i = 0; i < line.Length; i++)
			{
				if (isInBlock && !isInChord)
				{
					if (char.IsWhiteSpace(line[i]))
					{
						yield return line.Substring(start, i - start);
						isInBlock = false;
						continue;
					}
					else if (i == line.Length - 1)
					{
						yield return line.Substring(start, i - start + 1);
						isInBlock = false;
						continue;
					}
				}

				if (!isInBlock && !char.IsWhiteSpace(line[i]))
				{
					isInBlock = true;
					start = i;
				}

				if (!isInChord && line[i] == '[')
				{
					isInChord = true;
				}

				if (isInChord && line[i] == ']')
				{
					isInChord = false;
				}
			}
		}

		internal static IEnumerable<string> SplitIntoSyllables(string block)
		{
			int start = 0;
			bool isInChord = false;
			for (int i = 0; i < block.Length; i++)
			{

				if (!isInChord && block[i] == '[')
				{
					if (i > 0)
					{
						yield return block.Substring(start, i - start);
						start = i;
					}

					isInChord = true;
				}

				if (isInChord && block[i] == ']')
				{
					isInChord = false;
				}

				if (i == block.Length - 1)
					yield return block.Substring(start, i - start + 1);
			}
		}

		internal Block ParseBlock(string block)
		{
			List<string> syllables = SplitIntoSyllables(block).ToList();

			Chord chord;

			if (syllables.Count == 1 && TryParseChord(syllables.Single(), out chord))
				return chord;

			return new Word(syllables.Select(ParseSyllable));
		}

		internal static bool TryParseChord(string s, out Chord chord)
		{
			if (s.StartsWith("[") && s.EndsWith("]") && s.Length > 2)
			{
				chord = new Chord(s.Substring(1, s.Length - 2).Trim());
				return true;
			}

			chord = null;
			return false;
		}

		internal Syllable ParseSyllable(string syllable)
		{
			int i = syllable.IndexOf("]");

			Chord chord = null;

			if (i > -1)
			{
				if (!TryParseChord(syllable.Substring(0, i + 1), out chord))
					throw new FormatException($"Incorrect chord format at line {_lineNumber}.");
			}

			string text = syllable.Substring(i + 1);

			return new Syllable(chord, text.Length == 0 ? null : text);
		}

		internal static LineType GetLineType(string line)
		{
			if (line == null)
				throw new ArgumentNullException(nameof(line));

			for (int i = 0; i < line.Length; i++)
			{
				if (line[i] == '#')
					return LineType.Comment;
				else if (line[i] == '{')
					return LineType.Directive;
				else if (!char.IsWhiteSpace(line[i]))
					return LineType.Text;
			}

			return LineType.Whitespace;
		}

		internal PageType GetPageType(string s)
		{
			switch (s)
			{
				case "a4": return PageType.A4;
				case "letter": return PageType.Letter;
				default: throw new FormatException($"Invalid directive value at line {_lineNumber}.");
			}
		}

		internal Alignment GetAlignment(string s)
		{
			switch (s)
			{
				case "left": return Alignment.Left;
				case "center": return Alignment.Center;
				default: throw new FormatException($"Invalid directive value at line {_lineNumber}.");
			}
		}

		internal enum LineType
		{
			Comment,
			Text,
			Directive,
			Whitespace
		}

		internal class DirectiveParts
		{
			public string Key { get; set; }
			public string SubKey { get; set; }
			public string Value { get; set; }
		}

		readonly TextReader _textReader;
		readonly IReadOnlyDictionary<string, DirectiveParser> _directiveParsers;
		internal bool _isInTab = false;
		int _lineNumber = 0;

		static readonly IReadOnlyCollection<DirectiveParser> _defaultDirectiveParsers = new DirectiveParser[] {
			new ChordColourParser(),
			new ChordFontParser(),
			new ChordSizeParser(),
			new ColumnsParser(),
			new ColumnBreakParser(),
			new CommentParser(),
			new CommentBoxParser(),
			new CommentItalicParser(),
			new DefineParser(),
			new EndOfChorusParser(),
			new EndOfTabParser(),
			new GridParser(),
			new NewPageParser(),
			new NewPhysicalPageParser(),
			new NewSongParser(),
			new NoGridParser(),
			new PageTypeParser(),
			new StartOfChorusParser(),
			new StartOfTabParser(),
			new SubtitleParser(),
			new TextFontParser(),
			new TextSizeParser(),
			new TitleParser(),
			new TitlesParser()
		};
	}
}
