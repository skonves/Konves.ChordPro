using Konves.ChordPro.DirectiveHandlers;
using Konves.ChordPro.Directives;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Konves.ChordPro.UnitTests.DirectiveHandlers
{
	[TestClass]
	public class PageTypeHandlerTestFixture
	{
		[TestMethod]
		[TestCategory("DirectiveHandler")]
		public void TryParseTest_LongForm_Letter()
		{
			// Arrange
			PageType pageType = PageType.Letter;
			string input = $"{{pagetype: letter}}";
			DirectiveComponents components = DirectiveComponents.Parse(input);
			DirectiveHandler sut = PageTypeHandler.Instance;
			Directive directive;

			// Act
			bool result = sut.TryParse(components, out directive);

			// Assert
			Assert.IsTrue(result);
			Assert.IsInstanceOfType(directive, typeof(PageTypeDirective));
			Assert.AreEqual(pageType, (directive as PageTypeDirective).PageType);
		}

		[TestMethod]
		[TestCategory("DirectiveHandler")]
		public void TryParseTest_LongForm_A4()
		{
			// Arrange
			PageType pageType = PageType.A4;
			string input = $"{{pagetype: a4}}";
			DirectiveComponents components = DirectiveComponents.Parse(input);
			DirectiveHandler sut = PageTypeHandler.Instance;
			Directive directive;

			// Act
			bool result = sut.TryParse(components, out directive);

			// Assert
			Assert.IsTrue(result);
			Assert.IsInstanceOfType(directive, typeof(PageTypeDirective));
			Assert.AreEqual(pageType, (directive as PageTypeDirective).PageType);
		}

		[TestMethod]
		[TestCategory("DirectiveHandler")]
		public void TryParseTest_LongForm_Other()
		{
			// Arrange
			string input = $"{{pagetype: not a valid page type}}";
			DirectiveComponents components = DirectiveComponents.Parse(input);
			DirectiveHandler sut = PageTypeHandler.Instance;
			Directive directive;

			// Act
			bool result = sut.TryParse(components, out directive);

			// Assert
			Assert.IsFalse(result);
		}

		[TestMethod]
		[TestCategory("DirectiveHandler")]
		public void GetStringTest_LongForm()
		{
			// Arrange
			Directive directive = new PageTypeDirective(PageType.Letter);
			string expectedText = $"{{pagetype: letter}}";
			DirectiveHandler sut = PageTypeHandler.Instance;

			// Act
			string text = sut.GetString(directive, shorten: false);

			// Assert
			Assert.AreEqual(expectedText, text);
		}

		[TestMethod]
		[TestCategory("DirectiveHandler")]
		public void GetStringTest_ShortForm()
		{
			// Arrange
			Directive directive = new PageTypeDirective(PageType.Letter);
			string expectedText = $"{{pagetype: letter}}";
			DirectiveHandler sut = PageTypeHandler.Instance;

			// Act
			string text = sut.GetString(directive, shorten: true);

			// Assert
			Assert.AreEqual(expectedText, text);
		}
	}
}
