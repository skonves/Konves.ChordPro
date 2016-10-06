using Konves.ChordPro.DirectiveHandlers;
using Konves.ChordPro.Directives;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Konves.ChordPro.UnitTests.DirectiveHandlers
{
	[TestClass]
	public class TextFontHandlerTestFixture
	{
		[TestMethod]
		public void TryParseTest_LongForm()
		{
			// Arrange
			string fontFamily = "times";
			string input = $"{{textfont: {fontFamily}}}";
			DirectiveComponents components = DirectiveComponents.Parse(input);
			DirectiveHandler sut = TextFontHandler.Instance;
			Directive directive;

			// Act
			bool result = sut.TryParse(components, out directive);

			// Assert
			Assert.IsTrue(result);
			Assert.IsInstanceOfType(directive, typeof(TextFontDirective));
			Assert.AreEqual(fontFamily, (directive as TextFontDirective).FontFamily);
		}

		[TestMethod]
		public void TryParseTest_ShortForm()
		{
			// Arrange
			string fontFamily = "times";
			string input = $"{{tf: {fontFamily}}}";
			DirectiveComponents components = DirectiveComponents.Parse(input);
			DirectiveHandler sut = TextFontHandler.Instance;
			Directive directive;

			// Act
			bool result = sut.TryParse(components, out directive);

			// Assert
			Assert.IsTrue(result);
			Assert.IsInstanceOfType(directive, typeof(TextFontDirective));
			Assert.AreEqual(fontFamily, (directive as TextFontDirective).FontFamily);
		}

		[TestMethod]
		public void GetStringTest_LongForm()
		{
			// Arrange
			string fontFamily = "times";
			Directive directive = new TextFontDirective(fontFamily);
			string expectedText = $"{{textfont: {fontFamily}}}";
			DirectiveHandler sut = TextFontHandler.Instance;

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
			Directive directive = new TextFontDirective(fontFamily);
			string expectedText = $"{{tf: {fontFamily}}}";
			DirectiveHandler sut = TextFontHandler.Instance;

			// Act
			string text = sut.GetString(directive, shorten: true);

			// Assert
			Assert.AreEqual(expectedText, text);
		}
	}
}
