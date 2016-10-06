using Konves.ChordPro.Directives;
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
	}
}
