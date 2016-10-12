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
			string fontFamily = "times";
			string input = $"{{chordfont: {fontFamily}}}";
			DirectiveComponents components = DirectiveComponents.Parse(input);
			DirectiveHandler sut = ChordFontHandler.Instance;
			Directive directive;

			// Act
			bool result = sut.TryParse(components, out directive);

			// Assert
			Assert.IsTrue(result);
			Assert.IsInstanceOfType(directive, typeof(ChordFontDirective));
			Assert.AreEqual(fontFamily, (directive as ChordFontDirective).FontFamily);
		}

		[TestMethod]
		public void TryParseTest_ShortForm()
		{
			// Arrange
			string fontFamily = "times";
			string input = $"{{cf: {fontFamily}}}";
			DirectiveComponents components = DirectiveComponents.Parse(input);
			DirectiveHandler sut = ChordFontHandler.Instance;
			Directive directive;

			// Act
			bool result = sut.TryParse(components, out directive);

			// Assert
			Assert.IsTrue(result);
			Assert.IsInstanceOfType(directive, typeof(ChordFontDirective));
			Assert.AreEqual(fontFamily, (directive as ChordFontDirective).FontFamily);
		}

		[TestMethod]
		public void GetStringTest_LongForm()
		{
			// Arrange
			string fontFamily = "times";
			Directive directive = new ChordFontDirective(fontFamily);
			string expectedText = $"{{chordfont: {fontFamily}}}";
			DirectiveHandler sut = ChordFontHandler.Instance;

			// Act
			string text = sut.GetString(directive, shorten: false);

			// Assert
			Assert.AreEqual(expectedText, text);
		}

		[TestMethod]
		public void GetStringTest_ShortForm()
		{
			// Arrange
			string fontFamily = "times";
			Directive directive = new ChordFontDirective(fontFamily);
			string expectedText = $"{{cf: {fontFamily}}}";
			DirectiveHandler sut = ChordFontHandler.Instance;

			// Act
			string text = sut.GetString(directive, shorten: true);

			// Assert
			Assert.AreEqual(expectedText, text);
		}
	}
}

