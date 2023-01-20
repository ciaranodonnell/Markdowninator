using System.Text;

namespace MDDG.Core
{
    public static class CodeFormatter
    {


        /// <summary>
        /// Reads the whitespace from the beginning of the first line in the 
        /// code and removes that from the beginning of every line
        /// </summary>
        /// <param name="originalCode">The code to format</param>
        /// <returns>The same code but with the excess indentation removed.</returns>
        public static string RemoveExcessIdentation(this string originalCode)
        {
            if (string.IsNullOrWhiteSpace(originalCode)) return originalCode;

            StringReader reader = new StringReader(originalCode);
            string? currentLine;
            string initialWhiteSpace = GetInitialWhitespace(originalCode);
            int wsLength = initialWhiteSpace.Length;

            //There is no indentation on the first line so just return
            if (wsLength == 0) return originalCode;

            StringBuilder output = new();
            while ((currentLine = reader.ReadLine()!) != null)
            {
                if (currentLine.StartsWith(initialWhiteSpace))
                {
                    if (initialWhiteSpace.Length == currentLine.Length)
                    {
                        output.AppendLine();
                    }
                    else
                    {
                        output.Append(currentLine, wsLength, currentLine.Length - wsLength).AppendLine();
                    }
                }
                else
                {
                    output.AppendLine(currentLine);
                }
            }
            if (!originalCode.EndsWith(System.Environment.NewLine))
                return output.ToString(0, output.Length - Environment.NewLine.Length);
            return output.ToString();
        }

        internal static string GetInitialWhitespace(this string originalCode)
        {
            StringBuilder sb = new();
            foreach (var c in originalCode)
            {
                if (c == '\n' || c == '\r') break;
                if (char.IsWhiteSpace(c))
                {
                    sb.Append(c);
                }
                else
                {
                    break;
                }
            }
            return sb.ToString();
        }
    }
}