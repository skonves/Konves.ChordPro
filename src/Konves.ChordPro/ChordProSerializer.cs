using Konves.ChordPro.DirectiveHandlers;
using Konves.ChordPro.Directives;
using System;
using System.Collections.Generic;
using System.IO;

namespace Konves.ChordPro
{
	public static class ChordProSerializer
	{
		public static Document Deserialize(Stream stream)
		{
			return Deserialize(stream, null);
		}

		public static Document Deserialize(Stream stream, IEnumerable<DirectiveHandler> customHandlers)
		{
			using (StreamReader reader = new StreamReader(stream))
			{
				xParser parser = new xParser(reader);
				return new Document(parser.Parse());
			}
		}

		public static void Serialize(Document document, TextWriter writer, bool shorten = false)
		{
			Serialize(document, writer, null, shorten);
		}

		public static void Serialize(Document document, TextWriter writer, IEnumerable<DirectiveHandler> customHandlers, bool shorten = false)
		{
			var index = DirectiveHandlerUtility.GetHandlersDictionaryByType(customHandlers);

			foreach (ILine line in document.Lines)
			{
				if (line is Directive)
					writer.WriteLine(index[line.GetType()].GetString(line as Directive, shorten)); // TODO: harden
				else if (line is SongLine)
					writer.WriteLine(line.ToString()); // TODO: fix
				else if (line is TabLine)
					writer.WriteLine((line as TabLine).Text);
				else
					throw new ArgumentException("unknown line type");
			}
		}
	}
}
