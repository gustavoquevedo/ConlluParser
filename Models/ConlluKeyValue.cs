namespace ConlluParser.Models
{
    public class ConlluKeyValue
    {
        public string Key { get; set; }
        public string Value { get; set; }

        public static bool CheckValid(string text)
        {
            var parts = text.Split('=');
            return parts.Length == 2 && 
                parts[0].Length > 0 && 
                parts[1].Length > 0;
        }

        public static ConlluKeyValue Create(string text)
        {
            var parts = text.Split('=');
            return new ConlluKeyValue
            {
                Key = parts[0],
                Value = parts[1]
            };
        }
    }
}