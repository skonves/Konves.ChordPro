using Konves.ChordPro.Directives;

namespace Konves.ChordPro.DirectiveHandlers
{
	public sealed class TextFontHandler : DirectiveHandler<TextFontDirective>
	{
		private TextFontHandler() { }

		public static TextFontHandler Instance { get; } = new TextFontHandler();

		protected override bool TryCreate(DirectiveComponents components, out Directive directive)
		{
			directive = new TextFontDirective(components.Value);
			return true;
		}

		protected override string GetValue(Directive directive)
		{
			return (directive as TextFontDirective)?.FontFamily;
		}

		public override string LongName { get { return "textfont"; } }
		public override string ShortName { get { return "tf"; } }
		public override ComponentPresence SubKey { get { return ComponentPresence.NotAllowed; } }
		public override ComponentPresence Value { get { return ComponentPresence.Required; } }
	}
}
