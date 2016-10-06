using System;
using System.Text.RegularExpressions;

namespace Konves.ChordPro
{
	public sealed class DirectiveComponents
	{
		public DirectiveComponents(string key, string subKey, string value)
		{
			Key = key;
			SubKey = subKey;
			Value = value;
		}

		public static DirectiveComponents Parse(string s)
		{
			DirectiveComponents components;
			if (TryParse(s, out components))
				return components;

			throw new FormatException("Directive is not in the format {key[ subkey][:value]}");
		}

		public static bool TryParse(string s, out DirectiveComponents components)
		{
			Match match = _directiveRegex.Match(s);

			if (match == null || string.IsNullOrWhiteSpace(match.Groups["key"].Value))
			{
				components = null;
				return false;
			}

			components = new DirectiveComponents(
				key: match.Groups["key"]?.Value.ToLower().Trim(),
				subKey: match.Groups["subkey"]?.Value.Trim(),
				value: match.Groups["value"]?.Value.Trim());

			return true;
		}

		public string Key { get; }
		public string SubKey { get; }
		public string Value { get; }

		static readonly Regex _directiveRegex = new Regex(@"^\s*{\s*(?<key>[^\s:}]+)(?:\s+(?<subkey>[^:}]*))?(?:\:(?<value>[^}]+))?}\s*", RegexOptions.Compiled);
	}
}
