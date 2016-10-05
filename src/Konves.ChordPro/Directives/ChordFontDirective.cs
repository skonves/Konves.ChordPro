namespace Konves.ChordPro.Directives
{
	public sealed class ChordFontDirective : Directive
	{
		public ChordFontDirective(string fontFamily)
		{
			FontFamily = fontFamily;
		}

		public string FontFamily { get; set; }
	}
}
