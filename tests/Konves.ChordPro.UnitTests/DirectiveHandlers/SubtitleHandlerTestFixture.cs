using Konves.ChordPro.DirectiveHandlers;
using Konves.ChordPro.Directives;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Konves.ChordPro.UnitTests.DirectiveHandlers
{
	[TestClass]
	public class SubtitleHandlerTestFixture
	{
		[TestMethod]
		[TestCategory("DirectiveHandler")]
		public void TryParseTest_LongForm()
		{
			// Arrange
			string subtitle = "some subtitle";
			string input = $"{{subtitle: {subtitle}}}";
			DirectiveComponents components = DirectiveComponents.Parse(input);
			DirectiveHandler sut = SubtitleHandler.Instance;
			Directive directive;

			// Act
			bool result = sut.TryParse(components, out directive);

			// Assert
			Assert.IsTrue(result);
			Assert.IsInstanceOfType(directive, typeof(SubtitleDirective));
			Assert.AreEqual(subtitle, (directive as SubtitleDirective).Text);
		}

		[TestMethod]
		[TestCategory("DirectiveHandler")]
		public void TryParseTest_ShortForm()
		{
			// Arrange
			string subtitle = "some subtitle";
			string input = $"{{st: {subtitle}}}";
			DirectiveComponents components = DirectiveComponents.Parse(input);
			DirectiveHandler sut = SubtitleHandler.Instance;
			Directive directive;

			// Act
			bool result = sut.TryParse(components, out directive);

			// Assert
			Assert.IsTrue(result);
			Assert.IsInstanceOfType(directive, typeof(SubtitleDirective));
			Assert.AreEqual(subtitle, (directive as SubtitleDirective).Text);
		}

		[TestMethod]
		[TestCategory("DirectiveHandler")]
		public void GetStringTest_LongForm()
		{
			// Arrange
			string subtitle = "some subtitle";
			Directive directive = new SubtitleDirective(subtitle);
			string expectedText = $"{{subtitle: {subtitle}}}";
			DirectiveHandler sut = SubtitleHandler.Instance;

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
			string subtitle = "some subtitle";
			Directive directive = new SubtitleDirective(subtitle);
			string expectedText = $"{{st: {subtitle}}}";
			DirectiveHandler sut = SubtitleHandler.Instance;

			// Act
			string text = sut.GetString(directive, shorten: true);

			// Assert
			Assert.AreEqual(expectedText, text);
		}
	}
}
