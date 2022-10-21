namespace ConlluParser.Models
{
    public class ConlluParserViewModel
    {
        public static string[] CorpusNames = { "all", "atis", "ewt", "gum", "lines", "partut" };
        public static string[] Datasets = { "train", "dev", "test" };

        public ConlluParserViewModel()
        {
            Params = new ConlluParserParams
            {
                ToLowerCase = true,
                RemoveComments = true,
                RemovePunctuation = true,
                RemoveRangeLines = true,
                RemoveSubindexLines = true,
                DivideWithEmptyLines = false,
            };
            Corpus = new ConlluCorpus();
        }

        public ConlluParserParams Params { get; set; }
        public ConlluCorpus Corpus { get; set; }

    }
}