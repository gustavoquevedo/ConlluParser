namespace ConlluParser.Models
{
    public class ConlluParserViewModel
    {
        public ConlluParserViewModel()
        {
            Params = new ConlluParserParams
            {
                RemoveComments = true,
                RemovePunctuation = true,
                RemoveRangeLines = true,
                RemoveSubindexLines = true,
                ToLowerCase = true
            };
            Corpus = new ConlluCorpus();
        }

        public ConlluParserParams Params { get; set; }
        public ConlluCorpus Corpus { get; set; }
    }
}