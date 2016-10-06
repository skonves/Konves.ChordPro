using Konves.ChordPro.DirectiveHandlers;
using Konves.ChordPro.Directives;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Konves.ChordPro.UnitTests.DirectiveHandlers
{
	[TestClass]
	public class NoGridHandlerTestFixture : KeyOnlyBaseTestFixture<NoGridDirective>
	{
		public NoGridHandlerTestFixture() : base("{no_grid}", "{ng}", NoGridHandler.Instance) { }
	}
}