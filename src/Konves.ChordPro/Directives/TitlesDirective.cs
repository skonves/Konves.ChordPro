namespace Konves.ChordPro.Directives
{
	public sealed class TitlesDirective : Directive
	{
		public TitlesDirective(Alignment flush)
		{
			Flush = flush;
		}

		public Alignment Flush { get; set; }
	}
}
