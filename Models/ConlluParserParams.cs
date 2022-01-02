using System.ComponentModel;

namespace ConlluParser.Models
{
    public class ConlluParserParams
    {
        [DisplayName("CoNLL-U file")]
        public string ConlluFilePath { get; set; }

        [DisplayName("Remove Punctuation")]
        public bool RemovePunctuation { get; set; }

        [DisplayName("Convert to lowercase")]
        public bool ToLowerCase { get; set; }

        [DisplayName("Remove comments")]
        public bool RemoveComments { get; set; }

        [DisplayName("Remove range lines")]
        public bool RemoveRangeLines { get; set; }

        [DisplayName("Remove lines with subindexes")]
        public bool RemoveSubindexLines { get; set; }
    }
}