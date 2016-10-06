using Konves.ChordPro.DirectiveHandlers;
using Konves.ChordPro.Directives;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Konves.ChordPro.UnitTests.DirectiveHandlers
{
	[TestClass]
	public class CommentBoxHandlerTestFixture
	{
		[TestMethod]
		public void TryParseTest_LongForm()
		{
			// Arrange
			string comment = "some comment";
			string input = $"{{comment_box: {comment}}}";
			DirectiveComponents components = DirectiveComponents.Parse(input);
			DirectiveHandler sut = CommentBoxHandler.Instance;
			Directive directive;

			// Act
			bool result = sut.TryParse(components, out directive);

			// Assert
			Assert.IsTrue(result);
			Assert.IsInstanceOfType(directive, typeof(CommentBoxDirective));
			Assert.AreEqual(comment, (directive as CommentBoxDirective).Text);
		}

		[TestMethod]
		public void TryParseTest_ShortForm()
		{
			// Arrange
			string comment = "some comment";
			string input = $"{{cb: {comment}}}";
			DirectiveComponents components = DirectiveComponents.Parse(input);
			DirectiveHandler sut = CommentBoxHandler.Instance;
			Directive directive;

			// Act
			bool result = sut.TryParse(components, out directive);

			// Assert
			Assert.IsTrue(result);
			Assert.IsInstanceOfType(directive, typeof(CommentBoxDirective));
			Assert.AreEqual(comment, (directive as CommentBoxDirective).Text);
		}

		[TestMethod]
		public void GetStringTest_LongForm()
		{
			// Arrange
			string comment = "some comment";
			Directive directive = new CommentBoxDirective(comment);
			string expectedText = $"{{comment_box: {comment}}}";
			DirectiveHandler sut = CommentBoxHandler.Instance;

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
			Directive directive = new CommentBoxDirective(comment);
			string expectedText = $"{{cb: {comment}}}";
			DirectiveHandler sut = CommentBoxHandler.Instance;

			// Act
			string text = sut.GetString(directive, shorten: true);

			// Assert
			Assert.AreEqual(expectedText, text);
		}
	}
}
