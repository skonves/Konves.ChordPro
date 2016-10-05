using Konves.ChordPro.Directives;

namespace Konves.ChordPro.DirectiveHandlers
{
	public sealed class CommentItalicHandler : DirectiveHandler<CommentItalicDirective>
	{
		protected override bool TryCreate(DirectiveComponents components, out Directive directive)
		{
			directive = new CommentItalicDirective(components.Value);
			return true;
		}

		protected override string GetValue(Directive directive)
		{
			return (directive as CommentItalicDirective)?.Text;
		}

		public override string LongName { get { return "comment_italic"; } }
		public override string ShortName { get { return "ci"; } }
		public override ComponentPresence SubKey { get { return ComponentPresence.NotAllowed; } }
		public override ComponentPresence Value { get { return ComponentPresence.Required; } }
	}
}
