using Konves.ChordPro.DirectiveHandlers;
using Konves.ChordPro.Directives;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Konves.ChordPro.UnitTests.DirectiveHandlers
{
	[TestClass]
	public class NewSongHandlerTestFixture : KeyOnlyBaseTestFixture<NewSongDirective>
	{
		public NewSongHandlerTestFixture() : base("{new_song}", "{ns}", NewSongHandler.Instance) { }
	}
}