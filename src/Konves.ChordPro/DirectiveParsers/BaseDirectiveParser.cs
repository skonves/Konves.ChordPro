using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konves.ChordPro.DirectiveParsers
{
	public abstract class DirectiveHandler<TDirective> : DirectiveHandler where TDirective : Directive
	{
		public override Type DirectiveType { get { return typeof(TDirective); } }
	}

	public abstract class DirectiveHandler
	{
		public bool TryParse(DirectiveComponents components, out Directive directive)
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

		protected virtual string GetValue(Directive directive)
		{
			return null;
		}

		protected virtual string GetSubKey(Directive directive)
		{
			return null;
		}

		public string GetString(Directive directive, bool shorten = false)
		{
			string key = shorten ? ShortName : LongName;
			string subkey = GetSubKey(directive);
			string value = GetValue(directive);

			string subkeyString = SubKey != ComponentPresence.NotAllowed && !string.IsNullOrWhiteSpace(subkey) ? " " + subkey : null;
			string valueString = Value != ComponentPresence.NotAllowed && !string.IsNullOrWhiteSpace(value) ? ": " + value : null;

			return $"{{{key}{subkeyString}{valueString}}}";
		}

		public abstract ComponentPresence SubKey { get; }
		public abstract ComponentPresence Value { get; }
		public abstract string LongName { get; }
		public virtual string ShortName { get { return LongName; } }
		public abstract Type DirectiveType { get; }
	}

	public enum ComponentPresence
	{
		Required,
		Optional,
		NotAllowed
	}
}
