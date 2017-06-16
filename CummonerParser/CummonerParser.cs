using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AngleSharp.Parser.Html;

namespace CummonerParser
{
    internal class CummonerParser
    {
        private readonly string _sourceUrl = "http://www.totempole666.com/archives-2/";
        private readonly string _currentPath = AppContext.BaseDirectory;


        public void ParseCummoner()
        {
            var html = HtmlHelper.LoadHtml(_sourceUrl);
            var chapters = GetChapters(html).ToList();
            
            var chapterCounter = 1;
            foreach (var chapter in chapters)
            {
                Console.WriteLine("Chapter : " + chapterCounter++);

                SaveChapter(chapter);
            }
        }

        private IEnumerable<Chapter> GetChapters(string html)
        {
            var parser = new HtmlParser();
            var document = parser.Parse(html);

            foreach (var chapterElement in document.QuerySelectorAll(".comic-archive-chapter-wrap"))
            {
                var chapter = new Chapter
                {
                    ChapterName = chapterElement.QuerySelector("h3").TextContent,
                    Pages = chapterElement.QuerySelectorAll(".comic-archive-title > a")
                        .Select(x => new Page { Name = x.TextContent, Url = x.Attributes["href"].Value })
                        .ToList()
                };

                yield return chapter;
            }
        }

        private void SaveChapter(Chapter chapter)
        {
            var di = new DirectoryInfo(Path.Combine(_currentPath, chapter.ChapterName.EscapePath()));

            if (!di.Exists)
            {
                di.Create();
            }

            var pageCounter = 1;
            foreach (var chapterPage in chapter.Pages)
            {
                if (!SavePage(chapterPage, di))
                    continue;

                Console.WriteLine("Page : " + pageCounter++);
            }
        }

        private bool SavePage(Page chapterPage, DirectoryInfo chapterDirectory)
        {
            var image = HtmlHelper.DownloadFile(chapterPage.GetImageUrl());

            var fileName = Path.Combine(chapterDirectory.FullName, chapterPage.Name.EscapePath() + ".jpg");

            if (File.Exists(fileName))
                return false;

            using (var fs = new FileStream(fileName, FileMode.Create))
            using (var bw = new BinaryWriter(fs))
            {
                bw.Write(image);
            }
            return true;
        }
    }
}