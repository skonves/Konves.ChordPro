using Konves.ChordPro.Directives;

namespace Konves.ChordPro.DirectiveHandlers
{
	public sealed class TitlesHandler : DirectiveHandler<TitlesDirective>
	{
		private TitlesHandler() { }

		public static TitlesHandler Instance { get; } = new TitlesHandler();

		protected override bool TryCreate(DirectiveComponents components, out Directive directive)
		{
			switch (components.Value.ToLower())
			{
				case "left":
					directive = new TitlesDirective(Alignment.Left);
					return true;
				case "center":
					directive = new TitlesDirective(Alignment.Center);
					return true;
				default:
					directive = null;
					return false;
			}
		}

		protected override string GetValue(Directive directive)
		{
			return (directive as TitlesDirective)?.Flush.ToString().ToLower();
		}

		public override string LongName { get { return "titles"; } }
		public override ComponentPresence SubKey { get { return ComponentPresence.NotAllowed; } }
		public override ComponentPresence Value { get { return ComponentPresence.Required; } }
	}
}
