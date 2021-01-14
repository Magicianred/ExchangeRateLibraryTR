using ExchangeRate.API.Model;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace ExchangeRate.API.Interfaces
{
    public interface IExchangeRateApiBusiness
    {
        Task<List<Currency>> GetAllList();
        Task<DataTable> GetAllDataTable();
        Task<List<Currency>> SortBy(bool isAscending, bool CurrencyName = false, bool BanknoteBuying = false, bool BanknoteSelling = false, bool CurrencyCode = false, bool Isim = false);
        Task<List<Currency>> FilterBy(string CurrencyCode = null, string CurrencyName = null, string BanknoteBuying = null, string BanknoteSelling = null);
        Task<bool> Export(List<Currency> currency, string path);
        Task<bool> FilterByAndExport(string path, string CurrencyCode = null, string CurrencyName = null, string BanknoteBuying = null, string BanknoteSelling = null);
        Task<bool> SortByAndExport(string path, bool isAscending, bool CurrencyName = false, bool BanknoteBuying = false, bool BanknoteSelling = false, bool CurrencyCode = false, bool Isim = false);
    }
}
