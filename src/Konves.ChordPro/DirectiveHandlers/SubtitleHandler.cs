using Konves.ChordPro.Directives;

namespace Konves.ChordPro.DirectiveHandlers
{
	public sealed class SubtitleHandler : DirectiveHandler<SubtitleDirective>
	{
		private SubtitleHandler() { }

		public static SubtitleHandler Instance { get; } = new SubtitleHandler();

		protected override bool TryCreate(DirectiveComponents components, out Directive directive)
		{
			directive = new SubtitleDirective(components.Value);
			return true;
		}

		protected override string GetValue(Directive directive)
		{
			return (directive as SubtitleDirective)?.Text;
		}

		public override string LongName { get { return "subtitle"; } }
		public override string ShortName { get { return "st"; } }
		public override ComponentPresence SubKey { get { return ComponentPresence.NotAllowed; } }
		public override ComponentPresence Value { get { return ComponentPresence.Required; } }
	}
}
