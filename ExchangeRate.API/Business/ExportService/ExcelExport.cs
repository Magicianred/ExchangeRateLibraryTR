using ExchangeRate.API.Interfaces;
using OfficeOpenXml;
using System.Data;
using System.IO;
using System.Threading.Tasks;

namespace ExchangeRate.API.Business.ExportService
{
    public class ExcelExport : IExportService
    {
        public async Task<bool> Export   (DataTable table, string savePath)
        {
            try
            {
                ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                using (ExcelPackage pck = new ExcelPackage())
                {
                    var ws = pck.Workbook.Worksheets.Add("Worksheet-Name");
                    ws.Cells["A1"].LoadFromDataTable(table, true, OfficeOpenXml.Table.TableStyles.Medium1);
                    using (var fileStream = File.Create(savePath))
                        pck.SaveAs(fileStream);
                    pck.Dispose();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
