using Konves.ChordPro.DirectiveHandlers;
using Konves.ChordPro.Directives;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Konves.ChordPro.UnitTests.DirectiveHandlers
{
	[TestClass]
	public class ChordColourHandlerTestFixture
	{
		[TestMethod]
		[TestCategory("DirectiveHandler")]
		public void TryParseTest_LongForm()
		{
			// Arrange
			string color = "red";
			string input = $"{{chordcolour: {color}}}";
			DirectiveComponents components = DirectiveComponents.Parse(input);
			DirectiveHandler sut = ChordColourHandler.Instance;
			Directive directive;

			// Act
			bool result = sut.TryParse(components, out directive);

			// Assert
			Assert.IsTrue(result);
			Assert.IsInstanceOfType(directive, typeof(ChordColourDirective));
			Assert.AreEqual(color, (directive as ChordColourDirective).Colour);
		}

		[TestMethod]
		[TestCategory("DirectiveHandler")]
		public void GetStringTest_LongForm()
		{
			// Arrange
			string color = "red";
			Directive directive = new ChordColourDirective(color);
			string expectedText = $"{{chordcolour: {color}}}";
			DirectiveHandler sut = ChordColourHandler.Instance;

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
			string color = "red";
			Directive directive = new ChordColourDirective(color);
			string expectedText = $"{{chordcolour: {color}}}";
			DirectiveHandler sut = ChordColourHandler.Instance;

			// Act
			string text = sut.GetString(directive, shorten: true);

			// Assert
			Assert.AreEqual(expectedText, text);
		}
	}
}
