namespace Konves.ChordPro.Directives
{
	public sealed class DefineDirective : Directive
	{
		public DefineDirective(string chord, string definition)
		{
			Chord = chord;
			Definition = definition;
		}

		public string Chord { get; set; }
		public string Definition { get; set; }
	}
}
