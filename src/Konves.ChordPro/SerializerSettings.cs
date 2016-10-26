using Konves.ChordPro.DirectiveHandlers;
using System.Collections.Generic;

namespace Konves.ChordPro
{
	public sealed class SerializerSettings
	{
		public bool ShortenDirectives { get; set; }
		public List<DirectiveHandler> CustomHandlers { get; set; }
	}
}
