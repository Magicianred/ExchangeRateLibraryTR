using ExchangeRate.API.Interfaces;
using System;
using System.Data;
using System.IO;
using System.Threading.Tasks;

namespace ExchangeRate.API.Business.ExportService
{
    public class CsvExport : IExportService
    {
        public async Task<bool> Export(DataTable table, string savePath)
        {
            try
            {
                StreamWriter sw = new StreamWriter(savePath, false);
                //headers    
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    sw.Write(table.Columns[i]);
                    if (i < table.Columns.Count - 1)
                    {
                        sw.Write(",");
                    }
                }
                sw.Write(sw.NewLine);
                foreach (DataRow dr in table.Rows)
                {
                    for (int i = 0; i < table.Columns.Count; i++)
                    {
                        if (!Convert.IsDBNull(dr[i]))
                        {
                            string value = dr[i].ToString();
                            if (value.Contains(','))
                            {
                                value = String.Format("\"{0}\"", value);
                                sw.Write(value);
                            }
                            else
                            {
                                sw.Write(dr[i].ToString());
                            }
                        }
                        if (i < table.Columns.Count - 1)
                        {
                            sw.Write(",");
                        }
                    }
                    sw.Write(sw.NewLine);
                }
                sw.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
