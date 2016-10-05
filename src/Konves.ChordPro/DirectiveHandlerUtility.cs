using Konves.ChordPro.DirectiveHandlers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Konves.ChordPro
{
	internal static class DirectiveHandlerUtility
	{
		internal static IReadOnlyDictionary<string, DirectiveHandler> GetHandlersDictionaryByName(IEnumerable<DirectiveHandler> customParsers)
		{
			Dictionary<string, DirectiveHandler> index = new Dictionary<string, DirectiveHandler>();

			foreach (DirectiveHandler parser in _defaultDirectiveParsers ?? Enumerable.Empty<DirectiveHandler>())
			{
				index[parser.LongName] = parser;
				index[parser.ShortName] = parser;
			}

			foreach (DirectiveHandler parser in customParsers ?? Enumerable.Empty<DirectiveHandler>())
			{
				index[parser.LongName] = parser;
				index[parser.ShortName] = parser;
			}

			return index;
		}

		internal static IReadOnlyDictionary<Type, DirectiveHandler> GetHandlersDictionaryByType(IEnumerable<DirectiveHandler> customParsers)
		{
			Dictionary<Type, DirectiveHandler> index = new Dictionary<Type, DirectiveHandler>();

			foreach (DirectiveHandler parser in _defaultDirectiveParsers ?? Enumerable.Empty<DirectiveHandler>())
			{
				index[parser.DirectiveType] = parser;
			}

			foreach (DirectiveHandler parser in customParsers ?? Enumerable.Empty<DirectiveHandler>())
			{
				index[parser.DirectiveType] = parser;
			}

			return index;
		}

		static readonly IReadOnlyCollection<DirectiveHandler> _defaultDirectiveParsers = new DirectiveHandler[] {
			new ChordColourHandler(),
			new ChordFontHandler(),
			new ChordSizeHandler(),
			new ColumnsHandler(),
			new ColumnBreakHandler(),
			new CommentHandler(),
			new CommentBoxHandler(),
			new CommentItalicHandler(),
			new DefineHandler(),
			new EndOfChorusHandler(),
			new EndOfTabHandler(),
			new GridHandler(),
			new NewPageHandler(),
			new NewPhysicalPageHandler(),
			new NewSongHandler(),
			new NoGridHandler(),
			new PageTypeHandler(),
			new StartOfChorusHandler(),
			new StartOfTabHandler(),
			new SubtitleHandler(),
			new TextFontHandler(),
			new TextSizeHandler(),
			new TitleHandler(),
			new TitlesHandler()
		};
	}
}
