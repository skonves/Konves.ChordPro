namespace Konves.ChordPro
{
	public sealed class Syllable
	{
		public Syllable(Chord chord, string text)
		{
			Chord = chord;
			Text = text;
		}
		public Chord Chord { get; set; }
		public string Text { get; set; }

		public override string ToString()
		{
			return $"{Chord?.ToString()}{Text}";
		}
	}
}
