using HtmlAgilityPack;
using System;
using System.Net.Http;
using System.Runtime.CompilerServices;

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
        
            if (touchdownTable != null)
            {
                int rowCount = 1;

                foreach (var row in touchdownTable)
                {
                    var name = row.SelectSingleNode(".//a[@class='AnchorLink']");
                    var team = row.SelectSingleNode(".//span[@class='pl2 ns10 athleteCell__teamAbbrev']");
                    var columns = row.SelectNodes(".//tbody[@class='Table__TBODY']");

                        if (name != null && team != null)
                        {
                            var players = name.InnerText.Trim();        
                            var playersTeams = team.InnerText.Trim();;
                            Console.WriteLine($"RK: {rowCount} NAME: {players} TEAM: {playersTeams}");
                     
                            rowCount++;

                            if (rowCount >= 51)
                                {
                                    break;
                                }                
                        }
                        else
                        {
                            Console.WriteLine("Information could not be accessed.");
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
