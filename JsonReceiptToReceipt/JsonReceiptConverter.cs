using JsonReceiptToReceipt.Model;

namespace JsonReceiptToReceipt
{
    public class JsonReceiptConverter
    {

        public static Dictionary<int, string> Convert(List<PartOfReceipt> receipts)
        {
            Dictionary<int, string> result = new Dictionary<int, string>();
            receipts = receipts.Where(x => string.IsNullOrEmpty(x.Locale)).OrderBy(x => x.BoundingPoly.Vertices.OrderBy(y => y.Y).ThenBy(y => y.X).Select(z => z.Y).FirstOrDefault()).ToList();

            int line = 1;
            PartOfReceipt before = null;
            for(int i = 0; i < receipts.Count;)
            {
                PartOfReceipt currentPart = receipts[i];
                if (!result.ContainsKey(line))
                {
                    result.Add(line, currentPart.Description);
                    before = currentPart;
                    i++;
                    continue;
                }

                int beforeMinHeight = before.BoundingPoly.Vertices.OrderBy(a => a.Y).First().Y;
                int beforeMaxHeight = before.BoundingPoly.Vertices.OrderByDescending(a => a.Y).First().Y;
                int average = (beforeMinHeight + beforeMaxHeight) / 2;
                int curentPartMinHeight = currentPart.BoundingPoly.Vertices.OrderBy(a => a.Y).First().Y;
                if(average > curentPartMinHeight)
                {
                    result[line] += " " + currentPart.Description;
                    before = currentPart;
                    i++;
                    continue;
                }

                line++;
            }
            return result;
        }

        public static List<string> ReceiptBeautifiler(Dictionary<int, string> parts)
        {
            List<string> lines = new List<string>();
            lines.Add("line | text");
            lines.Add("_______________________________________________");
            foreach (var part in parts)
            {
                int spaceCount = 4 - part.Key.ToString().Length;
                string space = "";
                while(spaceCount > 0)
                {
                    space += " ";
                    spaceCount--;
                }
                string line = space + part.Key + " | " + part.Value;
                lines.Add(line);
            }
            return lines;
        }

    }
}