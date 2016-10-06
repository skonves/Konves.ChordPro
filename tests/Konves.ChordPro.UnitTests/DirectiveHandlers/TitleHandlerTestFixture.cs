using Konves.ChordPro.DirectiveHandlers;
using Konves.ChordPro.Directives;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Konves.ChordPro.UnitTests.DirectiveHandlers
{
	[TestClass]
	public class TitleHandlerTestFixture
	{
		[TestMethod]
		public void TryParseTest_LongForm()
		{
			// Arrange
			string title = "some title";
			string input = $"{{title: {title}}}";
			DirectiveComponents components = DirectiveComponents.Parse(input);
			DirectiveHandler sut = TitleHandler.Instance;
			Directive directive;

			// Act
			bool result = sut.TryParse(components, out directive);

			// Assert
			Assert.IsTrue(result);
			Assert.IsInstanceOfType(directive, typeof(TitleDirective));
			Assert.AreEqual(title, (directive as TitleDirective).Text);
		}

		[TestMethod]
		public void TryParseTest_ShortForm()
		{
			// Arrange
			string title = "some title";
			string input = $"{{t: {title}}}";
			DirectiveComponents components = DirectiveComponents.Parse(input);
			DirectiveHandler sut = TitleHandler.Instance;
			Directive directive;

			// Act
			bool result = sut.TryParse(components, out directive);

			// Assert
			Assert.IsTrue(result);
			Assert.IsInstanceOfType(directive, typeof(TitleDirective));
			Assert.AreEqual(title, (directive as TitleDirective).Text);
		}

		[TestMethod]
		public void GetStringTest_LongForm()
		{
			// Arrange
			string title = "some title";
			Directive directive = new TitleDirective(title);
			string expectedText = $"{{title: {title}}}";
			DirectiveHandler sut = TitleHandler.Instance;

			// Act
			string text = sut.GetString(directive, shorten: false);

			// Assert
			Assert.AreEqual(expectedText, text);
		}

		[TestMethod]
		public void GetStringTest_ShortForm()
		{
			// Arrange
			string title = "some title";
			Directive directive = new TitleDirective(title);
			string expectedText = $"{{t: {title}}}";
			DirectiveHandler sut = TitleHandler.Instance;

			// Act
			string text = sut.GetString(directive, shorten: true);

			// Assert
			Assert.AreEqual(expectedText, text);
		}
	}
}
