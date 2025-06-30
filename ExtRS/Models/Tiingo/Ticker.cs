namespace Sonrai.ExtRS.Models.Tiingo;

public class Ticker
{
    public decimal AdjClose { get; set; }
    public decimal AdjHigh { get; set; }
    public decimal AdjLow { get; set; }
    public decimal AdjOpen { get; set; }
    public long AdjVolume { get; set; }
    public decimal Close { get; set; }
    public DateTime Date { get; set; }
    public long DivCash { get; set; }
    public decimal High { get; set; }
    public decimal Low { get; set; }
    public decimal Open { get; set; }
    public string? SplitFactor { get; set; }
    public long Volume { get; set; }
}
