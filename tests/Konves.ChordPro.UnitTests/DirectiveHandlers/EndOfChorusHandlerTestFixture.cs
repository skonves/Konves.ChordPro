using Konves.ChordPro.DirectiveHandlers;
using Konves.ChordPro.Directives;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Konves.ChordPro.UnitTests.DirectiveHandlers
{
	[TestClass]
	public class EndOfChorusHandlerTestFixture : KeyOnlyBaseTestFixture<EndOfChorusDirective>
	{
		public EndOfChorusHandlerTestFixture() : base("{end_of_chorus}", "{eoc}", EndOfChorusHandler.Instance) { }
	}
}