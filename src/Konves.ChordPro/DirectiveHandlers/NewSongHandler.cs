using Konves.ChordPro.Directives;

namespace Konves.ChordPro.DirectiveHandlers
{
	public sealed class NewSongHandler : DirectiveHandler<NewSongDirective>
	{
		private NewSongHandler() { }

		public static NewSongHandler Instance { get; } = new NewSongHandler();

		protected override bool TryCreate(DirectiveComponents components, out Directive directive)
		{
			directive = new NewSongDirective();
			return true;
		}

		public override string LongName { get { return "new_song"; } }
		public override string ShortName { get { return "ns"; } }
		public override ComponentPresence SubKey { get { return ComponentPresence.NotAllowed; } }
		public override ComponentPresence Value { get { return ComponentPresence.NotAllowed; } }
	}
}
