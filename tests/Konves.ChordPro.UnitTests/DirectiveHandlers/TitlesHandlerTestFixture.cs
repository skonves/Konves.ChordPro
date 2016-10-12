using Konves.ChordPro.DirectiveHandlers;
using Konves.ChordPro.Directives;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Konves.ChordPro.UnitTests.DirectiveHandlers
{
	[TestClass]
	public class TitlesHandlerTestFixture
	{
		[TestMethod]
		public void TryParseTest_LongForm_Left()
		{
			// Arrange
			Alignment alignment = Alignment.Left;
			string input = $"{{titles: left}}";
			DirectiveComponents components = DirectiveComponents.Parse(input);
			DirectiveHandler sut = TitlesHandler.Instance;
			Directive directive;

			// Act
			bool result = sut.TryParse(components, out directive);

			// Assert
			Assert.IsTrue(result);
			Assert.IsInstanceOfType(directive, typeof(TitlesDirective));
			Assert.AreEqual(alignment, (directive as TitlesDirective).Flush);
		}

		[TestMethod]
		public void TryParseTest_LongForm_Center()
		{
			// Arrange
			Alignment alignment = Alignment.Center;
			string input = $"{{titles: center}}";
			DirectiveComponents components = DirectiveComponents.Parse(input);
			DirectiveHandler sut = TitlesHandler.Instance;
			Directive directive;

			// Act
			bool result = sut.TryParse(components, out directive);

			// Assert
			Assert.IsTrue(result);
			Assert.IsInstanceOfType(directive, typeof(TitlesDirective));
			Assert.AreEqual(alignment, (directive as TitlesDirective).Flush);
		}

		[TestMethod]
		public void TryParseTest_LongForm_Other()
		{
			// Arrange
			string input = $"{{titles: not a valid alignment}}";
			DirectiveComponents components = DirectiveComponents.Parse(input);
			DirectiveHandler sut = TitlesHandler.Instance;
			Directive directive;

			// Act
			bool result = sut.TryParse(components, out directive);

			// Assert
			Assert.IsFalse(result);
		}

		[TestMethod]
		public void GetStringTest_LongForm()
		{
			// Arrange
			Directive directive = new TitlesDirective(Alignment.Left);
			string expectedText = $"{{titles: left}}";
			DirectiveHandler sut = TitlesHandler.Instance;

			// Act
			string text = sut.GetString(directive, shorten: false);

			// Assert
			Assert.AreEqual(expectedText, text);
		}

		[TestMethod]
		public void GetStringTest_ShortForm()
		{
			// Arrange
			Directive directive = new TitlesDirective(Alignment.Left);
			string expectedText = $"{{titles: left}}";
			DirectiveHandler sut = TitlesHandler.Instance;

			// Act
			string text = sut.GetString(directive, shorten: true);

			// Assert
			Assert.AreEqual(expectedText, text);
		}
	}
}
