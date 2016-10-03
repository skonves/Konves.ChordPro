using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Konves.ChordPro.UnitTests
{
	[TestClass]
	public class ParserTestFixture
	{
		[TestMethod]
		public void GetLineTypeTest()
		{
			DoGetLineTypeTest("{directive}", xParser.LineType.Directive);
			DoGetLineTypeTest("    {directive}", xParser.LineType.Directive);
			DoGetLineTypeTest("# Comment", xParser.LineType.Comment);
			DoGetLineTypeTest("    # Comment", xParser.LineType.Comment);
			DoGetLineTypeTest("song line", xParser.LineType.Text);
			DoGetLineTypeTest("    song line", xParser.LineType.Text);
			DoGetLineTypeTest("", xParser.LineType.Whitespace);
			DoGetLineTypeTest("    ", xParser.LineType.Whitespace);
		}

		private void DoGetLineTypeTest(string line, xParser.LineType expected)
		{
			// Arrange

			// Act
			xParser.LineType result = xParser.GetLineType(line);

			// Assert
			Assert.AreEqual(result, expected);
		}

		[TestMethod]
		public void TestGetDirectiveParts()
		{
			DoTestGetDirectiveParts("{asdf:qwerty}", "asdf", string.Empty, "qwerty");
			DoTestGetDirectiveParts("  {  asdf  :  qwerty  }  ", "asdf", string.Empty, "qwerty");
			DoTestGetDirectiveParts("{asdf abc:qwerty}", "asdf", "abc", "qwerty");
			DoTestGetDirectiveParts("  {  asdf   abc  :  qwerty  asdf  }  ", "asdf", "abc", "qwerty  asdf");
			DoTestGetDirectiveParts("{asdf:qwerty}#Comment", "asdf", string.Empty, "qwerty");
			DoTestGetDirectiveParts("  {  asdf  :  qwerty  }  # Comment", "asdf", string.Empty, "qwerty");
			DoTestGetDirectiveParts("{asdf abc:qwerty}# Comment", "asdf", "abc", "qwerty");
			DoTestGetDirectiveParts("  {  asdf   abc  :  qwerty  asdf  }  # Comment", "asdf", "abc", "qwerty  asdf");
		}

		private void DoTestGetDirectiveParts(string input, string expectedKey, string expectedSubKey, string expectedValue)
		{
			// Arrange

			// Act
			xParser.DirectiveParts result = xParser.GetDirectiveParts(input);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(expectedKey, result.Key);
			Assert.AreEqual(expectedSubKey, result.SubKey);
			Assert.AreEqual(expectedValue, result.Value);
		}

		[TestMethod]
		public void SplitIntoBlocksTest()
		{
			DoSplitIntoBlocksTest("asdf [X]asdf asdf", "asdf", "[X]asdf", "asdf");
			DoSplitIntoBlocksTest("asdf asdf[X] asdf", "asdf", "asdf[X]", "asdf");
			DoSplitIntoBlocksTest("asdf [x]asdf[x] asdf", "asdf", "[x]asdf[x]", "asdf");
			DoSplitIntoBlocksTest("asdf asdf[x]asdf asdf", "asdf", "asdf[x]asdf", "asdf");
			DoSplitIntoBlocksTest("asdf [x][x]asdf[x][x] asdf", "asdf", "[x][x]asdf[x][x]", "asdf");
			DoSplitIntoBlocksTest("asdf asdf[x][x]asdf asdf", "asdf", "asdf[x][x]asdf", "asdf");

			DoSplitIntoBlocksTest(" [X] ", "[X]");
			DoSplitIntoBlocksTest(" asdf ", "asdf");
			DoSplitIntoBlocksTest(" [X]asdf ", "[X]asdf");
			DoSplitIntoBlocksTest(" asdf[X] ", "asdf[X]");
			DoSplitIntoBlocksTest(" [x]asdf[x] ", "[x]asdf[x]");
			DoSplitIntoBlocksTest(" asdf[x]asdf ", "asdf[x]asdf");

			DoSplitIntoBlocksTest("asdf [ X ]asdf asdf", "asdf", "[ X ]asdf", "asdf");
			DoSplitIntoBlocksTest("asdf asdf[ X ] asdf", "asdf", "asdf[ X ]", "asdf");
			DoSplitIntoBlocksTest("asdf [ x ]asdf[ x ] asdf", "asdf", "[ x ]asdf[ x ]", "asdf");
			DoSplitIntoBlocksTest("asdf asdf[ x ]asdf asdf", "asdf", "asdf[ x ]asdf", "asdf");
			DoSplitIntoBlocksTest("asdf [ x ][ x ]asdf[ x ][ x ] asdf", "asdf", "[ x ][ x ]asdf[ x ][ x ]", "asdf");
			DoSplitIntoBlocksTest("asdf asdf[ x ][ x ]asdf asdf", "asdf", "asdf[ x ][ x ]asdf", "asdf");
		}

		private void DoSplitIntoBlocksTest(string line, params string[] expectedBlocks)
		{
			// Arrange

			// Act
			List<string> result = xParser.SplitIntoBlocks(line).ToList();

			// Assert
			CollectionAssert.AreEqual(expectedBlocks, result);
		}

		[TestMethod]
		public void SplitIntoSyllablesTest()
		{
			DoSplitIntoSyllablesTest("[x]", "[x]");
			DoSplitIntoSyllablesTest("asdf", "asdf");
			DoSplitIntoSyllablesTest("[x]asdf", "[x]asdf");
			DoSplitIntoSyllablesTest("asdf[x]", "asdf", "[x]");
			DoSplitIntoSyllablesTest("[x]asdf[x]", "[x]asdf", "[x]");
			DoSplitIntoSyllablesTest("asdf[x]asdf", "asdf", "[x]asdf");
			DoSplitIntoSyllablesTest("asdf[x][x]asdf", "asdf", "[x]", "[x]asdf");
			DoSplitIntoSyllablesTest("[x][x]asdf", "[x]", "[x]asdf");
		}

		private void DoSplitIntoSyllablesTest(string block, params string[] expectedSyllables)
		{
			// Arrange

			// Act
			List<string> result = xParser.SplitIntoSyllables(block).ToList();

			// Assert
			CollectionAssert.AreEqual(expectedSyllables, result);
		}

		[TestMethod]
		public void TryParseChordTest()
		{
			DoTryParseChordTest("[X]", true, new Chord("X"));
			DoTryParseChordTest("[ X ]", true, new Chord("X"));
			DoTryParseChordTest("X", false);
			DoTryParseChordTest("[]", false);
			DoTryParseChordTest("[X", false);
			DoTryParseChordTest("X]", false);
		}

		private void DoTryParseChordTest(string s, bool expectedResult, Chord expectedChord = null)
		{
			// Arrange

			// Act
			Chord chord;
			bool result = xParser.TryParseChord(s, out chord);

			// Assert
			Assert.AreEqual(expectedResult, result);
			if (ReferenceEquals(expectedChord, null))
				Assert.IsNull(chord);
			Assert.AreEqual(expectedChord?.Text, chord?.Text);
		}

		[TestMethod]
		public void ParseSyllableTest()
		{
			DoParseSyllableTest("[X]", new Syllable(new Chord("X"), null));
			DoParseSyllableTest("[X]asdf", new Syllable(new Chord("X"), "asdf"));
			DoParseSyllableTest("asdf", new Syllable(null, "asdf"));
		}

		private void DoParseSyllableTest(string s, Syllable expectedSyllable)
		{
			// Arrange
			xParser sut = new xParser(null);

			// Act
			Syllable result = sut.ParseSyllable(s);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(expectedSyllable?.Chord?.Text, result?.Chord?.Text);
			Assert.AreEqual(expectedSyllable?.Text, result?.Text);
		}

		[TestMethod]
		public void ParseBlockTest()
		{
			DoParseBlockTest("[x]", new Chord("x"));

			DoParseBlockTest("asdf", new Word(new[] {
				new Syllable(null, "asdf")
			}));

			DoParseBlockTest("[x]asdf", new Word(new[] {
				new Syllable(new Chord("x"), "asdf")
			}));

			DoParseBlockTest("asdf[x]", new Word(new[] {
				new Syllable(null, "asdf"),
				new Syllable(new Chord("x"), null)
			}));

			DoParseBlockTest("[x]asdf[x]", new Word(new[] {
				new Syllable(new Chord("x"), "asdf"),
				new Syllable(new Chord("x"), null)
			}));

			DoParseBlockTest("asdf[x]asdf", new Word(new[] {
				new Syllable(null, "asdf"),
				new Syllable(new Chord("x"), "asdf")
			}));

			DoParseBlockTest("asdf[x][x]asdf", new Word(new[] {
				new Syllable(null, "asdf"),
				new Syllable(new Chord("x"), null),
				new Syllable(new Chord("x"), "asdf")
			}));
		}

		private void DoParseBlockTest(string s, Block expected)
		{
			// Arrange
			xParser sut = new xParser(null);

			// Act
			Block result = sut.ParseBlock(s);

			// Assert
			Assert.IsNotNull(result);
			if (expected is Chord)
			{
				Assert.IsInstanceOfType(result, typeof(Chord));

				Chord resultChord = result as Chord;
				Chord expectedChord = expected as Chord;

				Assert.AreEqual(expectedChord.Text, resultChord.Text);
			}
			else if (expected is Word)
			{
				Assert.IsInstanceOfType(result, typeof(Word));

				Word resultWord = result as Word;
				Word expectedWord = expected as Word;

				Assert.AreEqual(expectedWord.Syllables.Count, resultWord.Syllables.Count);
			}
		}

		[TestMethod]
		public void TestParseDirective_ChordColor()
		{
			// Arrange
			string color = "red";
			string input = $"{{chordcolour:{color}}}";
			xParser sut = new xParser(null);

			// Act
			Directive result = sut.ParseDirective(input);

			// Assert
			Assert.IsInstanceOfType(result, typeof(ChordColourDirective));
			Assert.AreEqual(color, (result as ChordColourDirective).Colour);
		}

		[TestMethod]
		[ExpectedException(typeof(FormatException))]
		public void TestParseDirective_ChordColor_Null()
		{
			// Arrange
			string input = "{chordcolour}";
			xParser sut = new xParser(null);

			// Act
			Directive result = sut.ParseDirective(input);

			// Assert
			Assert.Fail();
		}

		[TestMethod]
		public void TestParseDirective_ChordFont()
		{
			// Arrange
			string font = "times";
			string input = $"{{chordfont:{font}}}";
			xParser sut = new xParser(null);

			// Act
			Directive result = sut.ParseDirective(input);

			// Assert
			Assert.IsInstanceOfType(result, typeof(ChordFontDirective));
			Assert.AreEqual(font, (result as ChordFontDirective).FontFamily);
		}

		[TestMethod]
		[ExpectedException(typeof(FormatException))]
		public void TestParseDirective_ChordFont_Null()
		{
			// Arrange
			string input = "{chordfont}";
			xParser sut = new xParser(null);

			// Act
			Directive result = sut.ParseDirective(input);

			// Assert
			Assert.Fail();
		}

		[TestMethod]
		public void TestParseDirective_CF()
		{
			// Arrange
			string font = "times";
			string input = $"{{cf:{font}}}";
			xParser sut = new xParser(null);

			// Act
			Directive result = sut.ParseDirective(input);

			// Assert
			Assert.IsInstanceOfType(result, typeof(ChordFontDirective));
			Assert.AreEqual(font, (result as ChordFontDirective).FontFamily);
		}

		[TestMethod]
		[ExpectedException(typeof(FormatException))]
		public void TestParseDirective_CF_Null()
		{
			// Arrange
			string input = "{cf}";
			xParser sut = new xParser(null);

			// Act
			Directive result = sut.ParseDirective(input);

			// Assert
			Assert.Fail();
		}

		[TestMethod]
		public void TestParseDirective_ChordSize()
		{
			// Arrange
			int size = 9;
			string input = $"{{chordsize:{size}}}";
			xParser sut = new xParser(null);

			// Act
			Directive result = sut.ParseDirective(input);

			// Assert
			Assert.IsInstanceOfType(result, typeof(ChordSizeDirective));
			Assert.AreEqual(size, (result as ChordSizeDirective).FontSize);
		}

		[TestMethod]
		[ExpectedException(typeof(FormatException))]
		public void TestParseDirective_ChordSize_NaN()
		{
			// Arrange
			string input = "{chordsize:NaN}";
			xParser sut = new xParser(null);

			// Act
			Directive result = sut.ParseDirective(input);

			// Assert
			Assert.Fail();
		}

		[TestMethod]
		[ExpectedException(typeof(FormatException))]
		public void TestParseDirective_ChordSize_Null()
		{
			// Arrange
			string input = "{chordsize}";
			xParser sut = new xParser(null);

			// Act
			Directive result = sut.ParseDirective(input);

			// Assert
			Assert.Fail();
		}

		[TestMethod]
		public void TestParseDirective_CS()
		{
			// Arrange
			int size = 9;
			string input = $"{{cs:{size}}}";
			xParser sut = new xParser(null);

			// Act
			Directive result = sut.ParseDirective(input);

			// Assert
			Assert.IsInstanceOfType(result, typeof(ChordSizeDirective));
			Assert.AreEqual(size, (result as ChordSizeDirective).FontSize);
		}

		[TestMethod]
		[ExpectedException(typeof(FormatException))]
		public void TestParseDirective_CS_NaN()
		{
			// Arrange
			string input = "{cs:NaN}";
			xParser sut = new xParser(null);

			// Act
			Directive result = sut.ParseDirective(input);

			// Assert
			Assert.Fail();
		}

		[TestMethod]
		[ExpectedException(typeof(FormatException))]
		public void TestParseDirective_CS_Null()
		{
			// Arrange
			string input = "{cs}";
			xParser sut = new xParser(null);

			// Act
			Directive result = sut.ParseDirective(input);

			// Assert
			Assert.Fail();
		}

		[TestMethod]
		public void TestParseDirective_Columns()
		{
			// Arrange
			int number = 9;
			string input = $"{{columns:{number}}}";
			xParser sut = new xParser(null);

			// Act
			Directive result = sut.ParseDirective(input);

			// Assert
			Assert.IsInstanceOfType(result, typeof(ColumnsDirective));
			Assert.AreEqual(number, (result as ColumnsDirective).Number);
		}

		[TestMethod]
		[ExpectedException(typeof(FormatException))]
		public void TestParseDirective_Columns_NaN()
		{
			// Arrange
			string input = "{columns:NaN}";
			xParser sut = new xParser(null);

			// Act
			Directive result = sut.ParseDirective(input);

			// Assert
			Assert.Fail();
		}

		[TestMethod]
		[ExpectedException(typeof(FormatException))]
		public void TestParseDirective_Columns_Null()
		{
			// Arrange
			string input = "{columns}";
			xParser sut = new xParser(null);

			// Act
			Directive result = sut.ParseDirective(input);

			// Assert
			Assert.Fail();
		}

		[TestMethod]
		public void TestParseDirective_Col()
		{
			// Arrange
			int number = 9;
			string input = $"{{col:{number}}}";
			xParser sut = new xParser(null);

			// Act
			Directive result = sut.ParseDirective(input);

			// Assert
			Assert.IsInstanceOfType(result, typeof(ColumnsDirective));
			Assert.AreEqual(number, (result as ColumnsDirective).Number);
		}

		[TestMethod]
		[ExpectedException(typeof(FormatException))]
		public void TestParseDirective_Col_NaN()
		{
			// Arrange
			string input = "{col:NaN}";
			xParser sut = new xParser(null);

			// Act
			Directive result = sut.ParseDirective(input);

			// Assert
			Assert.Fail();
		}

		[TestMethod]
		[ExpectedException(typeof(FormatException))]
		public void TestParseDirective_Col_Null()
		{
			// Arrange
			string input = "{col}";
			xParser sut = new xParser(null);

			// Act
			Directive result = sut.ParseDirective(input);

			// Assert
			Assert.Fail();
		}

		[TestMethod]
		public void TestParseDirective_ColumnBreak()
		{
			// Arrange
			string input = "{column_break}";
			xParser sut = new xParser(null);

			// Act
			Directive result = sut.ParseDirective(input);

			// Assert
			Assert.IsInstanceOfType(result, typeof(ColumnBreakDirective));
		}

		[TestMethod]
		[ExpectedException(typeof(FormatException))]
		public void TestParseDirective_ColumnBreak_NotNull()
		{
			// Arrange
			string input = "{column_break:value}";
			xParser sut = new xParser(null);

			// Act
			Directive result = sut.ParseDirective(input);

			// Assert
			Assert.Fail();
		}

		[TestMethod]
		public void TestParseDirective_ColB()
		{
			// Arrange
			string input = "{colb}";
			xParser sut = new xParser(null);

			// Act
			Directive result = sut.ParseDirective(input);

			// Assert
			Assert.IsInstanceOfType(result, typeof(ColumnBreakDirective));
		}

		[TestMethod]
		[ExpectedException(typeof(FormatException))]
		public void TestParseDirective_ColB_NotNull()
		{
			// Arrange
			string input = "{colb:value}";
			xParser sut = new xParser(null);

			// Act
			Directive result = sut.ParseDirective(input);

			// Assert
			Assert.Fail();
		}

		[TestMethod]
		public void TestParseDirective_Comment()
		{
			// Arrange
			string comment = "some comment text";
			string input = $"{{comment:{comment}}}";
			xParser sut = new xParser(null);

			// Act
			Directive result = sut.ParseDirective(input);

			// Assert
			Assert.IsInstanceOfType(result, typeof(CommentDirective));
			Assert.AreEqual(comment, (result as CommentDirective).Text);
		}

		[TestMethod]
		[ExpectedException(typeof(FormatException))]
		public void TestParseDirective_Comment_Null()
		{
			// Arrange
			string input = "{comment}";
			xParser sut = new xParser(null);

			// Act
			Directive result = sut.ParseDirective(input);

			// Assert
			Assert.Fail();
		}

		[TestMethod]
		public void TestParseDirective_C()
		{
			// Arrange
			string comment = "some comment text";
			string input = $"{{c:{comment}}}";
			xParser sut = new xParser(null);

			// Act
			Directive result = sut.ParseDirective(input);

			// Assert
			Assert.IsInstanceOfType(result, typeof(CommentDirective));
			Assert.AreEqual(comment, (result as CommentDirective).Text);
		}

		[TestMethod]
		[ExpectedException(typeof(FormatException))]
		public void TestParseDirective_C_Null()
		{
			// Arrange
			string input = "{c}";
			xParser sut = new xParser(null);

			// Act
			Directive result = sut.ParseDirective(input);

			// Assert
			Assert.Fail();
		}

		[TestMethod]
		public void TestParseDirective_CommentItalic()
		{
			// Arrange
			string comment = "some comment text";
			string input = $"{{comment_italic:{comment}}}";
			xParser sut = new xParser(null);

			// Act
			Directive result = sut.ParseDirective(input);

			// Assert
			Assert.IsInstanceOfType(result, typeof(CommentItalicDirective));
			Assert.AreEqual(comment, (result as CommentItalicDirective).Text);
		}

		[TestMethod]
		[ExpectedException(typeof(FormatException))]
		public void TestParseDirective_CommentItalic_Null()
		{
			// Arrange
			string input = "{comment_italic}";
			xParser sut = new xParser(null);

			// Act
			Directive result = sut.ParseDirective(input);

			// Assert
			Assert.Fail();
		}

		[TestMethod]
		public void TestParseDirective_CI()
		{
			// Arrange
			string comment = "some comment text";
			string input = $"{{ci:{comment}}}";
			xParser sut = new xParser(null);

			// Act
			Directive result = sut.ParseDirective(input);

			// Assert
			Assert.IsInstanceOfType(result, typeof(CommentItalicDirective));
			Assert.AreEqual(comment, (result as CommentItalicDirective).Text);
		}

		[TestMethod]
		[ExpectedException(typeof(FormatException))]
		public void TestParseDirective_CI_Null()
		{
			// Arrange
			string input = "{ci}";
			xParser sut = new xParser(null);

			// Act
			Directive result = sut.ParseDirective(input);

			// Assert
			Assert.Fail();
		}

		[TestMethod]
		public void TestParseDirective_CommentBox()
		{
			// Arrange
			string comment = "some comment text";
			string input = $"{{comment_box:{comment}}}";
			xParser sut = new xParser(null);

			// Act
			Directive result = sut.ParseDirective(input);

			// Assert
			Assert.IsInstanceOfType(result, typeof(CommentBoxDirective));
			Assert.AreEqual(comment, (result as CommentBoxDirective).Text);
		}

		[TestMethod]
		[ExpectedException(typeof(FormatException))]
		public void TestParseDirective_CommentBox_Null()
		{
			// Arrange
			string input = "{comment_box}";
			xParser sut = new xParser(null);

			// Act
			Directive result = sut.ParseDirective(input);

			// Assert
			Assert.Fail();
		}

		[TestMethod]
		public void TestParseDirective_CB()
		{
			// Arrange
			string comment = "some comment text";
			string input = $"{{cb:{comment}}}";
			xParser sut = new xParser(null);

			// Act
			Directive result = sut.ParseDirective(input);

			// Assert
			Assert.IsInstanceOfType(result, typeof(CommentBoxDirective));
			Assert.AreEqual(comment, (result as CommentBoxDirective).Text);
		}

		[TestMethod]
		[ExpectedException(typeof(FormatException))]
		public void TestParseDirective_CB_Null()
		{
			// Arrange
			string input = "{cb}";
			xParser sut = new xParser(null);

			// Act
			Directive result = sut.ParseDirective(input);

			// Assert
			Assert.Fail();
		}

		[TestMethod]
		public void TestParseDirective_Define()
		{
			// Arrange
			string chord = "X";
			string definition = "some chord definition";
			string input = $"{{define {chord}:{definition}}}";
			xParser sut = new xParser(null);

			// Act
			Directive result = sut.ParseDirective(input);

			// Assert
			Assert.IsInstanceOfType(result, typeof(DefineDirective));
			Assert.AreEqual(chord, (result as DefineDirective).Chord);
			Assert.AreEqual(definition, (result as DefineDirective).Definition);
		}

		[TestMethod]
		[ExpectedException(typeof(FormatException))]
		public void TestParseDirective_Define_NullChord()
		{
			// Arrange
			string input = "{define:definition}";
			xParser sut = new xParser(null);

			// Act
			Directive result = sut.ParseDirective(input);

			// Assert
			Assert.Fail();
		}

		[TestMethod]
		[ExpectedException(typeof(FormatException))]
		public void TestParseDirective_Define_NullDefinition()
		{
			// Arrange
			string input = "{define X}";
			xParser sut = new xParser(null);

			// Act
			Directive result = sut.ParseDirective(input);

			// Assert
			Assert.Fail();
		}

		[TestMethod]
		public void TestParseDirective_EndOfChorus()
		{
			// Arrange
			string input = "{end_of_chorus}";
			xParser sut = new xParser(null);

			// Act
			Directive result = sut.ParseDirective(input);

			// Assert
			Assert.IsInstanceOfType(result, typeof(EndOfChorusDirective));
		}

		[TestMethod]
		[ExpectedException(typeof(FormatException))]
		public void TestParseDirective_EndOfChorus_NotNull()
		{
			// Arrange
			string input = "{end_of_chorus:value}";
			xParser sut = new xParser(null);

			// Act
			Directive result = sut.ParseDirective(input);

			// Assert
			Assert.Fail();
		}

		[TestMethod]
		public void TestParseDirective_EOC()
		{
			// Arrange
			string input = "{eoc}";
			xParser sut = new xParser(null);

			// Act
			Directive result = sut.ParseDirective(input);

			// Assert
			Assert.IsInstanceOfType(result, typeof(EndOfChorusDirective));
		}

		[TestMethod]
		[ExpectedException(typeof(FormatException))]
		public void TestParseDirective_EOC_NotNull()
		{
			// Arrange
			string input = "{eoc:value}";
			xParser sut = new xParser(null);

			// Act
			Directive result = sut.ParseDirective(input);

			// Assert
			Assert.Fail();
		}

		[TestMethod]
		public void TestParseDirective_EndOfTab()
		{
			// Arrange
			string input = "{end_of_tab}";
			xParser sut = new xParser(null);

			// Act
			Directive result = sut.ParseDirective(input);

			// Assert
			Assert.IsInstanceOfType(result, typeof(EndOfTabDirective));
			Assert.IsFalse(sut._isInTab);
		}

		[TestMethod]
		[ExpectedException(typeof(FormatException))]
		public void TestParseDirective_EndOfTab_NotNull()
		{
			// Arrange
			string input = "{end_of_tab:value}";
			xParser sut = new xParser(null);

			// Act
			Directive result = sut.ParseDirective(input);

			// Assert
			Assert.Fail();
		}

		[TestMethod]
		public void TestParseDirective_EOT()
		{
			// Arrange
			string input = "{eot}";
			xParser sut = new xParser(null);

			// Act
			Directive result = sut.ParseDirective(input);

			// Assert
			Assert.IsInstanceOfType(result, typeof(EndOfTabDirective));
			Assert.IsFalse(sut._isInTab);
		}

		[TestMethod]
		[ExpectedException(typeof(FormatException))]
		public void TestParseDirective_EOT_NotNull()
		{
			// Arrange
			string input = "{eot:value}";
			xParser sut = new xParser(null);

			// Act
			Directive result = sut.ParseDirective(input);

			// Assert
			Assert.Fail();
		}

		[TestMethod]
		public void TestParseDirective_Grid()
		{
			// Arrange
			string input = "{grid}";
			xParser sut = new xParser(null);

			// Act
			Directive result = sut.ParseDirective(input);

			// Assert
			Assert.IsInstanceOfType(result, typeof(GridDirective));
		}

		[TestMethod]
		[ExpectedException(typeof(FormatException))]
		public void TestParseDirective_Grid_NotNull()
		{
			// Arrange
			string input = "{grid:value}";
			xParser sut = new xParser(null);

			// Act
			Directive result = sut.ParseDirective(input);

			// Assert
			Assert.Fail();
		}

		[TestMethod]
		public void TestParseDirective_G()
		{
			// Arrange
			string input = "{g}";
			xParser sut = new xParser(null);

			// Act
			Directive result = sut.ParseDirective(input);

			// Assert
			Assert.IsInstanceOfType(result, typeof(GridDirective));
		}

		[TestMethod]
		[ExpectedException(typeof(FormatException))]
		public void TestParseDirective_G_NotNull()
		{
			// Arrange
			string input = "{g:value}";
			xParser sut = new xParser(null);

			// Act
			Directive result = sut.ParseDirective(input);

			// Assert
			Assert.Fail();
		}

		[TestMethod]
		public void TestParseDirective_NewPage()
		{
			// Arrange
			string input = "{new_page}";
			xParser sut = new xParser(null);

			// Act
			Directive result = sut.ParseDirective(input);

			// Assert
			Assert.IsInstanceOfType(result, typeof(NewPageDirective));
		}

		[TestMethod]
		[ExpectedException(typeof(FormatException))]
		public void TestParseDirective_NewPage_NotNull()
		{
			// Arrange
			string input = "{new_page:value}";
			xParser sut = new xParser(null);

			// Act
			Directive result = sut.ParseDirective(input);

			// Assert
			Assert.Fail();
		}

		[TestMethod]
		public void TestParseDirective_NP()
		{
			// Arrange
			string input = "{np}";
			xParser sut = new xParser(null);

			// Act
			Directive result = sut.ParseDirective(input);

			// Assert
			Assert.IsInstanceOfType(result, typeof(NewPageDirective));
		}

		[TestMethod]
		[ExpectedException(typeof(FormatException))]
		public void TestParseDirective_NP_NotNull()
		{
			// Arrange
			string input = "{np:value}";
			xParser sut = new xParser(null);

			// Act
			Directive result = sut.ParseDirective(input);

			// Assert
			Assert.Fail();
		}

		[TestMethod]
		public void TestParseDirective_NewPhysicalPage()
		{
			// Arrange
			string input = "{new_physical_page}";
			xParser sut = new xParser(null);

			// Act
			Directive result = sut.ParseDirective(input);

			// Assert
			Assert.IsInstanceOfType(result, typeof(NewPhysicalPageDirective));
		}

		[TestMethod]
		[ExpectedException(typeof(FormatException))]
		public void TestParseDirective_NewPhysicalPage_NotNull()
		{
			// Arrange
			string input = "{new_physical_page:value}";
			xParser sut = new xParser(null);

			// Act
			Directive result = sut.ParseDirective(input);

			// Assert
			Assert.Fail();
		}

		[TestMethod]
		public void TestParseDirective_NPP()
		{
			// Arrange
			string input = "{npp}";
			xParser sut = new xParser(null);

			// Act
			Directive result = sut.ParseDirective(input);

			// Assert
			Assert.IsInstanceOfType(result, typeof(NewPhysicalPageDirective));
		}

		[TestMethod]
		[ExpectedException(typeof(FormatException))]
		public void TestParseDirective_NPP_NotNull()
		{
			// Arrange
			string input = "{npp:value}";
			xParser sut = new xParser(null);

			// Act
			Directive result = sut.ParseDirective(input);

			// Assert
			Assert.Fail();
		}

		[TestMethod]
		public void TestParseDirective_NewSong()
		{
			// Arrange
			string input = "{new_song}";
			xParser sut = new xParser(null);

			// Act
			Directive result = sut.ParseDirective(input);

			// Assert
			Assert.IsInstanceOfType(result, typeof(NewSongDirective));
		}

		[TestMethod]
		[ExpectedException(typeof(FormatException))]
		public void TestParseDirective_NewSong_NotNull()
		{
			// Arrange
			string input = "{new_song:value}";
			xParser sut = new xParser(null);

			// Act
			Directive result = sut.ParseDirective(input);

			// Assert
			Assert.Fail();
		}

		[TestMethod]
		public void TestParseDirective_NS()
		{
			// Arrange
			string input = "{ns}";
			xParser sut = new xParser(null);

			// Act
			Directive result = sut.ParseDirective(input);

			// Assert
			Assert.IsInstanceOfType(result, typeof(NewSongDirective));
		}

		[TestMethod]
		[ExpectedException(typeof(FormatException))]
		public void TestParseDirective_NS_NotNull()
		{
			// Arrange
			string input = "{ns:value}";
			xParser sut = new xParser(null);

			// Act
			Directive result = sut.ParseDirective(input);

			// Assert
			Assert.Fail();
		}

		[TestMethod]
		public void TestParseDirective_NoGrid()
		{
			// Arrange
			string input = "{no_grid}";
			xParser sut = new xParser(null);

			// Act
			Directive result = sut.ParseDirective(input);

			// Assert
			Assert.IsInstanceOfType(result, typeof(NoGridDirective));
		}

		[TestMethod]
		[ExpectedException(typeof(FormatException))]
		public void TestParseDirective_NoGrid_NotNull()
		{
			// Arrange
			string input = "{no_grid:value}";
			xParser sut = new xParser(null);

			// Act
			Directive result = sut.ParseDirective(input);

			// Assert
			Assert.Fail();
		}

		[TestMethod]
		public void TestParseDirective_NG()
		{
			// Arrange
			string input = "{ng}";
			xParser sut = new xParser(null);

			// Act
			Directive result = sut.ParseDirective(input);

			// Assert
			Assert.IsInstanceOfType(result, typeof(NoGridDirective));
		}

		[TestMethod]
		[ExpectedException(typeof(FormatException))]
		public void TestParseDirective_NG_NotNull()
		{
			// Arrange
			string input = "{ng:value}";
			xParser sut = new xParser(null);

			// Act
			Directive result = sut.ParseDirective(input);

			// Assert
			Assert.Fail();
		}

		[TestMethod]
		public void TestParseDirective_PageType_A4()
		{
			// Arrange
			string input = "{pagetype:a4}";
			xParser sut = new xParser(null);
			PageType expectedPageType = PageType.A4;

			// Act
			Directive result = sut.ParseDirective(input);

			// Assert
			Assert.IsInstanceOfType(result, typeof(PageTypeDirective));
			Assert.AreEqual(expectedPageType, (result as PageTypeDirective).PageType);
		}

		[TestMethod]
		public void TestParseDirective_PageType_Letter()
		{
			// Arrange
			string input = "{pagetype:letter}";
			xParser sut = new xParser(null);
			PageType expectedPageType = PageType.Letter;

			// Act
			Directive result = sut.ParseDirective(input);

			// Assert
			Assert.IsInstanceOfType(result, typeof(PageTypeDirective));
			Assert.AreEqual(expectedPageType, (result as PageTypeDirective).PageType);
		}

		[TestMethod]
		[ExpectedException(typeof(FormatException))]
		public void TestParseDirective_PageType_Other()
		{
			// Arrange
			string input = "{pagetype:invalid value}";
			xParser sut = new xParser(null);

			// Act
			Directive result = sut.ParseDirective(input);

			// Assert
			Assert.Fail();
		}

		[TestMethod]
		[ExpectedException(typeof(FormatException))]
		public void TestParseDirective_PageType_Null()
		{
			// Arrange
			string input = "{pagetype}";
			xParser sut = new xParser(null);

			// Act
			Directive result = sut.ParseDirective(input);

			// Assert
			Assert.Fail();
		}

		[TestMethod]
		public void TestParseDirective_StartOfChorus()
		{
			// Arrange
			string input = "{start_of_chorus}";
			xParser sut = new xParser(null);

			// Act
			Directive result = sut.ParseDirective(input);

			// Assert
			Assert.IsInstanceOfType(result, typeof(StartOfChorusDirective));
		}

		[TestMethod]
		[ExpectedException(typeof(FormatException))]
		public void TestParseDirective_StartOfChorus_NotNull()
		{
			// Arrange
			string input = "{start_of_chorus:value}";
			xParser sut = new xParser(null);

			// Act
			Directive result = sut.ParseDirective(input);

			// Assert
			Assert.Fail();
		}

		[TestMethod]
		public void TestParseDirective_SOC()
		{
			// Arrange
			string input = "{soc}";
			xParser sut = new xParser(null);

			// Act
			Directive result = sut.ParseDirective(input);

			// Assert
			Assert.IsInstanceOfType(result, typeof(StartOfChorusDirective));
		}

		[TestMethod]
		[ExpectedException(typeof(FormatException))]
		public void TestParseDirective_SOC_NotNull()
		{
			// Arrange
			string input = "{soc:value}";
			xParser sut = new xParser(null);

			// Act
			Directive result = sut.ParseDirective(input);

			// Assert
			Assert.Fail();
		}

		[TestMethod]
		public void TestParseDirective_StartOfTab()
		{
			// Arrange
			string input = "{start_of_tab}";
			xParser sut = new xParser(null);

			// Act
			Directive result = sut.ParseDirective(input);

			// Assert
			Assert.IsInstanceOfType(result, typeof(StartOfTabDirective));
			Assert.IsTrue(sut._isInTab);
		}

		[TestMethod]
		[ExpectedException(typeof(FormatException))]
		public void TestParseDirective_StartOfTab_NotNull()
		{
			// Arrange
			string input = "{start_of_tab:value}";
			xParser sut = new xParser(null);

			// Act
			Directive result = sut.ParseDirective(input);

			// Assert
			Assert.Fail();
		}

		[TestMethod]
		public void TestParseDirective_SOT()
		{
			// Arrange
			string input = "{sot}";
			xParser sut = new xParser(null);

			// Act
			Directive result = sut.ParseDirective(input);

			// Assert
			Assert.IsInstanceOfType(result, typeof(StartOfTabDirective));
			Assert.IsTrue(sut._isInTab);
		}

		[TestMethod]
		[ExpectedException(typeof(FormatException))]
		public void TestParseDirective_SOT_NotNull()
		{
			// Arrange
			string input = "{sot:value}";
			xParser sut = new xParser(null);

			// Act
			Directive result = sut.ParseDirective(input);

			// Assert
			Assert.Fail();
		}

		[TestMethod]
		public void TestParseDirective_Subtitle()
		{
			// Arrange
			string subtitle = "some subtitle text";
			string input = $"{{subtitle:{subtitle}}}";
			xParser sut = new xParser(null);

			// Act
			Directive result = sut.ParseDirective(input);

			// Assert
			Assert.IsInstanceOfType(result, typeof(SubtitleDirective));
			Assert.AreEqual(subtitle, (result as SubtitleDirective).Text);
		}

		[TestMethod]
		[ExpectedException(typeof(FormatException))]
		public void TestParseDirective_Subtitle_Null()
		{
			// Arrange
			string input = "{subtitle}";
			xParser sut = new xParser(null);

			// Act
			Directive result = sut.ParseDirective(input);

			// Assert
			Assert.Fail();
		}

		[TestMethod]
		public void TestParseDirective_ST()
		{
			// Arrange
			string subtitle = "some subtitle text";
			string input = $"{{st:{subtitle}}}";
			xParser sut = new xParser(null);

			// Act
			Directive result = sut.ParseDirective(input);

			// Assert
			Assert.IsInstanceOfType(result, typeof(SubtitleDirective));
			Assert.AreEqual(subtitle, (result as SubtitleDirective).Text);
		}

		[TestMethod]
		[ExpectedException(typeof(FormatException))]
		public void TestParseDirective_ST_Null()
		{
			// Arrange
			string input = "{st}";
			xParser sut = new xParser(null);

			// Act
			Directive result = sut.ParseDirective(input);

			// Assert
			Assert.Fail();
		}

		[TestMethod]
		public void TestParseDirective_TextFont()
		{
			// Arrange
			string font = "some font name";
			string input = $"{{textfont:{font}}}";
			xParser sut = new xParser(null);

			// Act
			Directive result = sut.ParseDirective(input);

			// Assert
			Assert.IsInstanceOfType(result, typeof(TextFontDirective));
			Assert.AreEqual(font, (result as TextFontDirective).FontFamily);
		}

		[TestMethod]
		[ExpectedException(typeof(FormatException))]
		public void TestParseDirective_TextFont_Null()
		{
			// Arrange
			string input = "{textfont}";
			xParser sut = new xParser(null);

			// Act
			Directive result = sut.ParseDirective(input);

			// Assert
			Assert.Fail();
		}

		[TestMethod]
		public void TestParseDirective_TF()
		{
			// Arrange
			string font = "some font name";
			string input = $"{{tf:{font}}}";
			xParser sut = new xParser(null);

			// Act
			Directive result = sut.ParseDirective(input);

			// Assert
			Assert.IsInstanceOfType(result, typeof(TextFontDirective));
			Assert.AreEqual(font, (result as TextFontDirective).FontFamily);
		}

		[TestMethod]
		[ExpectedException(typeof(FormatException))]
		public void TestParseDirective_TF_Null()
		{
			// Arrange
			string input = "{tf}";
			xParser sut = new xParser(null);

			// Act
			Directive result = sut.ParseDirective(input);

			// Assert
			Assert.Fail();
		}

		[TestMethod]
		public void TestParseDirective_TextSize()
		{
			// Arrange
			int size = 9;
			string input = $"{{textsize:{size}}}";
			xParser sut = new xParser(null);

			// Act
			Directive result = sut.ParseDirective(input);

			// Assert
			Assert.IsInstanceOfType(result, typeof(TextSizeDirective));
			Assert.AreEqual(size, (result as TextSizeDirective).FontSize);
		}

		[TestMethod]
		[ExpectedException(typeof(FormatException))]
		public void TestParseDirective_TextSize_NaN()
		{
			// Arrange
			string input = "{textsize:NaN}";
			xParser sut = new xParser(null);

			// Act
			Directive result = sut.ParseDirective(input);

			// Assert
			Assert.Fail();
		}

		[TestMethod]
		[ExpectedException(typeof(FormatException))]
		public void TestParseDirective_TextSize_Null()
		{
			// Arrange
			string input = "{textsize}";
			xParser sut = new xParser(null);

			// Act
			Directive result = sut.ParseDirective(input);

			// Assert
			Assert.Fail();
		}

		[TestMethod]
		public void TestParseDirective_TS()
		{
			// Arrange
			int size = 9;
			string input = $"{{ts:{size}}}";
			xParser sut = new xParser(null);

			// Act
			Directive result = sut.ParseDirective(input);

			// Assert
			Assert.IsInstanceOfType(result, typeof(TextSizeDirective));
			Assert.AreEqual(size, (result as TextSizeDirective).FontSize);
		}

		[TestMethod]
		[ExpectedException(typeof(FormatException))]
		public void TestParseDirective_TS_NaN()
		{
			// Arrange
			string input = "{ts:NaN}";
			xParser sut = new xParser(null);

			// Act
			Directive result = sut.ParseDirective(input);

			// Assert
			Assert.Fail();
		}

		[TestMethod]
		[ExpectedException(typeof(FormatException))]
		public void TestParseDirective_TS_Null()
		{
			// Arrange
			string input = "{ts}";
			xParser sut = new xParser(null);

			// Act
			Directive result = sut.ParseDirective(input);

			// Assert
			Assert.Fail();
		}

		[TestMethod]
		public void TestParseDirective_Title()
		{
			// Arrange
			string title = "some title text";
			string input = $"{{title:{title}}}";
			xParser sut = new xParser(null);

			// Act
			Directive result = sut.ParseDirective(input);

			// Assert
			Assert.IsInstanceOfType(result, typeof(TitleDirective));
			Assert.AreEqual(title, (result as TitleDirective).Text);
		}

		[TestMethod]
		[ExpectedException(typeof(FormatException))]
		public void TestParseDirective_Title_Null()
		{
			// Arrange
			string input = "{title}";
			xParser sut = new xParser(null);

			// Act
			Directive result = sut.ParseDirective(input);

			// Assert
			Assert.Fail();
		}

		[TestMethod]
		public void TestParseDirective_T()
		{
			// Arrange
			string title = "some title text";
			string input = $"{{t:{title}}}";
			xParser sut = new xParser(null);

			// Act
			Directive result = sut.ParseDirective(input);

			// Assert
			Assert.IsInstanceOfType(result, typeof(TitleDirective));
			Assert.AreEqual(title, (result as TitleDirective).Text);
		}

		[TestMethod]
		[ExpectedException(typeof(FormatException))]
		public void TestParseDirective_T_Null()
		{
			// Arrange
			string input = "{t}";
			xParser sut = new xParser(null);

			// Act
			Directive result = sut.ParseDirective(input);

			// Assert
			Assert.Fail();
		}

		[TestMethod]
		public void TestParseDirective_Titles_Left()
		{
			// Arrange
			string input = "{titles:left}";
			xParser sut = new xParser(null);
			Alignment expectedAlignmentType = Alignment.Left;

			// Act
			Directive result = sut.ParseDirective(input);

			// Assert
			Assert.IsInstanceOfType(result, typeof(TitlesDirective));
			Assert.AreEqual(expectedAlignmentType, (result as TitlesDirective).Flush);
		}

		[TestMethod]
		public void TestParseDirective_Titles_Center()
		{
			// Arrange
			string input = "{titles:center}";
			xParser sut = new xParser(null);
			Alignment expectedAlignmentType = Alignment.Center;

			// Act
			Directive result = sut.ParseDirective(input);

			// Assert
			Assert.IsInstanceOfType(result, typeof(TitlesDirective));
			Assert.AreEqual(expectedAlignmentType, (result as TitlesDirective).Flush);
		}

		[TestMethod]
		[ExpectedException(typeof(FormatException))]
		public void TestParseDirective_Titles_Other()
		{
			// Arrange
			string input = "{titles:invalid value}";
			xParser sut = new xParser(null);

			// Act
			Directive result = sut.ParseDirective(input);

			// Assert
			Assert.Fail();
		}

		[TestMethod]
		[ExpectedException(typeof(FormatException))]
		public void TestParseDirective_Titles_Null()
		{
			// Arrange
			string input = "{titles}";
			xParser sut = new xParser(null);

			// Act
			Directive result = sut.ParseDirective(input);

			// Assert
			Assert.Fail();
		}

		[TestMethod]
		[ExpectedException(typeof(FormatException))]
		public void TestParseDirective_Invalid()
		{
			// Arrange
			string input = "{invalid_directive}";
			xParser sut = new xParser(null);

			// Act
			Directive result = sut.ParseDirective(input);

			// Assert
			Assert.Fail();
		}
	}
}
