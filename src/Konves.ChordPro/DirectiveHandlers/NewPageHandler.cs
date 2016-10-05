using Konves.ChordPro.Directives;

namespace Konves.ChordPro.DirectiveHandlers
{
	public sealed class NewPageHandler : DirectiveHandler<NewPageDirective>
	{
		protected override bool TryCreate(DirectiveComponents components, out Directive directive)
		{
			directive = new NewPageDirective();
			return true;
		}

		public override string LongName { get { return "new_page"; } }
		public override string ShortName { get { return "np"; } }
		public override ComponentPresence SubKey { get { return ComponentPresence.NotAllowed; } }
		public override ComponentPresence Value { get { return ComponentPresence.NotAllowed; } }
	}
}
