using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XYZ_Bank.Helpers;

namespace AirIndia.Utilities
{
    internal class ExcelUtils
    {
        public static List<CustomerData> ReadCustomerData(string excelFilePath, string sheetName)
        {
            List<CustomerData> customerDataList = new List<CustomerData>();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            using (var stream = new FileStream(excelFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = true,
                        }
                    });

                    var dataTable = result.Tables[sheetName];

                    if (dataTable != null)
                    {
                        foreach (DataRow row in dataTable.Rows)
                        {
                            CustomerData customerData = new CustomerData
                            {
                                FirstName = GetValueOrDefault(row,"firstName"),
                                LastName = GetValueOrDefault(row, "lastName"),
                                PostCode = GetValueOrDefault(row, "postCode"),
                                Currency = GetValueOrDefault(row, "currency")
                            };

                            customerDataList.Add(customerData);
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Sheet '{sheetName}' not found in the Excel file.");
                    }
                }
            }

            return customerDataList;
        }

        static string GetValueOrDefault(DataRow row, string columnName)
        {
            Console.WriteLine(row + "  " + columnName);
            return row.Table.Columns.Contains(columnName) ? row[columnName]?.ToString() : null;
        }
    }
}

