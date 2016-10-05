namespace Konves.ChordPro
{
	public sealed class Chord : Block
	{
		public Chord(string text)
		{
			Text = text;
		}

		public string Text { get; set; }

		public override string ToString()
		{
			return string.IsNullOrEmpty(Text) ? string.Empty : $"[{Text}]";
		}
	}
}
