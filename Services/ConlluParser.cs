using ConlluParser.Models;
using System;
using System.IO;
using System.Linq;

namespace ConlluParser.Services
{
    public static class ConlluParser
    {
        private static ConlluCorpus LoadConlluFile(string path)
        {
            var corpus = new ConlluCorpus();
            var sentence = new ConlluSentence();
            foreach (string line in File.ReadLines(path))
            {
                if(string.IsNullOrWhiteSpace(line))
                {
                    if (sentence.Lines.Any())
                    {
                        corpus.Sentences.Add(sentence);
                        sentence = new ConlluSentence();
                    }
                }
                else if(line.StartsWith("#"))
                {
                    sentence.Comments.Add(line);
                }
                else
                {
                    var conlluLine = ParseConlluLine(line);
                    sentence.Lines.AddLast(conlluLine);
                }
            }
            if(sentence.Lines.Any())
            {
                corpus.Sentences.Add(sentence);
            }
            return corpus;
        }

        private static ConlluLine ParseConlluLine(string line)
        {
            var tokens = line.Split('\t');
            return new ConlluLine
            {
                IdStr = tokens[0],
                Form = tokens[1],
                Lemma = tokens[2],
                Upos = tokens[3],
                Xpos = tokens[4],
                Feats = tokens[5],
                HeadStr = tokens[6],
                Deprel = tokens[7],
                Deps = tokens[8],
                Misc = tokens[9]
            };
        }

        public static ConlluCorpus ParseConlluFile(ConlluParserParams options)
        {
            var corpus = LoadConlluFile(options.ConlluFilePath);

            corpus.RemoveLines(options);

            if (options.RemoveComments)
            {
                corpus.RemoveComments();
            }
            if (options.ToLowerCase)
            {
                corpus.ToLowerCase();
            }
            return corpus;
        }
    }
}