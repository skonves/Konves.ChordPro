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
		internal xParser(TextReader textReader)
		{
			_textReader = textReader;
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
			var parts = GetDirectiveParts(line);

			switch (parts.Key)
			{
				case "chordcolour":
					{
						if (string.IsNullOrWhiteSpace(parts.Value))
							throw new FormatException($"Expected directive value at line {_lineNumber}.");
						return new ChordColourDirective(parts.Value);
					}
				case "chordfont":
				case "cf":
					{
						if (string.IsNullOrWhiteSpace(parts.Value))
							throw new FormatException($"Expected directive value at line {_lineNumber}.");
						return new ChordFontDirective(parts.Value);
					}
				case "chordsize":
				case "cs":
					{
						int value;
						if (int.TryParse(parts.Value, out value))
							return new ChordSizeDirective(value);
						throw new FormatException($"Expected integer directive value at line {_lineNumber}. Found '{parts.Value}'");
					}
				case "columns":
				case "col":
					{
						int value;
						if (int.TryParse(parts.Value, out value))
							return new ColumnsDirective(value);
						throw new FormatException($"Expected integer directive value at line {_lineNumber}. Found '{parts.Value}'");
					}
				case "column_break":
				case "colb":
					{
						if (!string.IsNullOrWhiteSpace(parts.Value))
							throw new FormatException($"Unexpected directive value at line {_lineNumber}.");
						return new ColumnBreakDirective();
					}
				case "comment":
				case "c":
					{
						if (string.IsNullOrWhiteSpace(parts.Value))
							throw new FormatException($"Expected directive value at line {_lineNumber}.");
						return new CommentDirective(parts.Value);
					}
				case "comment_box":
				case "cb":
					{
						if (string.IsNullOrWhiteSpace(parts.Value))
							throw new FormatException($"Expected directive value at line {_lineNumber}.");
						return new CommentBoxDirective(parts.Value);
					}
				case "comment_italic":
				case "ci":
					{
						if (string.IsNullOrWhiteSpace(parts.Value))
							throw new FormatException($"Expected directive value at line {_lineNumber}.");
						return new CommentItalicDirective(parts.Value);
					}
				case "define":
					{
						if (string.IsNullOrWhiteSpace(parts.SubKey))
							throw new FormatException($"Expected chord name at line {_lineNumber}.");
						if (string.IsNullOrWhiteSpace(parts.Value))
							throw new FormatException($"Expected directive value at line {_lineNumber}.");
						return new DefineDirective(parts.SubKey, parts.Value);
					}
				case "end_of_chorus":
				case "eoc":
					{
						if (!string.IsNullOrWhiteSpace(parts.Value))
							throw new FormatException($"Unexpected directive value at line {_lineNumber}.");
						return new EndOfChorusDirective();
					}
				case "end_of_tab":
				case "eot":
					{
						if (!string.IsNullOrWhiteSpace(parts.Value))
							throw new FormatException($"Unexpected directive value at line {_lineNumber}.");
						_isInTab = false;
						return new EndOfTabDirective();
					}
				case "grid":
				case "g":
					{
						if (!string.IsNullOrWhiteSpace(parts.Value))
							throw new FormatException($"Unexpected directive value at line {_lineNumber}.");
						return new GridDirective();
					}
				case "new_page":
				case "np":
					{
						if (!string.IsNullOrWhiteSpace(parts.Value))
							throw new FormatException($"Unexpected directive value at line {_lineNumber}.");
						return new NewPageDirective();
					}
				case "new_physical_page":
				case "npp":
					{
						if (!string.IsNullOrWhiteSpace(parts.Value))
							throw new FormatException($"Unexpected directive value at line {_lineNumber}.");
						return new NewPhysicalPageDirective();
					}
				case "new_song":
				case "ns":
					{
						if (!string.IsNullOrWhiteSpace(parts.Value))
							throw new FormatException($"Unexpected directive value at line {_lineNumber}.");
						return new NewSongDirective();
					}
				case "no_grid":
				case "ng":
					{
						if (!string.IsNullOrWhiteSpace(parts.Value))
							throw new FormatException($"Unexpected directive value at line {_lineNumber}.");
						return new NoGridDirective();
					}
				case "pagetype":
					{
						if (string.IsNullOrWhiteSpace(parts.Value))
							throw new FormatException($"Expected directive value at line {_lineNumber}.");
						return new PageTypeDirective(GetPageType(parts.Value));
					}
				case "start_of_chorus":
				case "soc":
					{
						if (!string.IsNullOrWhiteSpace(parts.Value))
							throw new FormatException($"Unexpected directive value at line {_lineNumber}.");
						return new StartOfChorusDirective();
					}
				case "start_of_tab":
				case "sot":
					{
						if (!string.IsNullOrWhiteSpace(parts.Value))
							throw new FormatException($"Unexpected directive value at line {_lineNumber}.");
						_isInTab = true;
						return new StartOfTabDirective();
					}
				case "subtitle":
				case "st":
					{
						if (string.IsNullOrWhiteSpace(parts.Value))
							throw new FormatException($"Expected directive value at line {_lineNumber}.");
						return new SubtitleDirective(parts.Value);
					}
				case "textfont":
				case "tf":
					{
						if (string.IsNullOrWhiteSpace(parts.Value))
							throw new FormatException($"Expected directive value at line {_lineNumber}.");
						return new TextFontDirective(parts.Value);
					}
				case "textsize":
				case "ts":
					{
						int value;
						if (int.TryParse(parts.Value, out value))
							return new TextSizeDirective(value);
						throw new FormatException($"Expected integer directive value at line {_lineNumber}. Found '{parts.Value}'");
					}
				case "title":
				case "t":
					{
						if (string.IsNullOrWhiteSpace(parts.Value))
							throw new FormatException($"Expected directive value at line {_lineNumber}.");
						return new TitleDirective(parts.Value);
					}
				case "titles":
					{
						if (string.IsNullOrWhiteSpace(parts.Value))
							throw new FormatException($"Expected directive value at line {_lineNumber}.");
						return new TitlesDirective(GetAlignment(parts.Value));
					}
				default:
					throw new FormatException($"Unknown directive '{parts.Key}' at line {_lineNumber}.");
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

		internal static DirectiveParts GetDirectiveParts(string s)
		{
			Match match = _directiveRegex.Match(s);

			return new DirectiveParts
			{
				Key = match.Groups["key"]?.Value.Trim(),
				SubKey = match.Groups["subkey"]?.Value.Trim(),
				Value = match.Groups["value"]?.Value.Trim(),
			};
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

		static readonly Regex _directiveRegex = new Regex(@"^\s*{\s*(?<key>[^\s:}]+)(?:\s+(?<subkey>[^:}]*))?(?:\:(?<value>[^}]+))?}\s*", RegexOptions.Compiled);
		readonly TextReader _textReader;
		internal bool _isInTab = false;
		int _lineNumber = 0;
	}
}
