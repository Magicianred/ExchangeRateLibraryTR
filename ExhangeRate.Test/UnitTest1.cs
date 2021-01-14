using NUnit.Framework;
using ExchangeRate.API.Business;
using System.Linq;
using ExchangeRate.API.Model;
using ExchangeRate.API.Interfaces;
using Moq;
using System.Collections.Generic;
using ExchangeRate.API.API;
using System.Threading.Tasks;
using System;

namespace ExhangeRate.Test
{

    public class Tests
    {
        Mock myInterfaceMock;
        public Tests()
        {
        }

        [Test]
        public void GetCurrenceyTurkeyAllToday_Test()
        {
            List<Currency> curencyList = new List<Currency>();
            ExchangeRateApiBusiness business = new ExchangeRateApiBusiness();
            curencyList = business.GetAllList().GetAwaiter().GetResult().ToList();
            Assert.True(curencyList.Count > 0);
        }
        [Test]
        public void SortTest()
        {
            ExchangeRateApiBusiness business = new ExchangeRateApiBusiness();
            var list = business.SortBy(false, true).GetAwaiter().GetResult().ToList();
                //business.GetCurrenciesSorted("",true).GetAwaiter().GetResult();
            Assert.True(list.Count > 0);
        }

        [Test]
        public void FilterTest()
        {
            ExchangeRateApiBusiness business = new ExchangeRateApiBusiness();
            var list = business.FilterBy("USD").GetAwaiter().GetResult().ToList();
            Assert.True(list.Count > 0);
        }

        [Test]
        public void ExportTest()
        {
            string pathExport = "ExchangeRate4.xlsx";
            ExchangeRateApi api = new ExchangeRateApi();
            string path = AppDomain.CurrentDomain.BaseDirectory + pathExport;
            var list =  api.SortByAndExport(path, false,true).GetAwaiter().GetResult();
            Assert.True(list);
        }
    }
}