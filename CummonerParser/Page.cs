using System;
using AngleSharp.Html.Parser;

namespace CummonerParser
{
    internal class Page
    {
        public string Name { get; init; }

        public string Url { get; init; }

        public string GetImageUrl()
        {
            var html = HtmlHelper.LoadHtml(Url);
            var parser = new HtmlParser();
            var document = parser.ParseDocument(html);

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