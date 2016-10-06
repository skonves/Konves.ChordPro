using Konves.ChordPro.Directives;

namespace Konves.ChordPro.DirectiveHandlers
{
	public sealed class EndOfChorusHandler : DirectiveHandler<EndOfChorusDirective>
	{
		private EndOfChorusHandler() { }

		public static EndOfChorusHandler Instance { get; } = new EndOfChorusHandler();

		protected override bool TryCreate(DirectiveComponents components, out Directive directive)
		{
			directive = new EndOfChorusDirective();
			return true;
		}

		public override string LongName { get { return "end_of_chorus"; } }
		public override string ShortName { get { return "eoc"; } }
		public override ComponentPresence SubKey { get { return ComponentPresence.NotAllowed; } }
		public override ComponentPresence Value { get { return ComponentPresence.NotAllowed; } }
	}
}
