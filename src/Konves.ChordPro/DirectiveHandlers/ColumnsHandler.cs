using Konves.ChordPro.Directives;

namespace Konves.ChordPro.DirectiveHandlers
{
	public sealed class ColumnsHandler : DirectiveHandler<ColumnsDirective>
	{
		protected override bool TryCreate(DirectiveComponents components, out Directive directive)
		{
			int value;
			if (int.TryParse(components.Value, out value))
			{
				directive = new ColumnsDirective(value);
				return true;
			}

			directive = null;
			return false;
		}

		protected override string GetValue(Directive directive)
		{
			return (directive as ColumnsDirective)?.Number.ToString();
		}

		public override string LongName { get { return "columns"; } }
		public override string ShortName { get { return "col"; } }
		public override ComponentPresence SubKey { get { return ComponentPresence.NotAllowed; } }
		public override ComponentPresence Value { get { return ComponentPresence.Required; } }
	}
}
