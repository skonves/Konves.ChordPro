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
