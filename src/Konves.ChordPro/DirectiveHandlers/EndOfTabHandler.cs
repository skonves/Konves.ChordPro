using Konves.ChordPro.Directives;

namespace Konves.ChordPro.DirectiveHandlers
{
	public sealed class EndOfTabHandler : DirectiveHandler<EndOfTabDirective>
	{
		private EndOfTabHandler() { }

		public static EndOfTabHandler Instance { get; } = new EndOfTabHandler();

		protected override bool TryCreate(DirectiveComponents components, out Directive directive)
		{
			directive = new EndOfTabDirective();
			return true;
		}

		public override string LongName { get { return "end_of_tab"; } }
		public override string ShortName { get { return "eot"; } }
		public override ComponentPresence SubKey { get { return ComponentPresence.NotAllowed; } }
		public override ComponentPresence Value { get { return ComponentPresence.NotAllowed; } }
	}
}
