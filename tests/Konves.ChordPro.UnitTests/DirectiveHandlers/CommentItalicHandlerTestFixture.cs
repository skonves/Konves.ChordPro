using Konves.ChordPro.DirectiveHandlers;
using Konves.ChordPro.Directives;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Konves.ChordPro.UnitTests.DirectiveHandlers
{
	[TestClass]
	public class CommentItalicHandlerTestFixture
	{
		[TestMethod]
		[TestCategory("DirectiveHandler")]
		public void TryParseTest_LongForm()
		{
			// Arrange
			string comment = "some comment";
			string input = $"{{comment_italic: {comment}}}";
			DirectiveComponents components = DirectiveComponents.Parse(input);
			DirectiveHandler sut = CommentItalicHandler.Instance;
			Directive directive;

			// Act
			bool result = sut.TryParse(components, out directive);

			// Assert
			Assert.IsTrue(result);
			Assert.IsInstanceOfType(directive, typeof(CommentItalicDirective));
			Assert.AreEqual(comment, (directive as CommentItalicDirective).Text);
		}

		[TestMethod]
		[TestCategory("DirectiveHandler")]
		public void TryParseTest_ShortForm()
		{
			// Arrange
			string comment = "some comment";
			string input = $"{{ci: {comment}}}";
			DirectiveComponents components = DirectiveComponents.Parse(input);
			DirectiveHandler sut = CommentItalicHandler.Instance;
			Directive directive;

			// Act
			bool result = sut.TryParse(components, out directive);

			// Assert
			Assert.IsTrue(result);
			Assert.IsInstanceOfType(directive, typeof(CommentItalicDirective));
			Assert.AreEqual(comment, (directive as CommentItalicDirective).Text);
		}

		[TestMethod]
		[TestCategory("DirectiveHandler")]
		public void GetStringTest_LongForm()
		{
			// Arrange
			string comment = "some comment";
			Directive directive = new CommentItalicDirective(comment);
			string expectedText = $"{{comment_italic: {comment}}}";
			DirectiveHandler sut = CommentItalicHandler.Instance;

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
			string comment = "some comment";
			Directive directive = new CommentItalicDirective(comment);
			string expectedText = $"{{ci: {comment}}}";
			DirectiveHandler sut = CommentItalicHandler.Instance;

			// Act
			string text = sut.GetString(directive, shorten: true);

			// Assert
			Assert.AreEqual(expectedText, text);
		}
	}
}
