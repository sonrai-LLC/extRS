namespace Sonrai.ExtRS.Models.Tiingo;

public class Ticker
{
    public DateTime Date { get; set; }
    public decimal Close { get; set; }
    public decimal High { get; set; }
    public decimal Low { get; set; }
    public decimal Open { get; set; }
    public long Volume { get; set; }
    decimal AdjClose { get; set; }
    decimal AdjLow { get; set; }
    decimal AdjOpen { get; set; }
    long AdjVolume { get; set; }
    long DivCash { get; set; }
    int SplitFactor { get; set; }
}
