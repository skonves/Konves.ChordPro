using Konves.ChordPro.DirectiveHandlers;
using Konves.ChordPro.Directives;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Konves.ChordPro.UnitTests.DirectiveHandlers
{
	[TestClass]
	public abstract class KeyOnlyBaseTestFixture<TDirective> where TDirective : Directive, new()
	{
		protected KeyOnlyBaseTestFixture(string longForm, string shortForm, DirectiveHandler sut)
		{
			_longForm = longForm;
			_shortForm = shortForm;
			_sut = sut;
		}
		
		public virtual void TryParseTest_LongForm()
		{
			// Arrange
			string input = _longForm;
			DirectiveComponents components = DirectiveComponents.Parse(input);
			Directive directive;

			// Act
			bool result = _sut.TryParse(components, out directive);

			// Assert
			Assert.IsTrue(result);
			Assert.IsInstanceOfType(directive, typeof(TDirective));
		}

		public virtual void TryParseTest_ShortForm()
		{
			// Arrange
			string input = _shortForm;
			DirectiveComponents components = DirectiveComponents.Parse(input);
			Directive directive;

			// Act
			bool result = _sut.TryParse(components, out directive);

			// Assert
			Assert.IsTrue(result);
			Assert.IsInstanceOfType(directive, typeof(TDirective));
		}

		public virtual void GetStringTest_LongForm()
		{
			// Arrange
			Directive directive = new TDirective();
			string expectedText = _longForm;

			// Act
			string text = _sut.GetString(directive, shorten: false);

			// Assert
			Assert.AreEqual(expectedText, text);
		}

		public virtual void GetStringTest_ShortForm()
		{
			// Arrange
			Directive directive = new TDirective();
			string expectedText = _shortForm;

			// Act
			string text = _sut.GetString(directive, shorten: true);

			// Assert
			Assert.AreEqual(expectedText, text);
		}

		readonly string _longForm;
		readonly string _shortForm;
		readonly DirectiveHandler _sut;
	}
}