using Konves.ChordPro.DirectiveHandlers;
using Konves.ChordPro.Directives;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Konves.ChordPro.UnitTests.DirectiveHandlers
{
	[TestClass]
	public class StartOfTabHandlerTestFixture : KeyOnlyBaseTestFixture<StartOfTabDirective>
	{
		public StartOfTabHandlerTestFixture() : base("{start_of_tab}", "{sot}", StartOfTabHandler.Instance) { }
	}
}