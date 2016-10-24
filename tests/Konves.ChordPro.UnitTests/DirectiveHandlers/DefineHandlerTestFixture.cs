using Konves.ChordPro.DirectiveHandlers;
using Konves.ChordPro.Directives;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Konves.ChordPro.UnitTests.DirectiveHandlers
{
	[TestClass]
	public class DefineHandlerTestFixture
	{
		[TestMethod]
		[TestCategory("DirectiveHandler")]
		public void TryParseTest_LongForm()
		{
			// Arrange
			string chord = "X";
			string definition = "some definition";
			string input = $"{{define {chord}: {definition}}}";
			DirectiveComponents components = DirectiveComponents.Parse(input);
			DirectiveHandler sut = DefineHandler.Instance;
			Directive directive;

			// Act
			bool result = sut.TryParse(components, out directive);

			// Assert
			Assert.IsTrue(result);
			Assert.IsInstanceOfType(directive, typeof(DefineDirective));
			Assert.AreEqual(chord, (directive as DefineDirective).Chord);
			Assert.AreEqual(definition, (directive as DefineDirective).Definition);
		}

		[TestMethod]
		[TestCategory("DirectiveHandler")]
		public void GetStringTest_LongForm()
		{
			// Arrange
			string chord = "X";
			string definition = "some definition";
			Directive directive = new DefineDirective(chord, definition);
			string expectedText = $"{{define {chord}: {definition}}}";
			DirectiveHandler sut = DefineHandler.Instance;

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
			string chord = "X";
			string definition = "some definition";
			Directive directive = new DefineDirective(chord, definition);
			string expectedText = $"{{define {chord}: {definition}}}";
			DirectiveHandler sut = DefineHandler.Instance;

			// Act
			string text = sut.GetString(directive, shorten: true);

			// Assert
			Assert.AreEqual(expectedText, text);
		}
	}
}
