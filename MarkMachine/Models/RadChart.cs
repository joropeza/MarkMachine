using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Drawing;
using System.Drawing.Imaging;

using MarketMachineCore.Data;

namespace MarkMachine.Models
{
    public class RadChart
    {
        public Byte[] ChartData { get; set; }
        public Image ChartImage { get; set; }
        public string ChartTitle { get; set; }


        public void CreateChart(int MarketId = 1)
        {
            Telerik.Web.UI.RadChart objChart = new Telerik.Web.UI.RadChart();



            // the data we want to chart

            // setting some chart parameters

            objChart.Height = 480;
            objChart.Width = 500;
            /*objChart.Margins.Right = new Unit(30, UnitType.Percentage);
            objChart.Margins.Bottom = new Unit(1, UnitType.Percentage);
            objChart.Margins.Left = new Unit(1, UnitType.Percentage);
            objChart.Margins.Top = new Unit(1, UnitType.Percentage);
            objChart.Background.MainColor = Color.White;
            objChart.Background.BorderColor = Color.White;*/

            // creating a series and adding it to the chart

            marketdbEntities dba = new marketdbEntities();

            Market m = dba.Markets.Find(MarketId);

            DateTime sd = DateTime.Now.AddMonths(-6);
            DateTime ed = DateTime.Now;

            Telerik.Charting.ChartSeriesItemsCollection sc = new Telerik.Charting.ChartSeriesItemsCollection();

            //m.PopulateDailyCandleList(DateTime.Now.AddYears(-4), ed);

            //MarketMachineClassLibrary.MarketDailyCandleList mdcl = m.mdcl;
            //MarketMachineClassLibrary.CandleList cl = mdcl.cl;

            int x = 0;
            double minny = 100000;
            double maxxy = 0;

            foreach (var t in m.DailyCandles.Where(i => i.Date >= sd))
            {
                Telerik.Charting.ChartSeriesItem i = new Telerik.Charting.ChartSeriesItem();

                i.XValue = t.Date.ToOADate();
                i.YValue = Double.Parse(t.Open.ToString());
                i.YValue2 = Double.Parse(t.Close.ToString());
                i.YValue3 = Double.Parse(t.Low.ToString());
                i.YValue4 = Double.Parse(t.High.ToString());
                sc.Add(i);

                if (Double.Parse(t.High.ToString()) > maxxy)
                {
                    maxxy = Double.Parse(t.High.ToString());
                }

                if (Double.Parse(t.Low.ToString()) < minny)
                {
                    minny = Double.Parse(t.Low.ToString());
                }

                x++;
            }


            Double offset = (maxxy - minny) * .2;


            objChart.PlotArea.YAxis.MaxValue = maxxy + offset;

            objChart.PlotArea.YAxis.MinValue = Math.Round(minny - offset, MidpointRounding.AwayFromZero);

            objChart.PlotArea.YAxis.LabelStep = 10;

            objChart.PlotArea.XAxis.AddRange(sd.ToOADate(), ed.ToOADate(), 1);
            objChart.Skin = "LightGreen";


            objChart.Legend.Appearance.Visible = false;


            objChart.Series.Add(new Telerik.Charting.ChartSeries());
            objChart.Series[0].Items.AddRange(sc);
            objChart.Series[0].Name = m.Name;
            objChart.Series[0].Type = Telerik.Charting.ChartSeriesType.CandleStick;
            objChart.Series[0].Appearance.BarWidthPercent = 80;

            objChart.Series[0].Appearance.Border.Color = Color.Black;
            objChart.Series[0].Appearance.Border.Width = 1;
            objChart.Series[0].Appearance.LabelAppearance.Visible = false;

            objChart.Series[0].PlotArea.XAxis.LayoutMode = Telerik.Charting.Styles.ChartAxisLayoutMode.Between;
            objChart.Series[0].PlotArea.XAxis.AutoScale = false;
            objChart.Series[0].PlotArea.XAxis.Appearance.MajorGridLines.Width = 2;
            objChart.Series[0].PlotArea.XAxis.Appearance.MinorGridLines.Visible = false;
            objChart.Series[0].PlotArea.XAxis.LabelStep = 10;

            objChart.Series[0].PlotArea.XAxis.Appearance.ValueFormat = Telerik.Charting.Styles.ChartValueFormat.ShortDate;
            objChart.Series[0].PlotArea.XAxis.Appearance.LabelAppearance.RotationAngle = 45;
            objChart.Series[0].PlotArea.XAxis.Appearance.LabelAppearance.Position.AlignedPosition = Telerik.Charting.Styles.AlignedPositions.Top;



            objChart.Series[0].PlotArea.YAxis.AxisMode = Telerik.Charting.ChartYAxisMode.Extended;
            objChart.Series[0].PlotArea.YAxis.AutoScale = false;
            objChart.Series[0].PlotArea.YAxis.Appearance.MajorGridLines.Width = 2;
            objChart.Series[0].PlotArea.YAxis.Appearance.MinorGridLines.Visible = false;
            objChart.ChartTitle.Visible = false;
            objChart.Appearance.Dimensions.Margins.Top = 10;
            objChart.Appearance.Dimensions.Margins.Bottom = 10;
            objChart.Appearance.Dimensions.Margins.Left = 10;
            objChart.Appearance.Dimensions.Margins.Right = 10;


            objChart.Series[0].PlotArea.Appearance.Dimensions.AutoSize = false;
            objChart.Series[0].PlotArea.Appearance.Dimensions.Width = 430;
            objChart.Series[0].PlotArea.Appearance.Dimensions.Height = 430;
            objChart.Series[0].PlotArea.Appearance.Dimensions.Paddings.Top = 10;
            objChart.Series[0].PlotArea.Appearance.Dimensions.Margins.Top = 10;






            //objChart.Text = "Minny: " + minny.ToString() + ", Maxxy: " + maxxy.ToString();

            // send the chart to the view engine

            /****************** TRADING RANGES

            foreach (MarketMachineClassLibrary.TradingRangeViewModel t in mdcl.cl.TheTradingRanges.Where(i => i.BeginDate >= sd).OrderBy(i => i.ActiveDate))
            {
                Telerik.Charting.ChartMarkedZone cz = new Telerik.Charting.ChartMarkedZone();
                cz.ValueStartX = t.BeginDate.ToOADate();
                cz.ValueEndX = t.ActiveDate.ToOADate();
                cz.ValueStartY = double.Parse(t.UpperPrice.ToString());
                cz.ValueEndY = double.Parse(t.LowerPrice.ToString());

                if (t.BullOrBear == "Bull")
                {
                    cz.Appearance.FillStyle.MainColor = System.Drawing.Color.LightGreen;

                }
                else if (t.BullOrBear == "Bear")
                {
                    cz.Appearance.FillStyle.MainColor = System.Drawing.Color.LightPink;
                }
                else
                {
                    cz.Appearance.FillStyle.MainColor = System.Drawing.Color.LightBlue;
                }

                objChart.Chart.PlotArea.MarkedZones.Add(cz);

            }


             */
 
            //return new ChartResult() { Chart = objChart };



            Image image = objChart.GetBitmap();

            ChartImage = image;

            //image.Save("c:\\temp\\test.jpg", ImageFormat.Jpeg);

            byte[] Ret;

            System.IO.MemoryStream ms = new System.IO.MemoryStream();

            image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            Ret = ms.ToArray();

            ChartData = Ret;

        }

    }
}