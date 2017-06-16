using System.IO;
using System.Text.RegularExpressions;

namespace CummonerParser
{
    internal static class Extensions
    {
        public static string EscapePath(this string path)
        {
            string regexSearch = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
            Regex r = new Regex(string.Format("[{0}]", Regex.Escape(regexSearch)));
            return r.Replace(path, "");
        }
    }
}
