using Konves.ChordPro.DirectiveHandlers;
using Konves.ChordPro.Directives;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Konves.ChordPro.UnitTests.DirectiveHandlers
{
	[TestClass]
	public class TextSizeHandlerTestFixture
	{
		[TestMethod]
		public void TryParseTest_LongForm()
		{
			// Arrange
			int fontSize = 9;
			string input = $"{{textsize: {fontSize}}}";
			DirectiveComponents components = DirectiveComponents.Parse(input);
			DirectiveHandler sut = TextSizeHandler.Instance;
			Directive directive;

			// Act
			bool result = sut.TryParse(components, out directive);

			// Assert
			Assert.IsTrue(result);
			Assert.IsInstanceOfType(directive, typeof(TextSizeDirective));
			Assert.AreEqual(fontSize, (directive as TextSizeDirective).FontSize);
		}

		[TestMethod]
		public void TryParseTest_ShortForm()
		{
			// Arrange
			int fontSize = 9;
			string input = $"{{ts: {fontSize}}}";
			DirectiveComponents components = DirectiveComponents.Parse(input);
			DirectiveHandler sut = TextSizeHandler.Instance;
			Directive directive;

			// Act
			bool result = sut.TryParse(components, out directive);

			// Assert
			Assert.IsTrue(result);
			Assert.IsInstanceOfType(directive, typeof(TextSizeDirective));
			Assert.AreEqual(fontSize, (directive as TextSizeDirective).FontSize);
		}

		[TestMethod]
		public void TryParseTest_NaN()
		{
			// Arrange
			string input = $"{{textsize: NaN}}";
			DirectiveComponents components = DirectiveComponents.Parse(input);
			DirectiveHandler sut = TextSizeHandler.Instance;
			Directive directive;

			// Act
			bool result = sut.TryParse(components, out directive);

			// Assert
			Assert.IsFalse(result);
			Assert.IsNull(directive);
		}

		[TestMethod]
		public void GetStringTest_LongForm()
		{
			// Arrange
			int fontSize = 9;
			Directive directive = new TextSizeDirective(fontSize);
			string expectedText = $"{{textsize: {fontSize}}}";
			DirectiveHandler sut = TextSizeHandler.Instance;

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
			Directive directive = new TextSizeDirective(fontSize);
			string expectedText = $"{{ts: {fontSize}}}";
			DirectiveHandler sut = TextSizeHandler.Instance;

			// Act
			string text = sut.GetString(directive, shorten: true);

			// Assert
			Assert.AreEqual(expectedText, text);
		}
	}
}
