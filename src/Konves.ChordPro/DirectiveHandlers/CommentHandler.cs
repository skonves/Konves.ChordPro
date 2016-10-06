using Konves.ChordPro.Directives;

namespace Konves.ChordPro.DirectiveHandlers
{
	public sealed class CommentHandler : DirectiveHandler<CommentDirective>
	{
		private CommentHandler() { }

		public static CommentHandler Instance { get; } = new CommentHandler();

		protected override bool TryCreate(DirectiveComponents components, out Directive directive)
		{
			directive = new CommentDirective(components.Value);
			return true;
		}

		protected override string GetValue(Directive directive)
		{
			return (directive as CommentDirective)?.Text;
		}

		public override string LongName { get { return "comment"; } }
		public override string ShortName { get { return "c"; } }
		public override ComponentPresence SubKey { get { return ComponentPresence.NotAllowed; } }
		public override ComponentPresence Value { get { return ComponentPresence.Required; } }
	}
}
