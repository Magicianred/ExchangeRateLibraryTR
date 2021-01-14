using ExchangeRate.API.Business.ExportService;
using ExchangeRate.API.Interfaces;
using ExchangeRate.API.Model.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExchangeRate.API.Business
{
    public static class ExportFactory
    {
        public static IExportService GetService (ApiExportExtensions type)
        {
            switch (type)
            {
                case ApiExportExtensions.Csv:
                    return new CsvExport();
                case ApiExportExtensions.Excel:
                    return new ExcelExport();
            }
            return null;
        }
    }
}
