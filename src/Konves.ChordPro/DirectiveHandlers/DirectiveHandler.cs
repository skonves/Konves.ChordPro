using Konves.ChordPro.Directives;
using System;

namespace Konves.ChordPro.DirectiveHandlers
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
					return false;

				if (Value == ComponentPresence.NotAllowed && !string.IsNullOrWhiteSpace(components.Value))
					return false;

				if (SubKey == ComponentPresence.Required && string.IsNullOrWhiteSpace(components.SubKey))
					return false;

				if (SubKey == ComponentPresence.NotAllowed && !string.IsNullOrWhiteSpace(components.SubKey))
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
			if (ReferenceEquals(directive, null))
				throw new ArgumentNullException(nameof(directive));

			string key = shorten ? ShortName : LongName;
			string subkey = GetSubKey(directive);
			string value = GetValue(directive);

			string subkeyString = GetSubKeyString(subkey);
			string valueString = GetValueString(value);

			return $"{{{key}{subkeyString}{valueString}}}";
		}

		internal string GetSubKeyString(string subkey)
		{
			return SubKey != ComponentPresence.NotAllowed && !string.IsNullOrWhiteSpace(subkey) ? " " + subkey : null;
		}

		internal string GetValueString(string value)
		{
			return Value != ComponentPresence.NotAllowed && !string.IsNullOrWhiteSpace(value) ? ": " + value : null;
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
