using HtmlAgilityPack;
using System;
using System.Net.Http;

namespace WebScraper
{
    class Program
    {
        static void Main(string[] args)
        {
            string url = "https://www.espn.co.uk/nfl/stats/player/_/view/scoring/table/scoring/sort/totalTouchdowns/dir/desc";
            var httpClient = new HttpClient();
            var html = httpClient.GetStringAsync(url).Result;
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            var touchdownTable = htmlDocument.DocumentNode.SelectNodes("//tr[@class='Table__TR Table__TR--sm Table__even']");
            var playerElements = htmlDocument.DocumentNode.SelectNodes(".//div[@class='athleteCell__flag flex items-start mr7']");
            
            if (touchdownTable != null)
            {
                foreach (var row in touchdownTable)
                {
                    var columns = row.SelectNodes(".//td[@class='Table__TD']");

                    if (columns != null && columns.Count >= 5)
                    {
                        var touchdowns = columns[4].InnerText.Trim();
                        Console.WriteLine($"Touchdowns: {touchdowns} ");
                    }
                }
            }
            else
            {
                Console.WriteLine("Table not found.");
            }


            if (playerElements != null)
            {
                foreach (var row in playerElements)
                {
                    var name = row.SelectSingleNode(".//a[@class='AnchorLink']");
                    var team = row.SelectSingleNode(".//span[@class='pl2 ns10 athleteCell__teamAbbrev']");
                    if (name != null || team != null)
                    {
                        var players = name.InnerText.Trim();
                        var playersTeams = team.InnerText.Trim();
                        Console.WriteLine($"Team: {playersTeams}, Player: {players}");
                    }
                }
            }
            else
            {
                Console.WriteLine("Table not found.");
            }
   
                
            
        }
    }
}
