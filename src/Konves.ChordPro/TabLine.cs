namespace Konves.ChordPro
{
	public sealed class TabLine : ILine
	{
		public TabLine()
		{
			Text = string.Empty;
		}

		public TabLine(string text)
		{
			Text = text;
		}

		public string Text { get; set; }

		public override string ToString()
		{
			return Text;
		}
	}
}
