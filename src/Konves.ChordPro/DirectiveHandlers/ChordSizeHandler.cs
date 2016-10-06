using Konves.ChordPro.Directives;

namespace Konves.ChordPro.DirectiveHandlers
{
	public sealed class ChordSizeHandler : DirectiveHandler<ChordSizeDirective>
	{
		private ChordSizeHandler() { }

		public static ChordSizeHandler Instance { get; } = new ChordSizeHandler();

		protected override bool TryCreate(DirectiveComponents components, out Directive directive)
		{
			int value;
			if (int.TryParse(components.Value, out value))
			{
				directive = new ChordSizeDirective(value);
				return true;
			}

			directive = null;
			return false;
		}

		protected override string GetValue(Directive directive)
		{
			return (directive as ChordSizeDirective)?.FontSize.ToString();
		}

		public override string LongName { get { return "chordsize"; } }
		public override string ShortName { get { return "cs"; } }
		public override ComponentPresence SubKey { get { return ComponentPresence.NotAllowed; } }
		public override ComponentPresence Value { get { return ComponentPresence.Required; } }
	}
}
