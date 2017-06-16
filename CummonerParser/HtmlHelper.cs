using System;
using System.Net.Http;

namespace CummonerParser
{
    internal class HtmlHelper
    {
        private static readonly HttpClient _httpClient = new HttpClient();

        public static string LoadHtml(string url) => _httpClient
                .GetAsync(new Uri(url))
                .Result
                .Content
                .ReadAsStringAsync()
                .Result;

        public static byte[] DownloadFile(string url)=> _httpClient
            .GetAsync(new Uri(url))
            .Result
            .Content
            .ReadAsByteArrayAsync()
            .Result;
    }
}