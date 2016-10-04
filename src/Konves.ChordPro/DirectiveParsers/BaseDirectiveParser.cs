using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konves.ChordPro.DirectiveParsers
{
	public abstract class DirectiveParser
	{
		public virtual bool TryParse(DirectiveComponents components, out Directive directive)
		{
			directive = null;

			if (components.Key == LongName || components.Key == ShortName)
			{
				if (Value == ComponentPresence.Required && string.IsNullOrWhiteSpace(components.Value))
					// throw new FormatException($"Value required for '{components.Key}' directive.");
					return false;

				if (Value == ComponentPresence.NotAllowed && !string.IsNullOrWhiteSpace(components.Value))
					// throw new FormatException($"Value not allowed for '{components.Key}' directive.");
					return false;

				if (SubKey == ComponentPresence.Required && string.IsNullOrWhiteSpace(components.SubKey))
					// throw new FormatException($"Sub-key required for '{components.Key}' directive.");
					return false;

				if (SubKey == ComponentPresence.NotAllowed && !string.IsNullOrWhiteSpace(components.SubKey))
					// throw new FormatException($"Sub-key not allowed for '{components.Key}' directive.");
					return false;

				return TryCreate(components, out directive);
			}
			return false;
		}

		protected abstract bool TryCreate(DirectiveComponents components, out Directive directive);

		public abstract ComponentPresence SubKey { get; }
		public abstract ComponentPresence Value { get; }
		public abstract string LongName { get; }
		public virtual string ShortName { get { return LongName; } }
	}

	public enum ComponentPresence
	{
		Required,
		Optional,
		NotAllowed
	}
}
