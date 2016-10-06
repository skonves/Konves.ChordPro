using Konves.ChordPro.DirectiveHandlers;
using Konves.ChordPro.Directives;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Konves.ChordPro.UnitTests.DirectiveHandlers
{
	[TestClass]
	public class NewPhysicalPageHandlerTestFixture : KeyOnlyBaseTestFixture<NewPhysicalPageDirective>
	{
		public NewPhysicalPageHandlerTestFixture() : base("{new_physical_page}", "{npp}", NewPhysicalPageHandler.Instance) { }
	}
}