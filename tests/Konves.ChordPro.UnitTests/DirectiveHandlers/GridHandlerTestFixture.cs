using Konves.ChordPro.DirectiveHandlers;
using Konves.ChordPro.Directives;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Konves.ChordPro.UnitTests.DirectiveHandlers
{
	[TestClass]
	public class GridHandlerTestFixture : KeyOnlyBaseTestFixture<GridDirective>
	{
		public GridHandlerTestFixture() : base("{grid}", "{g}", GridHandler.Instance) { }
	}
}
