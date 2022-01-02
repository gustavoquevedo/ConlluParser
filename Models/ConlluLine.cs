using System.Collections.Generic;
using System.Linq;

namespace ConlluParser.Models
{
    public class ConlluLine
    {
        public string IdStr { get; set; }
        public string Form { get; set; }
        public string Lemma { get; set; }
        public string Upos { get; set; }
        public string Xpos { get; set; }
        public string Feats { get; set; }        
        public string HeadStr { get; set; }
        public string Deprel { get; set; }
        public string Deps { get; set; }
        public string Misc { get; set; }

        public int? Id => GetId(IdStr);
        public int? Head => GetId(HeadStr);

        private static int? GetId(string idStr)
        {
            //var rangeParts = idStr.Split('-');
            //if (rangeParts.Length == 2)
            //{
            //    return int.Parse(rangeParts[0]);
            //}
            //var subIndexParts = idStr.Split('.');
            //if (subIndexParts.Length == 2)
            //{
            //    return int.Parse(subIndexParts[0]);
            //}
            if (int.TryParse(idStr, out var id))
            {
                return id;
            }
            return null;
        }

        public void DecreaseId() => IdStr = DecreaseId(IdStr);
        public void DecreaseHead() => HeadStr = DecreaseId(HeadStr);

        private static string DecreaseId(string idStr)
        {
            var rangeParts = idStr.Split('-');
            if (rangeParts.Length == 2)
            {
                return $"{int.Parse(rangeParts[0]) - 1}-{int.Parse(rangeParts[1]) - 1}";
            }
            var subIndexParts = idStr.Split('.');
            if (subIndexParts.Length == 2)
            {
                return $"{int.Parse(subIndexParts[0]) - 1}.{int.Parse(subIndexParts[1])}";
            }
            if (int.TryParse(idStr, out var idParsed))
            {
                return (--idParsed).ToString();
            }
            return idStr;
        }

        public IEnumerable<ConlluKeyValue> Features
        {
            get
            {
                if (Feats == "_")
                {
                    return new List<ConlluKeyValue>();
                }
                var parts = Feats.Split('|');
                return parts
                    .Where(p => ConlluKeyValue.CheckValid(p))
                    .Select(p => ConlluKeyValue.Create(p));
            }
        }

        public bool IsPunctuation => Upos == "PUNCT";
        public bool IsSubindex => IdStr.Contains(".");
        public bool IsRange => IdStr.Contains("-");
        public bool IsLowerCaseOnly => Form.ToLower() == Form;

        public override string ToString()
        {
            var content = new string[]
            {
                IdStr,
                Form,
                Lemma,
                Upos,
                Xpos,
                Feats,
                HeadStr,
                Deprel,
                Deps,
                Misc
            };
            return string.Join("\t", content);
        }
    }
}