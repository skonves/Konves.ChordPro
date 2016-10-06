using Konves.ChordPro.DirectiveHandlers;
using Konves.ChordPro.Directives;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Konves.ChordPro.UnitTests.DirectiveHandlers
{
	[TestClass]
	public class NewPageHandlerTestFixture : KeyOnlyBaseTestFixture<NewPageDirective>
	{
		public NewPageHandlerTestFixture() : base("{new_page}", "{np}", NewPageHandler.Instance) { }
	}
}
