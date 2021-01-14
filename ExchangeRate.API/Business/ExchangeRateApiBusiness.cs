using ExchangeRate.API.Model;
using System.Collections.Generic;
using System.Data;
using System.Xml;
using ExchangeRate.API.Utils;
using System.Threading.Tasks;
using System;
using System.Linq;
using System.Reflection;
using ExchangeRate.API.Model.Enum;
using System.IO;

namespace ExchangeRate.API.Business
{
    public class ExchangeRateApiBusiness
    {
        private const string tcmbUrl = @"http://www.tcmb.gov.tr/kurlar/today.xml";

        public async Task<List<Currency>> GetAllList()
        {
            var currency = await GetAllDataTable();
            if (currency.Rows.Count > 0)
            {
                List<Currency> currencies = await ExchangeRateApiUtils.ConvertDataTableToList<Currency>(currency);
                return currencies;

            }
            return null;
        }
        public async Task<DataTable> GetAllDataTable()
        {
            XmlTextReader rdr = new XmlTextReader(tcmbUrl);
            DataSet ds = new DataSet();
            ds.ReadXml(rdr);
            var currency = ds.Tables["Currency"];
            return currency;
        }
        public async Task<List<Currency>> SortBy(bool isAscending, bool CurrencyName = false, bool BanknoteBuying = false, bool BanknoteSelling = false, bool CurrencyCode = false, bool Isim = false)
        {
            var hack = new { CurrencyName, BanknoteBuying, BanknoteSelling, CurrencyCode, Isim };
            Dictionary<string, bool> sortItems = new Dictionary<string, bool>();
            foreach (PropertyInfo pi in hack.GetType().GetProperties())
            {
                sortItems.Add(pi.Name, Convert.ToBoolean(pi.GetValue(hack, null)));
            }
            var currencies = await GetAllList();
            if (sortItems.Any(i => i.Value))
                return currencies.AsQueryable().OrderBy(string.Join(",", sortItems.Where(i => i.Value).Select(i => i.Key)), isAscending).ToList();
            else
                return currencies;
        }
        public async Task<List<Currency>> FilterBy(string CurrencyCode = null, string CurrencyName = null, string BanknoteBuying = null, string BanknoteSelling = null)
        {
            var hack = new { CurrencyName, BanknoteBuying, BanknoteSelling, CurrencyCode };
            Dictionary<string, string> filterItems = new Dictionary<string, string>();
            filterItems = hack.GetType().GetProperties()
                            .ToDictionary(mc => mc.Name.ToString(),
                                 mc => Convert.ToString(mc.GetValue(hack, null)),
                                 StringComparer.OrdinalIgnoreCase);
            var currencies = await GetAllList();
            var filteredCurrencies = new List<Currency>();
            foreach (var item in filterItems.Where(i => !string.IsNullOrEmpty(i.Value)))
            {
                switch (item.Key)
                {
                    case "CurrencyCode":
                        filteredCurrencies = currencies.Where(i => i.CurrencyCode == item.Value.ToString()).ToList();
                        break;
                    case "CurrencyName":
                        filteredCurrencies = currencies.Where(i => i.CurrencyName == item.Value.ToString()).ToList();
                        break;
                    case "BanknoteBuying":
                        filteredCurrencies = currencies.Where(i => i.BanknoteBuying == item.Value.ToString()).ToList();
                        break;
                    case "BanknoteSelling":
                        filteredCurrencies = currencies.Where(i => i.BanknoteSelling == item.Value.ToString()).ToList();
                        break;
                }
            }
            return filteredCurrencies;
        }
        public async Task<bool> ExportAll(List<Currency> currency, string path)
        {
            var table = currency.ToDataTable();
            try
            {
                Enum.TryParse(Path.GetExtension(path), out ApiExportExtensions extension);
                var exportService = ExportFactory.GetService(extension);
                return await exportService.Export(table, path);
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// The document will be exported according to the extension, 
        /// which must be specified in the path information.  For now only .csv and .xlsx extension formats are supported
        /// </summary>
        /// <param name="path">Example: Example.xlxs</param>
        /// <param name="CurrencyCode"></param>
        /// <param name="CurrencyName"></param>
        /// <param name="BanknoteBuying"></param>
        /// <param name="BanknoteSelling"></param>
        /// <returns></returns>
        public async Task<bool> FilterByAndExport(string path, string CurrencyCode = null, string CurrencyName = null, string BanknoteBuying = null, string BanknoteSelling = null)
        {
            var currency = await FilterBy(CurrencyCode, CurrencyName, BanknoteBuying, BanknoteSelling);
            var table = currency.ToDataTable();
            Enum.TryParse(Path.GetExtension(path), out ApiExportExtensions extension);
            var exportService = ExportFactory.GetService(extension);
            return await exportService.Export(table, path);
        }
        /// <summary>
        /// The document will be exported according to the extension, 
        /// which must be specified in the path information.  For now only .csv and .xlsx extension formats are supported
        /// </summary>
        public async Task<bool> SortByAndExport(string path, bool isAscending, bool CurrencyName = false, bool BanknoteBuying = false, bool BanknoteSelling = false, bool CurrencyCode = false, bool Isim = false)
        {
            var currency = await SortBy(isAscending, CurrencyName, BanknoteBuying, BanknoteSelling, CurrencyCode, Isim);
            var table = currency.ToDataTable();
            Enum.TryParse(Path.GetExtension(path), out ApiExportExtensions extension);
            var exportService = ExportFactory.GetService(extension);
            return await exportService.Export(table, path);
        }
    }
}
