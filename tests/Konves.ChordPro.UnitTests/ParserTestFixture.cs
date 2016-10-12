using Konves.ChordPro.Directives;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Konves.ChordPro.UnitTests
{
	[TestClass]
	public class ParserTestFixture
	{
		[TestMethod]
		public void ParseSongLineTest()
		{
			// Arrange
			Parser sut = new Parser(null);
			string line = "asdf [X]asdf asdf";
			int expectedBlockCount = 3;

			// Act
			SongLine result = sut.ParseSongLine(line);

			// Assert
			Assert.IsNotNull(result);
			Assert.IsNotNull(result.Blocks);
			Assert.AreEqual(expectedBlockCount, result.Blocks.Count);
		}

		[TestMethod]
		public void GetLineTypeTest()
		{
			DoGetLineTypeTest("{directive}", Parser.LineType.Directive);
			DoGetLineTypeTest("    {directive}", Parser.LineType.Directive);
			DoGetLineTypeTest("# Comment", Parser.LineType.Comment);
			DoGetLineTypeTest("    # Comment", Parser.LineType.Comment);
			DoGetLineTypeTest("song line", Parser.LineType.Text);
			DoGetLineTypeTest("    song line", Parser.LineType.Text);
			DoGetLineTypeTest("", Parser.LineType.Whitespace);
			DoGetLineTypeTest("    ", Parser.LineType.Whitespace);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void GetLineTypeTest_Null()
		{
			// Arrange
			string s = null;

			// Act
			Parser.LineType result = Parser.GetLineType(s);
		}

		private void DoGetLineTypeTest(string line, Parser.LineType expected)
		{
			// Arrange

			// Act
			Parser.LineType result = Parser.GetLineType(line);

			// Assert
			Assert.AreEqual(result, expected);
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

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void SplitIntoBlocksTest_Null()
		{
			// Arrange
			string line = null;

			// Act
			List<string> result = Parser.SplitIntoBlocks(line).ToList();
		}

		private void DoSplitIntoBlocksTest(string line, params string[] expectedBlocks)
		{
			// Arrange

			// Act
			List<string> result = Parser.SplitIntoBlocks(line).ToList();

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
			List<string> result = Parser.SplitIntoSyllables(block).ToList();

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
			bool result = Parser.TryParseChord(s, out chord);

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

		[TestMethod]
		[ExpectedException(typeof(FormatException))]
		public void ParseSyllableTest_Exception()
		{
			// Arrange
			Parser sut = new Parser(null);
			string syllable = "[]";

			// Act
			Syllable result = sut.ParseSyllable(syllable);
		}

		private void DoParseSyllableTest(string s, Syllable expectedSyllable)
		{
			// Arrange
			Parser sut = new Parser(null);

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
			Parser sut = new Parser(null);

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
		[ExpectedException(typeof(FormatException))]
		public void ParseDirectiveTest_NotADirective()
		{
			// Arrange
			string line = "{some invalid format";
			Parser sut = new Parser(null);

			// Act
			Directive result = sut.ParseDirective(line);
		}

		[TestMethod]
		[ExpectedException(typeof(FormatException))]
		public void ParseDirectiveTest_UnknownDirective()
		{
			// Arrange
			string line = "{unknowndirective}";
			Parser sut = new Parser(null);

			// Act
			Directive result = sut.ParseDirective(line);
		}

		[TestMethod]
		[ExpectedException(typeof(FormatException))]
		public void ParseDirectiveTest_InvalidFormat()
		{
			// Arrange
			string line = "{title}"; // Missing value
			Parser sut = new Parser(null);

			// Act
			Directive result = sut.ParseDirective(line);
		}

		[TestMethod]
		public void ParseDirectiveTest_StartOfTab()
		{
			// Arrange
			string line = "{start_of_tab}";
			bool expectedIsInTab = true;
			Parser sut = new Parser(null);

			// Act
			Directive result = sut.ParseDirective(line);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(expectedIsInTab, sut._isInTab);
		}

		[TestMethod]
		public void ParseDirectiveTest_EndOfTab()
		{
			// Arrange
			string line = "{end_of_tab}";
			bool expectedIsInTab = false;
			Parser sut = new Parser(null);

			// Act
			Directive result = sut.ParseDirective(line);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(expectedIsInTab, sut._isInTab);
		}

		[TestMethod]
		public void ParseTest()
		{
			// Arrange
			string song = @"# Comment
{sot}
   tab line
{eot}

Song Line preceded by whitespace";
			int expectedLineCount = 5;
			TextReader reader = new StringReader(song);
			Parser sut = new Parser(reader);

			// Act
			List<ILine> result = sut.Parse().ToList();

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(expectedLineCount, result.Count);
			Assert.IsInstanceOfType(result[0], typeof(StartOfTabDirective));
			Assert.IsInstanceOfType(result[1], typeof(TabLine));
			Assert.IsInstanceOfType(result[2], typeof(EndOfTabDirective));
			Assert.IsInstanceOfType(result[3], typeof(SongLine));
			Assert.IsInstanceOfType(result[4], typeof(SongLine));

		}
	}
}
