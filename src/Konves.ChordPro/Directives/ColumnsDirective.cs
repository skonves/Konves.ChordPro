namespace Konves.ChordPro.Directives
{
	public sealed class ColumnsDirective : Directive
	{
		public ColumnsDirective(int number)
		{
			Number = number;
		}

		public int Number { get; set; }
	}
}
