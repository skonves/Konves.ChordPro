using Konves.ChordPro.Directives;

namespace Konves.ChordPro.DirectiveHandlers
{
	public sealed class TextSizeHandler : DirectiveHandler<TextSizeDirective>
	{
		private TextSizeHandler() { }

		public static TextSizeHandler Instance { get; } = new TextSizeHandler();

		protected override bool TryCreate(DirectiveComponents components, out Directive directive)
		{
			int value;
			if (int.TryParse(components.Value, out value))
			{
				directive = new TextSizeDirective(value);
				return true;
			}

			directive = null;
			return false;
		}

		protected override string GetValue(Directive directive)
		{
			return (directive as TextSizeDirective)?.FontSize.ToString();
		}

		public override string LongName { get { return "textsize"; } }
		public override string ShortName { get { return "ts"; } }
		public override ComponentPresence SubKey { get { return ComponentPresence.NotAllowed; } }
		public override ComponentPresence Value { get { return ComponentPresence.Required; } }
	}
}
