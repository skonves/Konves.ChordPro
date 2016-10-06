using Konves.ChordPro.Directives;

namespace Konves.ChordPro.DirectiveHandlers
{
	public sealed class NoGridHandler : DirectiveHandler<NoGridDirective>
	{
		private NoGridHandler() { }

		public static NoGridHandler Instance { get; } = new NoGridHandler();

		protected override bool TryCreate(DirectiveComponents components, out Directive directive)
		{
			directive = new NoGridDirective();
			return true;
		}

		public override string LongName { get { return "no_grid"; } }
		public override string ShortName { get { return "ng"; } }
		public override ComponentPresence SubKey { get { return ComponentPresence.NotAllowed; } }
		public override ComponentPresence Value { get { return ComponentPresence.NotAllowed; } }
	}
}
