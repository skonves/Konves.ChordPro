using Konves.ChordPro.Directives;

namespace Konves.ChordPro.DirectiveHandlers
{
	public sealed class NewPhysicalPageHandler : DirectiveHandler<NewPhysicalPageDirective>
	{
		private NewPhysicalPageHandler() { }

		public static NewPhysicalPageHandler Instance { get; } = new NewPhysicalPageHandler();

		protected override bool TryCreate(DirectiveComponents components, out Directive directive)
		{
			directive = new NewPhysicalPageDirective();
			return true;
		}

		public override string LongName { get { return "new_physical_page"; } }
		public override string ShortName { get { return "npp"; } }
		public override ComponentPresence SubKey { get { return ComponentPresence.NotAllowed; } }
		public override ComponentPresence Value { get { return ComponentPresence.NotAllowed; } }
	}
}
