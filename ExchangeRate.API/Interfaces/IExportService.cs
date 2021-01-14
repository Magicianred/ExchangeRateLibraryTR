using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRate.API.Interfaces
{
    public interface IExportService
    {
        Task<bool> Export(DataTable table, string savePath);
    }
}
