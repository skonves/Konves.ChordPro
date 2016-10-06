using Konves.ChordPro.DirectiveHandlers;
using Konves.ChordPro.Directives;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Konves.ChordPro.UnitTests.DirectiveHandlers
{
	[TestClass]
	public class EndOfTabHandlerTestFixture : KeyOnlyBaseTestFixture<EndOfTabDirective>
	{
		public EndOfTabHandlerTestFixture() : base("{end_of_tab}", "{eot}", EndOfTabHandler.Instance) { }
	}
}