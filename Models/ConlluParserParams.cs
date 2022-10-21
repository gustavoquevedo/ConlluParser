using System.ComponentModel;

namespace ConlluParser.Models
{
    public class ConlluParserParams
    {
        [DisplayName("CoNLL-U file")]
        public string ConlluFilePath { get; set; }

        [DisplayName("Convert to Lowercase")]
        public bool ToLowerCase { get; set; }

        [DisplayName("Remove Punctuation")]
        public bool RemovePunctuation { get; set; }

        [DisplayName("Remove Comments")]
        public bool RemoveComments { get; set; }

        [DisplayName("Remove Range lines")]
        public bool RemoveRangeLines { get; set; }

        [DisplayName("Remove lines with Subindexes")]
        public bool RemoveSubindexLines { get; set; }

        [DisplayName("Divide with Empty lines")]
        public bool DivideWithEmptyLines { get; set; }

        public string CorpusName { get; set; }
        public string Dataset { get; set; }
    }
}