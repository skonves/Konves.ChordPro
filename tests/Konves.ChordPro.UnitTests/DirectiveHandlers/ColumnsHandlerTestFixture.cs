using Konves.ChordPro.DirectiveHandlers;
using Konves.ChordPro.Directives;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Konves.ChordPro.UnitTests.DirectiveHandlers
{
	[TestClass]
	public class ColumnsHandlerTestFixture
	{
		[TestMethod]
		public void TryParseTest_LongForm()
		{
			// Arrange
			int number = 3;
			string input = $"{{columns: {number}}}";
			DirectiveComponents components = DirectiveComponents.Parse(input);
			DirectiveHandler sut = ColumnsHandler.Instance;
			Directive directive;

			// Act
			bool result = sut.TryParse(components, out directive);

			// Assert
			Assert.IsTrue(result);
			Assert.IsInstanceOfType(directive, typeof(ColumnsDirective));
			Assert.AreEqual(number, (directive as ColumnsDirective).Number);
		}

		[TestMethod]
		public void TryParseTest_ShortForm()
		{
			// Arrange
			int number = 3;
			string input = $"{{col: {number}}}";
			DirectiveComponents components = DirectiveComponents.Parse(input);
			DirectiveHandler sut = ColumnsHandler.Instance;
			Directive directive;

			// Act
			bool result = sut.TryParse(components, out directive);

			// Assert
			Assert.IsTrue(result);
			Assert.IsInstanceOfType(directive, typeof(ColumnsDirective));
			Assert.AreEqual(number, (directive as ColumnsDirective).Number);
		}

		[TestMethod]
		public void TryParseTest_NaN()
		{
			// Arrange
			string input = $"{{columns: NaN}}";
			DirectiveComponents components = DirectiveComponents.Parse(input);
			DirectiveHandler sut = ColumnsHandler.Instance;
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
			int number = 3;
			Directive directive = new ColumnsDirective(number);
			string expectedText = $"{{columns: {number}}}";
			DirectiveHandler sut = ColumnsHandler.Instance;

			// Act
			string text = sut.GetString(directive, shorten: false);

			// Assert
			Assert.AreEqual(expectedText, text);
		}

		[TestMethod]
		public void GetStringTest_ShortForm()
		{
			// Arrange
			int number = 3;
			Directive directive = new ColumnsDirective(number);
			string expectedText = $"{{col: {number}}}";
			DirectiveHandler sut = ColumnsHandler.Instance;

			// Act
			string text = sut.GetString(directive, shorten: true);

			// Assert
			Assert.AreEqual(expectedText, text);
		}
	}
}
