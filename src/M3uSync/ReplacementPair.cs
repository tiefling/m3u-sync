namespace M3uSync
{
    public class ReplacementPair
    {
        public ReplacementPair(string original, string replacement)
        {
            Original = original;
            Replacement = replacement;
        }

        public string Original { get; private set; }
        public string Replacement { get; set; }
    }
}
