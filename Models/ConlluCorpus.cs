using System.Collections.Generic;
using System.Linq;

namespace ConlluParser.Models
{
    public class ConlluCorpus
    {
        public ConlluCorpus()
        {
            Sentences = new List<ConlluSentence>();
        }

        public List<ConlluSentence> Sentences { get; private set; }

        public void RemoveLines(ConlluParserParams options)
        {
            foreach (var s in Sentences)
            {
                s.RemoveLines(options);
            }
            //Sentences.ForEach(s => s.RemoveLines(options));
        }

        public void ToLowerCase() => Sentences.ForEach(s => s.ToLowerCase());

        public void RemoveComments() => Sentences.ForEach(s => s.RemoveComments());

        public int NumSentences => Sentences.Count();

        public int NumLines => Sentences.SelectMany(s => s.Lines).Count();

        public int NumComments => Sentences.SelectMany(s => s.Comments).Count();

        public int NumPunctuationLines => Sentences
            .SelectMany(s => s.Lines)
            .Where(l => l.IsPunctuation).Count();

        public int NumLinesWithSubindexes => Sentences
            .SelectMany(s => s.Lines)
            .Where(l => l.IsSubindex).Count();

        public int NumLinesWithRanges => Sentences
            .SelectMany(s => s.Lines)
            .Where(l => l.IsRange).Count();

        public string GetOutput(bool divideWithEmptyLine = false, int maxLines = 0)
        {
            var content = Sentences.Select(s => s.ToString());
            if (maxLines > 0)
            {
                content = content.Take(maxLines);
                if (content.Any())
                {
                    content = content.Concat(new List<string> { $"===== Content has been limited to {maxLines} lines =====" });
                }
            }
            var separator = divideWithEmptyLine
                ? "\r\n\r\n"
                : "\r\n";
            return string.Join(separator, content);
        }
    }
}