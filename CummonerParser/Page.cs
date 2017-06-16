using System;
using AngleSharp.Parser.Html;

namespace CummonerParser
{
    internal class Page
    {
        public string Name { get; set; }

        public string Url { get; set; }

        public string GetImageUrl()
        {
            var html = HtmlHelper.LoadHtml(Url);
            var parser = new HtmlParser();
            var document = parser.Parse(html);

            try
            {
                return document.QuerySelector("#comic > a > img").Attributes["src"].Value;
            }
            catch (Exception)
            {
                return document.QuerySelector("#comic > img").Attributes["src"].Value;
            }
        }
    }
}