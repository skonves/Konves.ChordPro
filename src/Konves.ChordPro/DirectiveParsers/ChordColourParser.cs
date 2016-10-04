using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konves.ChordPro.DirectiveParsers
{
	public sealed class ChordColourParser : DirectiveParser
	{
		protected override bool TryCreate(DirectiveComponents components, out Directive directive)
		{
			directive = new ChordColourDirective(components.Value);
			return true;
		}

		public override string LongName { get { return "chordcolour"; } }
		public override ComponentPresence SubKey { get { return ComponentPresence.NotAllowed; } }
		public override ComponentPresence Value { get { return ComponentPresence.Required; } }
	}

	public sealed class ChordFontParser : DirectiveParser
	{
		protected override bool TryCreate(DirectiveComponents components, out Directive directive)
		{
			directive = new ChordFontDirective(components.Value);
			return true;
		}

		public override string LongName { get { return "chordfont"; } }
		public override string ShortName { get { return "cf"; } }
		public override ComponentPresence SubKey { get { return ComponentPresence.NotAllowed; } }
		public override ComponentPresence Value { get { return ComponentPresence.Required; } }
	}

	public sealed class ChordSizeParser : DirectiveParser
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

		public override string LongName { get { return "chordsize"; } }
		public override string ShortName { get { return "cs"; } }
		public override ComponentPresence SubKey { get { return ComponentPresence.NotAllowed; } }
		public override ComponentPresence Value { get { return ComponentPresence.Required; } }
	}

	public sealed class ColumnsParser : DirectiveParser
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

		public override string LongName { get { return "columns"; } }
		public override string ShortName { get { return "col"; } }
		public override ComponentPresence SubKey { get { return ComponentPresence.NotAllowed; } }
		public override ComponentPresence Value { get { return ComponentPresence.Required; } }
	}

	public sealed class ColumnBreakParser : DirectiveParser
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

	public sealed class CommentParser : DirectiveParser
	{
		protected override bool TryCreate(DirectiveComponents components, out Directive directive)
		{
			directive = new CommentDirective(components.Value);
			return true;
		}

		public override string LongName { get { return "comment"; } }
		public override string ShortName { get { return "c"; } }
		public override ComponentPresence SubKey { get { return ComponentPresence.NotAllowed; } }
		public override ComponentPresence Value { get { return ComponentPresence.Required; } }
	}

	public sealed class CommentBoxParser : DirectiveParser
	{
		protected override bool TryCreate(DirectiveComponents components, out Directive directive)
		{
			directive = new CommentBoxDirective(components.Value);
			return true;
		}

		public override string LongName { get { return "comment_box"; } }
		public override string ShortName { get { return "cb"; } }
		public override ComponentPresence SubKey { get { return ComponentPresence.NotAllowed; } }
		public override ComponentPresence Value { get { return ComponentPresence.Required; } }
	}

	public sealed class CommentItalicParser : DirectiveParser
	{
		protected override bool TryCreate(DirectiveComponents components, out Directive directive)
		{
			directive = new CommentItalicDirective(components.Value);
			return true;
		}

		public override string LongName { get { return "comment_italic"; } }
		public override string ShortName { get { return "ci"; } }
		public override ComponentPresence SubKey { get { return ComponentPresence.NotAllowed; } }
		public override ComponentPresence Value { get { return ComponentPresence.Required; } }
	}

	public sealed class DefineParser : DirectiveParser
	{
		protected override bool TryCreate(DirectiveComponents components, out Directive directive)
		{
			directive = new DefineDirective(components.SubKey, components.Value); //TODO: allow various formats
			return true;
		}

		public override string LongName { get { return "define"; } }
		public override ComponentPresence SubKey { get { return ComponentPresence.Required; } }
		public override ComponentPresence Value { get { return ComponentPresence.Required; } }
	}

	public sealed class EndOfChorusParser : DirectiveParser
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

	public sealed class EndOfTabParser : DirectiveParser
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

	public sealed class GridParser : DirectiveParser
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

	public sealed class NewPageParser : DirectiveParser
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

	public sealed class NewPhysicalPageParser : DirectiveParser
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

	public sealed class NewSongParser : DirectiveParser
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

	public sealed class NoGridParser : DirectiveParser
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

	public sealed class PageTypeParser : DirectiveParser
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

		public override string LongName { get { return "pagetype"; } }
		public override ComponentPresence SubKey { get { return ComponentPresence.NotAllowed; } }
		public override ComponentPresence Value { get { return ComponentPresence.Required; } }
	}

	public sealed class StartOfChorusParser : DirectiveParser
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

	public sealed class StartOfTabParser : DirectiveParser
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

	public sealed class SubtitleParser : DirectiveParser
	{
		protected override bool TryCreate(DirectiveComponents components, out Directive directive)
		{
			directive = new SubtitleDirective(components.Value);
			return true;
		}

		public override string LongName { get { return "subtitle"; } }
		public override string ShortName { get { return "st"; } }
		public override ComponentPresence SubKey { get { return ComponentPresence.NotAllowed; } }
		public override ComponentPresence Value { get { return ComponentPresence.Required; } }
	}

	public sealed class TextFontParser : DirectiveParser
	{
		protected override bool TryCreate(DirectiveComponents components, out Directive directive)
		{
			directive = new TextFontDirective(components.Value);
			return true;
		}

		public override string LongName { get { return "textfont"; } }
		public override string ShortName { get { return "tf"; } }
		public override ComponentPresence SubKey { get { return ComponentPresence.NotAllowed; } }
		public override ComponentPresence Value { get { return ComponentPresence.Required; } }
	}

	public sealed class TextSizeParser : DirectiveParser
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

		public override string LongName { get { return "textsize"; } }
		public override string ShortName { get { return "ts"; } }
		public override ComponentPresence SubKey { get { return ComponentPresence.NotAllowed; } }
		public override ComponentPresence Value { get { return ComponentPresence.Required; } }
	}

	public sealed class TitleParser : DirectiveParser
	{
		protected override bool TryCreate(DirectiveComponents components, out Directive directive)
		{
			directive = new TitleDirective(components.Value);
			return true;
		}

		public override string LongName { get { return "title"; } }
		public override string ShortName { get { return "t"; } }
		public override ComponentPresence SubKey { get { return ComponentPresence.NotAllowed; } }
		public override ComponentPresence Value { get { return ComponentPresence.Required; } }
	}

	public sealed class TitlesParser : DirectiveParser
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

		public override string LongName { get { return "titles"; } }
		public override ComponentPresence SubKey { get { return ComponentPresence.NotAllowed; } }
		public override ComponentPresence Value { get { return ComponentPresence.Required; } }
	}
}
