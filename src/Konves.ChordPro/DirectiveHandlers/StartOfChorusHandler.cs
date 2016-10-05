using Konves.ChordPro.Directives;

namespace Konves.ChordPro.DirectiveHandlers
{
	public sealed class StartOfChorusHandler : DirectiveHandler<StartOfChorusDirective>
	{
		protected override bool TryCreate(DirectiveComponents components, out Directive directive)
		{
			directive = new StartOfChorusDirective();
			return true;
		}

		public override string LongName { get { return "start_of_chorus"; } }
		public override string ShortName { get { return "soc"; } }
		public override ComponentPresence SubKey { get { return ComponentPresence.NotAllowed; } }
		public override ComponentPresence Value { get { return ComponentPresence.NotAllowed; } }
	}
}
