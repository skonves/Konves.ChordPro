using Konves.ChordPro.Directives;

namespace Konves.ChordPro.DirectiveHandlers
{
	public sealed class ChordFontHandler : DirectiveHandler<ChordFontDirective>
	{
		protected override bool TryCreate(DirectiveComponents components, out Directive directive)
		{
			directive = new ChordFontDirective(components.Value);
			return true;
		}

		protected override string GetValue(Directive directive)
		{
			return (directive as ChordFontDirective)?.FontFamily;
		}

		public override string LongName { get { return "chordfont"; } }
		public override string ShortName { get { return "cf"; } }
		public override ComponentPresence SubKey { get { return ComponentPresence.NotAllowed; } }
		public override ComponentPresence Value { get { return ComponentPresence.Required; } }
	}
}
