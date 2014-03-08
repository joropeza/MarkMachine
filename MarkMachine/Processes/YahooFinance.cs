using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using System.Text;
using MarketMachineCore.Data;

namespace MarketMachineClassLibrary
{

    public class QuickQuote
    {
        public DateTime QuoteDateTime { get; set; }
        public string Symbol { get; set; }
        public decimal Quote { get; set; }
        public decimal OpenPrice { get; set; }
        public decimal HighPrice { get; set; }
        public decimal LowPrice { get; set; }
        public Int64 Volume { get; set; }
        public string StockName { get; set; }
        public string MCText { get; set; }
        public decimal MarketCap { get; set; }


    }

    

    public class YahooFinance
    {
        
        public List<DailyCandle> GetHistoricalData(DateTime Beg, DateTime Endie, Market m)
        {
            
            //http://ichart.yahoo.com/table.csv?s=BAS.DE&a=0&b=1&c=2000&d=0&e=31&f=2010&g=w&ignore=.csv

            List<DailyCandle> qt = new List<DailyCandle>();
            string tester = "";

            int a = Beg.Month - 1;
            int b = Beg.Day;
            int c = Beg.Year;

            int d = Endie.Month - 1;
            int e = Endie.Day;
            int f = Endie.Year;
                       

            string yahooURL = @"http://ichart.yahoo.com/table.csv?s=" + m.Symbol + "&a=" + a.ToString() + "&b=" + b.ToString() + "&c=" + c.ToString() + "&d=" + d.ToString() + "&e=" + e.ToString() + "&f=" + f.ToString() + "&g=d&ignore=.csv";

            // Initialize a new WebRequest.
            HttpWebRequest webreq = (HttpWebRequest)WebRequest.Create(yahooURL);
            // Get the response from the Internet resource.
            HttpWebResponse webresp = (HttpWebResponse)webreq.GetResponse();
            // Read the body of the response from the server.
            StreamReader strm =
              new StreamReader(webresp.GetResponseStream(), Encoding.ASCII);

            String content = content = strm.ReadLine();
            while ((content = strm.ReadLine()) != null)
            {
                content = content.Replace("\"", "");
                string[] contents = content.ToString().Split(',');
                tester = tester + contents[4] + " - ";
                DailyCandle td = new DailyCandle();
                td.MarketId = m.MarketId;
                td.Date = DateTime.Parse(contents[0]);

                td.Open = decimal.Parse(contents[1]);
                td.High = decimal.Parse(contents[2]);
                td.Low = decimal.Parse(contents[3]);
                td.Close = decimal.Parse(contents[4]);
                try
                {
                    td.Volume = int.Parse(contents[5]);
                }
                catch
                {
                    td.Volume = 0;
                }
                qt.Add(td);

            }
            strm.Close();

            

            return qt;
                

        }

         

        public List<QuickQuote> GetQuote(List<string> symbols)
        {
            // Set the return class

            List<QuickQuote> qt = new List<QuickQuote>();

            //can fill in historical data like this:
            //http://ichart.finance.yahoo.com/table.csv?s=SPY&a=01&b=01&c=2012&d=07&e=15&f=2012&g=d&ignore=.csv

           
            
                string urlList = string.Join(",", symbols);

                // Use Yahoo finance service to download stock data from Yahoo
                string yahooURL = @"http://download.finance.yahoo.com/d/quotes.csv?s=" +
                                  urlList + "&f=sl1ohgvj1n";
                
                // Initialize a new WebRequest.
                HttpWebRequest webreq = (HttpWebRequest)WebRequest.Create(yahooURL);
                // Get the response from the Internet resource.
                HttpWebResponse webresp = (HttpWebResponse)webreq.GetResponse();
                // Read the body of the response from the server.
                StreamReader strm =
                  new StreamReader(webresp.GetResponseStream(), Encoding.ASCII);

                // Construct a XML in string format.
                //string tmp = "<stockquotes />";
                string content = "";
                foreach (string Quote in symbols)
                {
                    

                    content = strm.ReadLine().Replace("\"", "");
                    string[] contents = content.ToString().Split(',');
                    // If contents[2] = "N/A". the stock symbol is invalid.
                    if (contents[2] == "N/A")
                    {


                    }
                    else
                    {

                        QuickQuote q = new QuickQuote();
                        q.Symbol = contents[0];
                        q.Quote = decimal.Parse(contents[1]);
                        q.OpenPrice = decimal.Parse(contents[2]);
                        q.HighPrice = decimal.Parse(contents[3]);
                        q.LowPrice = decimal.Parse(contents[4]);
                        q.Volume = Int64.Parse(contents[5]);
                        q.StockName = contents[7];
                        q.MCText = (contents[6]);


                        if (q.MCText.EndsWith("B"))
                        {
                            q.MarketCap = decimal.Parse(q.MCText.TrimEnd('B')) * 1000;
                        }
                        if (q.MCText.EndsWith("M"))
                        {
                            q.MarketCap = decimal.Parse(q.MCText.TrimEnd('M')) * 1;
                        }
                    

                        qt.Add(q);



                            /*

                        //construct XML via strings.
                        tmp += "<Stock>";
                        tmp += "<Symbol>" + contents[0] + "</Symbol>";
                        try
                        {
                            tmp += "<Last>" +
                              String.Format("{0:c}", Convert.ToDouble(contents[1])) +
                                            "</Last>";
                        }
                        catch
                        {
                            tmp += "<Last>" + contents[1] + "</Last>";
                        }
                        tmp += "<Date>" + contents[2] + "</Date>";
                        tmp += "<Time>" + contents[3] + "</Time>";
                        // "<" and ">" are illegal in XML elements.
                        // Replace the characters "<" and ">"
                        // to "&gt;" and "&lt;".
                        if (contents[4].Trim().Substring(0, 1) == "-")
                            tmp += "<Change>&lt;span style='color:red'&gt;" +
                                   contents[4] + "(" + contents[10] + ")" +
                                   "&lt;span&gt;</Change>";
                        else if (contents[4].Trim().Substring(0, 1) == "+")
                            tmp += "<Change>&lt;span style='color:green'&gt;" +
                                   contents[4] + "(" + contents[10] + ")" +
                                   "&lt;span&gt;</Change>";
                        else
                            tmp += "<Change>" + contents[4] + "(" +
                                   contents[10] + ")" + "</Change>";
                        tmp += "<High>" + contents[5] + "</High>";
                        tmp += "<Low>" + contents[6] + "</Low>";
                        try
                        {
                            tmp += "<Volume>" + String.Format("{0:0,0}",
                                   Convert.ToInt64(contents[7])) + "</Volume>";
                        }
                        catch
                        {
                            tmp += "<Volume>" + contents[7] + "</Volume>";
                        }
                        tmp += "<Bid>" + contents[8] + "</Bid>";
                        tmp += "<Ask>" + contents[9] + "</Ask>";
                        tmp += "</Stock>";
                              
                              
                             */ 
                    }
                    // Set the return string
                   
                }
                // Set the return string
               
                // Close the StreamReader object.
                strm.Close();
           
            // Return the stock quote data in XML format.
            return qt;
        }        


    }


}