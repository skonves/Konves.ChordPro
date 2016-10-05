namespace Konves.ChordPro.Directives
{
	public sealed class ChordColourDirective : Directive
	{
		public ChordColourDirective(string colour)
		{
			Colour = colour;
		}

		public string Colour { get; set; }
	}
}
