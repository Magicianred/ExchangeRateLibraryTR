namespace ExchangeRate.API.Model
{
    public class Currency
    {
        public string Isim { get; set; }
        public string CurrencyName { get; set; }
        public string ForexBuying { get; set; }
        public string ForexSelling { get; set; }
        public string BanknoteBuying { get; set; }
        public string BanknoteSelling { get; set; }
        public string CrossRateUSD { get; set; }
        public string Kod { get; set; }
        public string CurrencyCode { get; set; }
    }

}
