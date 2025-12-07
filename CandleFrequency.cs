using ttm.schwab.api.enumerations;

namespace ttm.schwab
{
    public enum CandleFrequency
    {
        Minutes01,
        Minutes05,
        Minutes15,
        Minutes30,
        Daily,
        Weekly
    }

    internal static partial class TRANSLATE
    {
        //public static CandleFrequency Translate(CANDLE_FREQUENCY candleFrequency)
        //{
        //    return candleFrequency switch
        //    {
        //        CANDLE_FREQUENCY.Minutes01 => CandleFrequency.Minutes01,
        //        CANDLE_FREQUENCY.Minutes05 => CandleFrequency.Minutes05,
        //        CANDLE_FREQUENCY.Minutes15 => CandleFrequency.Minutes15,
        //        CANDLE_FREQUENCY.Minutes30 => CandleFrequency.Minutes30,
        //        CANDLE_FREQUENCY.Daily => CandleFrequency.Daily,
        //        CANDLE_FREQUENCY.Weekly => CandleFrequency.Weekly,
        //        _ => CandleFrequency.Daily,
        //    };
        //}

        //public static CANDLE_FREQUENCY Translate(CandleFrequency candleFrequency)
        //{
        //    return candleFrequency switch
        //    {
        //        CandleFrequency.Minutes01 => CANDLE_FREQUENCY.Minutes01,
        //        CandleFrequency.Minutes05 => CANDLE_FREQUENCY.Minutes05,
        //        CandleFrequency.Minutes15 => CANDLE_FREQUENCY.Minutes15,
        //        CandleFrequency.Minutes30 => CANDLE_FREQUENCY.Minutes30,
        //        CandleFrequency.Daily => CANDLE_FREQUENCY.Daily,
        //        CandleFrequency.Weekly => CANDLE_FREQUENCY.Weekly,
        //        _ => CANDLE_FREQUENCY.Daily,
        //    };
        //}
    }
}
