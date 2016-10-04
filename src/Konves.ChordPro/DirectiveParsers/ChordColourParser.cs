using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konves.ChordPro.DirectiveParsers
{
	public sealed class ChordColourHandler : DirectiveHandler<ChordColourDirective>
	{
		protected override bool TryCreate(DirectiveComponents components, out Directive directive)
		{
			directive = new ChordColourDirective(components.Value);
			return true;
		}

		protected override string GetValue(Directive directive)
		{
			return (directive as ChordColourDirective)?.Colour;
		}

		public override string LongName { get { return "chordcolour"; } }
		public override ComponentPresence SubKey { get { return ComponentPresence.NotAllowed; } }
		public override ComponentPresence Value { get { return ComponentPresence.Required; } }
	}

	public sealed class ChordFontHandler : DirectiveHandler<ChordFontDirective>
	{
		protected override bool TryCreate(DirectiveComponents components, out Directive directive)
		{
			directive = new ChordFontDirective(components.Value);
			return true;
		}

		protected override string GetValue(Directive directive)
		{
			return (directive as ChordFontDirective)?.FontFamily;
		}

		public override string LongName { get { return "chordfont"; } }
		public override string ShortName { get { return "cf"; } }
		public override ComponentPresence SubKey { get { return ComponentPresence.NotAllowed; } }
		public override ComponentPresence Value { get { return ComponentPresence.Required; } }
	}

	public sealed class ChordSizeHandler : DirectiveHandler<ChordSizeDirective>
	{
		protected override bool TryCreate(DirectiveComponents components, out Directive directive)
		{
			int value;
			if (int.TryParse(components.Value, out value))
			{
				directive = new ChordSizeDirective(value);
				return true;
			}

			directive = null;
			return false;
		}

		protected override string GetValue(Directive directive)
		{
			return (directive as ChordSizeDirective)?.FontSize.ToString();
		}

		public override string LongName { get { return "chordsize"; } }
		public override string ShortName { get { return "cs"; } }
		public override ComponentPresence SubKey { get { return ComponentPresence.NotAllowed; } }
		public override ComponentPresence Value { get { return ComponentPresence.Required; } }
	}

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

	public sealed class ColumnBreakHandler : DirectiveHandler<ColumnBreakDirective>
	{
		protected override bool TryCreate(DirectiveComponents components, out Directive directive)
		{
			directive = new ColumnBreakDirective();
			return true;
		}

		public override string LongName { get { return "column_break"; } }
		public override string ShortName { get { return "colb"; } }
		public override ComponentPresence SubKey { get { return ComponentPresence.NotAllowed; } }
		public override ComponentPresence Value { get { return ComponentPresence.NotAllowed; } }
	}

	public sealed class CommentHandler : DirectiveHandler<CommentDirective>
	{
		protected override bool TryCreate(DirectiveComponents components, out Directive directive)
		{
			directive = new CommentDirective(components.Value);
			return true;
		}

		protected override string GetValue(Directive directive)
		{
			return (directive as CommentDirective)?.Text;
		}

		public override string LongName { get { return "comment"; } }
		public override string ShortName { get { return "c"; } }
		public override ComponentPresence SubKey { get { return ComponentPresence.NotAllowed; } }
		public override ComponentPresence Value { get { return ComponentPresence.Required; } }
	}

	public sealed class CommentBoxHandler : DirectiveHandler<CommentBoxDirective>
	{
		protected override bool TryCreate(DirectiveComponents components, out Directive directive)
		{
			directive = new CommentBoxDirective(components.Value);
			return true;
		}

		protected override string GetValue(Directive directive)
		{
			return (directive as CommentBoxDirective)?.Text;
		}

		public override string LongName { get { return "comment_box"; } }
		public override string ShortName { get { return "cb"; } }
		public override ComponentPresence SubKey { get { return ComponentPresence.NotAllowed; } }
		public override ComponentPresence Value { get { return ComponentPresence.Required; } }
	}

	public sealed class CommentItalicHandler : DirectiveHandler<CommentItalicDirective>
	{
		protected override bool TryCreate(DirectiveComponents components, out Directive directive)
		{
			directive = new CommentItalicDirective(components.Value);
			return true;
		}

		protected override string GetValue(Directive directive)
		{
			return (directive as CommentItalicDirective)?.Text;
		}

		public override string LongName { get { return "comment_italic"; } }
		public override string ShortName { get { return "ci"; } }
		public override ComponentPresence SubKey { get { return ComponentPresence.NotAllowed; } }
		public override ComponentPresence Value { get { return ComponentPresence.Required; } }
	}

	public sealed class DefineHandler : DirectiveHandler<DefineDirective>
	{
		protected override bool TryCreate(DirectiveComponents components, out Directive directive)
		{
			directive = new DefineDirective(components.SubKey, components.Value); //TODO: allow various formats
			return true;
		}

		protected override string GetSubKey(Directive directive)
		{
			return (directive as DefineDirective)?.Chord;
		}

		protected override string GetValue(Directive directive)
		{
			return (directive as DefineDirective)?.Definition;
		}

		public override string LongName { get { return "define"; } }
		public override ComponentPresence SubKey { get { return ComponentPresence.Required; } }
		public override ComponentPresence Value { get { return ComponentPresence.Required; } }
	}

	public sealed class EndOfChorusHandler : DirectiveHandler<EndOfChorusDirective> //////////////////////////////////////
	{
		protected override bool TryCreate(DirectiveComponents components, out Directive directive)
		{
			directive = new EndOfChorusDirective();
			return true;
		}

		public override string LongName { get { return "end_of_chorus"; } }
		public override string ShortName { get { return "eoc"; } }
		public override ComponentPresence SubKey { get { return ComponentPresence.NotAllowed; } }
		public override ComponentPresence Value { get { return ComponentPresence.NotAllowed; } }
	}

	public sealed class EndOfTabHandler : DirectiveHandler<EndOfTabDirective>
	{
		protected override bool TryCreate(DirectiveComponents components, out Directive directive)
		{
			directive = new EndOfTabDirective();
			return true;
		}

		public override string LongName { get { return "end_of_tab"; } }
		public override string ShortName { get { return "eot"; } }
		public override ComponentPresence SubKey { get { return ComponentPresence.NotAllowed; } }
		public override ComponentPresence Value { get { return ComponentPresence.NotAllowed; } }
	}

	public sealed class GridHandler : DirectiveHandler<GridDirective>
	{
		protected override bool TryCreate(DirectiveComponents components, out Directive directive)
		{
			directive = new GridDirective();
			return true;
		}

		public override string LongName { get { return "grid"; } }
		public override string ShortName { get { return "g"; } }
		public override ComponentPresence SubKey { get { return ComponentPresence.NotAllowed; } }
		public override ComponentPresence Value { get { return ComponentPresence.NotAllowed; } }
	}

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

	public sealed class NewPhysicalPageHandler : DirectiveHandler<NewPhysicalPageDirective>
	{
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

	public sealed class NewSongHandler : DirectiveHandler<NewSongDirective>
	{
		protected override bool TryCreate(DirectiveComponents components, out Directive directive)
		{
			directive = new NewSongDirective();
			return true;
		}

		public override string LongName { get { return "new_song"; } }
		public override string ShortName { get { return "ns"; } }
		public override ComponentPresence SubKey { get { return ComponentPresence.NotAllowed; } }
		public override ComponentPresence Value { get { return ComponentPresence.NotAllowed; } }
	}

	public sealed class NoGridHandler : DirectiveHandler<NoGridDirective>
	{
		protected override bool TryCreate(DirectiveComponents components, out Directive directive)
		{
			directive = new NoGridDirective();
			return true;
		}

		public override string LongName { get { return "no_grid"; } }
		public override string ShortName { get { return "ng"; } }
		public override ComponentPresence SubKey { get { return ComponentPresence.NotAllowed; } }
		public override ComponentPresence Value { get { return ComponentPresence.NotAllowed; } }
	}

	public sealed class PageTypeHandler : DirectiveHandler<PageTypeDirective>
	{
		protected override bool TryCreate(DirectiveComponents components, out Directive directive)
		{
			switch (components.Value.ToLower())
			{
				case "a4":
					directive = new PageTypeDirective(PageType.A4);
					return true;
				case "letter":
					directive = new PageTypeDirective(PageType.Letter);
					return true;
				default:
					directive = null;
					return false;
			}
		}

		protected override string GetValue(Directive directive)
		{
			return (directive as PageTypeDirective)?.PageType.ToString().ToLower();
		}

		public override string LongName { get { return "pagetype"; } }
		public override ComponentPresence SubKey { get { return ComponentPresence.NotAllowed; } }
		public override ComponentPresence Value { get { return ComponentPresence.Required; } }
	}

	public sealed class StartOfChorusHandler : DirectiveHandler<StartOfChorusDirective>
	{
		protected override bool TryCreate(DirectiveComponents components, out Directive directive)
		{
			directive = new StartOfChorusDirective();
			return true;
		}

		public override string LongName { get { return "start_of_chorus"; } }
		public override string ShortName { get { return "soc"; } }
		public override ComponentPresence SubKey { get { return ComponentPresence.NotAllowed; } }
		public override ComponentPresence Value { get { return ComponentPresence.NotAllowed; } }
	}

	public sealed class StartOfTabHandler : DirectiveHandler<StartOfTabDirective>
	{
		protected override bool TryCreate(DirectiveComponents components, out Directive directive)
		{
			directive = new StartOfTabDirective();
			return true;
		}

		public override string LongName { get { return "start_of_tab"; } }
		public override string ShortName { get { return "sot"; } }
		public override ComponentPresence SubKey { get { return ComponentPresence.NotAllowed; } }
		public override ComponentPresence Value { get { return ComponentPresence.NotAllowed; } }
	}

	public sealed class SubtitleHandler : DirectiveHandler<SubtitleDirective>
	{
		protected override bool TryCreate(DirectiveComponents components, out Directive directive)
		{
			directive = new SubtitleDirective(components.Value);
			return true;
		}

		protected override string GetValue(Directive directive)
		{
			return (directive as SubtitleDirective)?.Text;
		}

		public override string LongName { get { return "subtitle"; } }
		public override string ShortName { get { return "st"; } }
		public override ComponentPresence SubKey { get { return ComponentPresence.NotAllowed; } }
		public override ComponentPresence Value { get { return ComponentPresence.Required; } }
	}

	public sealed class TextFontHandler : DirectiveHandler<TextFontDirective>
	{
		protected override bool TryCreate(DirectiveComponents components, out Directive directive)
		{
			directive = new TextFontDirective(components.Value);
			return true;
		}

		protected override string GetValue(Directive directive)
		{
			return (directive as TextFontDirective)?.FontFamily;
		}

		public override string LongName { get { return "textfont"; } }
		public override string ShortName { get { return "tf"; } }
		public override ComponentPresence SubKey { get { return ComponentPresence.NotAllowed; } }
		public override ComponentPresence Value { get { return ComponentPresence.Required; } }
	}

	public sealed class TextSizeHandler : DirectiveHandler<TextSizeDirective>
	{
		protected override bool TryCreate(DirectiveComponents components, out Directive directive)
		{
			int value;
			if (int.TryParse(components.Value, out value))
			{
				directive = new TextSizeDirective(value);
				return true;
			}

			directive = null;
			return false;
		}

		protected override string GetValue(Directive directive)
		{
			return (directive as TextSizeDirective)?.FontSize.ToString();
		}

		public override string LongName { get { return "textsize"; } }
		public override string ShortName { get { return "ts"; } }
		public override ComponentPresence SubKey { get { return ComponentPresence.NotAllowed; } }
		public override ComponentPresence Value { get { return ComponentPresence.Required; } }
	}

	public sealed class TitleHandler : DirectiveHandler<TitleDirective>
	{
		protected override bool TryCreate(DirectiveComponents components, out Directive directive)
		{
			directive = new TitleDirective(components.Value);
			return true;
		}

		protected override string GetValue(Directive directive)
		{
			return (directive as TitleDirective)?.Text;
		}

		public override string LongName { get { return "title"; } }
		public override string ShortName { get { return "t"; } }
		public override ComponentPresence SubKey { get { return ComponentPresence.NotAllowed; } }
		public override ComponentPresence Value { get { return ComponentPresence.Required; } }
	}

	public sealed class TitlesHandler : DirectiveHandler<TitlesDirective>
	{
		protected override bool TryCreate(DirectiveComponents components, out Directive directive)
		{
			switch (components.Value.ToLower())
			{
				case "left":
					directive = new TitlesDirective(Alignment.Left);
					return true;
				case "center":
					directive = new TitlesDirective(Alignment.Center);
					return true;
				default:
					directive = null;
					return false;
			}
		}

		protected override string GetValue(Directive directive)
		{
			return (directive as TitlesDirective)?.Flush.ToString().ToLower();
		}

		public override string LongName { get { return "titles"; } }
		public override ComponentPresence SubKey { get { return ComponentPresence.NotAllowed; } }
		public override ComponentPresence Value { get { return ComponentPresence.Required; } }
	}
}
