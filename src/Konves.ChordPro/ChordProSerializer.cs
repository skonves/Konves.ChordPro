using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Konves.ChordPro.ChordProParser;

namespace Konves.ChordPro
{
	public static class ChordProSerializer
	{
		public static Document Deserialize(Stream stream)
		{
			AntlrInputStream inputStream = new AntlrInputStream(stream);
			ChordProLexer lexer = new ChordProLexer(inputStream);
			CommonTokenStream commonTokenStream = new CommonTokenStream(lexer);
			ChordProParser parser = new ChordProParser(commonTokenStream);

			var lines = Map(parser.document());
			return new Document(lines);
		}

		public static void Serialize(Document document, TextWriter writer, bool shorten = false)
		{
			foreach (ILine line in document.Lines)
			{
				if (line is Directive)
					writer.WriteLine(Map(line as Directive, shorten));
				else if (line is SongLine)
					writer.WriteLine(line.ToString());
				else
					throw new ArgumentException("unknown line type");
			}
		}

		private static string Map(Directive directive, bool shorten)
		{
			if (directive is ChordColourDirective)
				return $"{{chordcolour:{(directive as ChordColourDirective).Colour}}}";
			else if (directive is ChordFontDirective)
				return $"{{{(shorten ? "cf" : "chordfont")}:{(directive as ChordColourDirective).Colour}}}";
			else if (directive is ChordSizeDirective)
				return $"{{{(shorten ? "cs" : "chordsize")}:{(directive as ChordSizeDirective).FontSize}}}";
			else if (directive is ColumnsDirective)
				return $"{{{(shorten ? "col" : "columns")}:{(directive as ColumnsDirective).Number}}}";
			else if (directive is ColumnBreakDirective)
				return $"{{{(shorten ? "colb" : "column_break")}}}";
			else if (directive is CommentDirective)
				return $"{{{(shorten ? "c" : "comment")}:{(directive as CommentDirective).Text}}}";
			else if (directive is CommentBoxDirective)
				return $"{{{(shorten ? "cb" : "comment_box")}:{(directive as CommentBoxDirective).Text}}}";
			else if (directive is CommentItalicDirective)
				return $"{{{(shorten ? "ci" : "comment_italic")}:{(directive as CommentItalicDirective).Text}}}";
			else if (directive is DefineDirective)
				return $"{{define:{(directive as DefineDirective).Definition}}}"; // TODO: fix
			else if (directive is EndOfChorusDirective)
				return $"{{{(shorten ? "eoc" : "end_of_chorus")}}}";
			else if (directive is EndOfTabDirective)
				return $"{{{(shorten ? "eot" : "end_of_tab")}}}";
			else if (directive is GridDirective)
				return $"{{{(shorten ? "g" : "grid")}}}";
			else if (directive is NewPageDirective)
				return $"{{{(shorten ? "np" : "new_page")}}}";
			else if (directive is NewPhysicalPageDirective)
				return $"{{{(shorten ? "npp" : "new_physical_page")}}}";
			else if (directive is NewSongDirective)
				return $"{{{(shorten ? "ns" : "new_song")}}}";
			else if (directive is NoGridDirective)
				return $"{{{(shorten ? "ng" : "no_grid")}}}";
			else if (directive is PageTypeDirective)
				return $"{{pagetype:{(directive as PageTypeDirective).PageType.ToString().ToLower()}}}";
			else if (directive is StartOfChorusDirective)
				return $"{{{(shorten ? "soc" : "start_of_chorus")}}}";
			else if (directive is StartOfTabDirective)
				return $"{{{(shorten ? "sot" : "start_of_tab")}}}";
			else if (directive is SubtitleDirective)
				return $"{{{(shorten ? "st" : "subtitle")}:{(directive as SubtitleDirective).Text}}}";
			else if (directive is TextFontDirective)
				return $"{{{(shorten ? "tf" : "textfont")}:{(directive as TextFontDirective).FontFamily}}}";
			else if (directive is TextSizeDirective)
				return $"{{{(shorten ? "ts" : "textsize")}:{(directive as TextSizeDirective).FontSize}}}";
			else if (directive is TitleDirective)
				return $"{{{(shorten ? "t" : "title")}:{(directive as TitleDirective).Text}}}";
			else if (directive is TitlesDirective)
				return $"{{titles:{(directive as TitlesDirective).Flush.ToString().ToLower()}}}";
			else
				throw new ArgumentException("directive is not of a known directive type");
		}

		private static IEnumerable<ILine> Map(DocumentContext source)
		{
			foreach (IParseTree line in source.children)
			{
				if (line is SongLineContext)
				{
					yield return new SongLine(Map(line as SongLineContext));
				}
				else if (line is DirectiveContext)
				{
					DirectiveContext directive = line as DirectiveContext;

					if (directive.CHORDCOLOR() != null)
						yield return new ChordColourDirective(directive.TEXT().Symbol.Text);
					else if (directive.CHORDFONT() != null)
						yield return new ChordFontDirective(directive.TEXT().Symbol.Text);
					else if (directive.CHORDSIZE() != null)
						yield return new ChordSizeDirective(int.Parse(directive.TEXT().Symbol.Text));
					else if (directive.COLUMNS() != null)
						yield return new ColumnsDirective(int.Parse(directive.TEXT().Symbol.Text));
					else if (directive.COLUMN_BREAK() != null)
						yield return new ColumnBreakDirective();
					else if (directive.COMMENT() != null)
						yield return new CommentDirective(directive.TEXT().Symbol.Text);
					else if (directive.COMMENT_BOX() != null)
						yield return new CommentBoxDirective(directive.TEXT().Symbol.Text);
					else if (directive.COMMENT_ITALIC() != null)
						yield return new CommentItalicDirective(directive.TEXT().Symbol.Text);
					else if (directive.DEFINE() != null)
						yield return new DefineDirective(directive.TEXT().Symbol.Text);
					else if (directive.END_OF_CHORUS() != null)
						yield return new EndOfChorusDirective();
					else if (directive.END_OF_TAB() != null)
						yield return new EndOfTabDirective();
					else if (directive.GRID() != null)
						yield return new GridDirective();
					else if (directive.NEW_PAGE() != null)
						yield return new NewPageDirective();
					else if (directive.NEW_PHYSICAL_PAGE() != null)
						yield return new NewPhysicalPageDirective();
					else if (directive.NEW_SONG() != null)
						yield return new NewSongDirective();
					else if (directive.NO_GRID() != null)
						yield return new NoGridDirective();
					else if (directive.PAGETYPE() != null)
						yield return new PageTypeDirective((PageType)Enum.Parse(typeof(PageType), directive.TEXT().Symbol.Text));
					else if (directive.START_OF_CHORUS() != null)
						yield return new StartOfChorusDirective();
					else if (directive.START_OF_TAB() != null)
						yield return new StartOfTabDirective();
					else if (directive.SUBTITLE() != null)
						yield return new SubtitleDirective(directive.TEXT().Symbol.Text);
					else if (directive.TEXTFONT() != null)
						yield return new TextFontDirective(directive.TEXT().Symbol.Text);
					else if (directive.TEXTSIZE() != null)
						yield return new TextSizeDirective(int.Parse(directive.TEXT().Symbol.Text));
					else if (directive.TITLE() != null)
						yield return new TitleDirective(directive.TEXT().Symbol.Text);
					else if (directive.TITLES() != null)
						yield return new TitlesDirective((Alignment)Enum.Parse(typeof(Alignment), directive.TEXT().Symbol.Text));
				}
			}
		}

		private static IEnumerable<Block> Map(SongLineContext source)
		{
			foreach (BlockContext block in source.block())
			{
				if (block.chord() != null)
					yield return new Chord(block.chord().TEXT().Symbol.Text);
				if (block.TEXT() != null)
					yield return new Word(block.TEXT().Symbol.Text);
				if (block.word() != null)
					yield return new Word(Map(block.word()));
			}
		}

		private static IEnumerable<Syllable> Map(WordContext source)
		{
			foreach (SyllableContext syllable in source.syllable())
			{
				string chord = syllable.chord()?.TEXT().Symbol.Text;
				string text = syllable.TEXT()?.Symbol.Text;

				yield return new Syllable(new Chord(chord), text);
			}
		}
	}
}
