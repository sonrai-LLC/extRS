namespace ExtRS.ReferenceData.Tiingo;

public class TiingoTicker
{
    public DateTime date { get; set; }
    public decimal close { get; set; }
    public decimal high { get; set; }
    public decimal low { get; set; }
    public decimal open { get; set; }
    public long volume { get; set; }
    //decimal AdjClose { get; set; }
    //decimal AdjLow { get; set; }
    //decimal AdjOpen { get; set; }
    //long AdjVolume { get; set; }
    //long DivCash { get; set; }
    //int SplitFactor { get; set; }

    //"close": 58.46,
    //"high": 59,
    //"low": 58.29,
    //"open": 58.96,
    //"volume": 3432099,
    //"adjClose": 58.46,
    //"adjHigh": 59,
    //"adjLow": 58.29,
    //"adjOpen": 58.96,
    //"adjVolume": 3432099,
    //"divCash": 0,
    //"splitFactor": 1
}

//SELECT TOP(1000) [Id]
//      ,[Ticker]
//      ,[Name]
//      ,[OneDay]
//      ,[OneWeek]
//      ,[OneMonth]
//      ,[OneYear]
//      ,[FiveYear]
//      ,[AllTime]
//FROM[ReferenceData].[dbo].[MAJOR_TICKERS]
