using ExchangeRate.API.Business;
using ExchangeRate.API.Interfaces;
using ExchangeRate.API.Model;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace ExchangeRate.API.API
{
    public class ExchangeRateApi:IExchangeRateApi
    {
        readonly ExchangeRateApiBusiness business;
        public ExchangeRateApi( )
        {
            business = new ExchangeRateApiBusiness();
        }
        public async Task<List<Currency>> GetAllList()
        {
            return await business.GetAllList();
        }
        public async Task<DataTable> GetAllDataTable()
        {
            return await business.GetAllDataTable();
        }
        public async Task<List<Currency>> SortBy(bool isAscending, bool CurrencyName = false, bool BanknoteBuying = false, bool BanknoteSelling = false, bool CurrencyCode = false, bool Isim = false)
        {
            return await business.SortBy(isAscending, CurrencyName, BanknoteBuying , BanknoteSelling ,CurrencyCode ,Isim);
        }
        public async Task<List<Currency>> FilterBy(string CurrencyCode = null, string CurrencyName = null, string BanknoteBuying = null, string BanknoteSelling = null)
        {
            return await business.FilterBy(CurrencyCode,  CurrencyName, BanknoteBuying, BanknoteSelling);
        }
        public async Task<bool> Export(List<Currency> currency, string path)
        {
            return await business.ExportAll(currency,path);
        }
        public async Task<bool> FilterByAndExport(string path, string CurrencyCode = null, string CurrencyName = null, string BanknoteBuying = null, string BanknoteSelling = null)
        {
            return await business.FilterByAndExport(path, CurrencyCode, CurrencyName, BanknoteBuying, BanknoteSelling);
        }
        public async Task<bool> SortByAndExport(string path, bool isAscending, bool CurrencyName = false, bool BanknoteBuying = false, bool BanknoteSelling = false, bool CurrencyCode = false, bool Isim = false)
        {
            return await business.SortByAndExport(path , isAscending, CurrencyName, BanknoteBuying, BanknoteSelling, CurrencyCode, Isim);
        }
    }
}
