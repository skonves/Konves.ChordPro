using Konves.ChordPro.DirectiveHandlers;
using Konves.ChordPro.Directives;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Konves.ChordPro.UnitTests.DirectiveHandlers
{
	[TestClass]
	public class GridHandlerTestFixture : KeyOnlyBaseTestFixture<GridDirective>
	{
		public GridHandlerTestFixture() : base("{grid}", "{g}", GridHandler.Instance) { }

		[TestMethod]
		[TestCategory("DirectiveHandler")]
		public override void TryParseTest_LongForm() { base.TryParseTest_LongForm(); }

		[TestMethod]
		[TestCategory("DirectiveHandler")]
		public override void TryParseTest_ShortForm() { base.TryParseTest_ShortForm(); }

		[TestMethod]
		[TestCategory("DirectiveHandler")]
		public override void GetStringTest_LongForm() { base.GetStringTest_LongForm(); }

		[TestMethod]
		[TestCategory("DirectiveHandler")]
		public override void GetStringTest_ShortForm() { base.GetStringTest_ShortForm(); }
	}
}
