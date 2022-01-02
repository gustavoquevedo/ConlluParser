using System.Collections.Generic;
using System.Linq;

namespace ConlluParser.Models
{
    public class ConlluSentence
    {
        public ConlluSentence()
        {
            Comments = new List<string>();
            Lines = new LinkedList<ConlluLine>();
        }

        public IList<string> Comments { get; private set; }
        public LinkedList<ConlluLine> Lines { get; private set; }

        public IEnumerable<ConlluKeyValue> CommentsParsed
        {
            get
            {
                return Comments
                    .Where(c => ConlluKeyValue.CheckValid(c))
                    .Select(c => ConlluKeyValue.Create(c));
            }
        }

        public void RemoveLines(ConlluParserParams options)
        {
            var node = Lines.First;
            while (node != null)
            {
                if (options.RemovePunctuation && node.Value.IsPunctuation)
                {
                    node = DeleteNode(node);
                }
                else if (options.RemoveRangeLines && node.Value.IsRange)
                {
                    node = DeleteNode(node);
                }
                else if (options.RemoveSubindexLines && node.Value.IsSubindex)
                {
                    node = DeleteNode(node);
                }
                else
                {
                    node = node.Next;
                }
                
            }
        }

        public void ToLowerCase()
        {
            foreach(var line in Lines)
            {
                line.Form = line.Form.ToLowerInvariant();
            }
        }

        public void RemoveComments()
        {
            Comments = new List<string>();
        }

        private LinkedListNode<ConlluLine> DeleteNode(
            LinkedListNode<ConlluLine> lineNode, bool reindex = true)
        {
            var nextNode = lineNode.Next;
            if (reindex && lineNode.Value.Id.HasValue)
            {
                Reindex(lineNode.Value.Id.Value);
            }
            Lines.Remove(lineNode);
            return nextNode;
        }

        private void Reindex(int idToBeDeleted)
        {
            for (var node = Lines.First; node != null; node = node.Next)
            {
                if (node.Value.Id > idToBeDeleted)
                {
                    node.Value.DecreaseId();
                }
                if (node.Value.Head == idToBeDeleted)
                {
                    node.Value.HeadStr = "0";
                }
                else if (node.Value.Head > idToBeDeleted)
                {
                    node.Value.DecreaseHead();
                }
            }
        }

        public override string ToString()
        {
            var linesStr = Lines.Select(l => l.ToString());
            var content = Comments.Concat(linesStr);
            return string.Join("\r\n", content);
        }
    }
}