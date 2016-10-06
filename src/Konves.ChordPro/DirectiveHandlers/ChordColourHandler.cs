using Konves.ChordPro.Directives;

namespace Konves.ChordPro.DirectiveHandlers
{
	public sealed class ChordColourHandler : DirectiveHandler<ChordColourDirective>
	{
		private ChordColourHandler() { }

		public static ChordColourHandler Instance { get; } = new ChordColourHandler();

		protected override bool TryCreate(DirectiveComponents components, out Directive directive)
		{
			directive = new ChordColourDirective(components.Value);
			return true;
		}

		protected override string GetValue(Directive directive)
		{
			return (directive as ChordColourDirective)?.Colour;
		}

		public override string LongName { get { return "chordcolour"; } }
		public override ComponentPresence SubKey { get { return ComponentPresence.NotAllowed; } }
		public override ComponentPresence Value { get { return ComponentPresence.Required; } }
	}
}
