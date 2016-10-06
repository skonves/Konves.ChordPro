using Konves.ChordPro.DirectiveHandlers;
using Konves.ChordPro.Directives;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Konves.ChordPro.UnitTests.DirectiveHandlers
{
	[TestClass]
	public class CommentHandlerTestFixture
	{
		[TestMethod]
		public void TryParseTest_LongForm()
		{
			// Arrange
			string comment = "some comment";
			string input = $"{{comment: {comment}}}";
			DirectiveComponents components = DirectiveComponents.Parse(input);
			DirectiveHandler sut = CommentHandler.Instance;
			Directive directive;

			// Act
			bool result = sut.TryParse(components, out directive);

			// Assert
			Assert.IsTrue(result);
			Assert.IsInstanceOfType(directive, typeof(CommentDirective));
			Assert.AreEqual(comment, (directive as CommentDirective).Text);
		}

		[TestMethod]
		public void TryParseTest_ShortForm()
		{
			// Arrange
			string comment = "some comment";
			string input = $"{{c: {comment}}}";
			DirectiveComponents components = DirectiveComponents.Parse(input);
			DirectiveHandler sut = CommentHandler.Instance;
			Directive directive;

			// Act
			bool result = sut.TryParse(components, out directive);

			// Assert
			Assert.IsTrue(result);
			Assert.IsInstanceOfType(directive, typeof(CommentDirective));
			Assert.AreEqual(comment, (directive as CommentDirective).Text);
		}

		[TestMethod]
		public void GetStringTest_LongForm()
		{
			// Arrange
			string comment = "some comment";
			Directive directive = new CommentDirective(comment);
			string expectedText = $"{{comment: {comment}}}";
			DirectiveHandler sut = CommentHandler.Instance;

			// Act
			string text = sut.GetString(directive, shorten: false);

			// Assert
			Assert.AreEqual(expectedText, text);
		}

		[TestMethod]
		public void GetStringTest_ShortForm()
		{
			// Arrange
			string comment = "some comment";
			Directive directive = new CommentDirective(comment);
			string expectedText = $"{{c: {comment}}}";
			DirectiveHandler sut = CommentHandler.Instance;

			// Act
			string text = sut.GetString(directive, shorten: true);

			// Assert
			Assert.AreEqual(expectedText, text);
		}
	}
}
