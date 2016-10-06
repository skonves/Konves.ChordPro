using Konves.ChordPro.DirectiveHandlers;
using Konves.ChordPro.Directives;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Konves.ChordPro.UnitTests.DirectiveHandlers
{
	[TestClass]
	public class ChordFontHandlerTestFixture
	{
		[TestMethod]
		public void TryParseTest_LongForm()
		{
			// Arrange
			int fontSize = 9;
			string input = $"{{chordsize: {fontSize}}}";
			DirectiveComponents components = DirectiveComponents.Parse(input);
			DirectiveHandler sut = ChordSizeHandler.Instance;
			Directive directive;

			// Act
			bool result = sut.TryParse(components, out directive);

			// Assert
			Assert.IsTrue(result);
			Assert.IsInstanceOfType(directive, typeof(ChordSizeDirective));
			Assert.AreEqual(fontSize, (directive as ChordSizeDirective).FontSize);
		}

		[TestMethod]
		public void TryParseTest_ShortForm()
		{
			// Arrange
			int fontSize = 9;
			string input = $"{{cs: {fontSize}}}";
			DirectiveComponents components = DirectiveComponents.Parse(input);
			DirectiveHandler sut = ChordSizeHandler.Instance;
			Directive directive;

			// Act
			bool result = sut.TryParse(components, out directive);

			// Assert
			Assert.IsTrue(result);
			Assert.IsInstanceOfType(directive, typeof(ChordSizeDirective));
			Assert.AreEqual(fontSize, (directive as ChordSizeDirective).FontSize);
		}

		[TestMethod]
		public void GetStringTest_LongForm()
		{
			// Arrange
			int fontSize = 9;
			Directive directive = new ChordSizeDirective(fontSize);
			string expectedText = $"{{chordsize: {fontSize}}}";
			DirectiveHandler sut = ChordSizeHandler.Instance;

			// Act
			string text = sut.GetString(directive, shorten: false);

			// Assert
			Assert.AreEqual(expectedText, text);
		}

		[TestMethod]
		public void GetStringTest_ShortForm()
		{
			// Arrange
			int fontSize = 9;
			Directive directive = new ChordSizeDirective(fontSize);
			string expectedText = $"{{cs: {fontSize}}}";
			DirectiveHandler sut = ChordSizeHandler.Instance;

			// Act
			string text = sut.GetString(directive, shorten: true);

			// Assert
			Assert.AreEqual(expectedText, text);
		}
	}
}

