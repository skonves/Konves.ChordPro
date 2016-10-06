using System;
using Konves.ChordPro.DirectiveHandlers;
using Konves.ChordPro.Directives;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Konves.ChordPro.UnitTests.DirectiveHandlers
{
	[TestClass]
	public class ColumnBreakHandlerTestFixture : KeyOnlyBaseTestFixture<ColumnBreakDirective>
	{
		public ColumnBreakHandlerTestFixture() : base("{column_break}", "{colb}", ColumnBreakHandler.Instance) { }
	}
}