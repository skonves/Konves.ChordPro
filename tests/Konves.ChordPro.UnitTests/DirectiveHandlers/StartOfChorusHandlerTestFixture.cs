using Konves.ChordPro.DirectiveHandlers;
using Konves.ChordPro.Directives;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Konves.ChordPro.UnitTests.DirectiveHandlers
{
	[TestClass]
	public class StartOfChorusHandlerTestFixture : KeyOnlyBaseTestFixture<StartOfChorusDirective>
	{
		public StartOfChorusHandlerTestFixture() : base("{start_of_chorus}", "{soc}", StartOfChorusHandler.Instance) { }
	}
}