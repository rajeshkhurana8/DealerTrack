using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.Text.RegularExpressions;
using ReadAndDisplaySalesData.Models;
using System.IO;

namespace ReadAndDisplaySalesData
{
    public static class Helper
    {
        /// <summary>
        /// This method parse the csv file and find most often vehicle sold
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns>VehicalSalesDataModel</returns>
        public static VehicalSalesDataModel GetSalesDataList(string fileName)
        {
            List<vehicalSalesData> salesDataList = new List<vehicalSalesData>();
            var set = new Dictionary<string, int>();
            var filePath = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\files"}" + "\\" + fileName;
            var rows = File.ReadAllLines(filePath, Encoding.Default).ToList();

            foreach (var row in rows.Skip(1))
            {
                
                MatchCollection columns = new Regex("((?<=\")[^\"]*(?=\"(,|$)+)|(?<=,|^)[^,\"]*(?=,|$))").Matches(row);
                salesDataList.Add(new vehicalSalesData()
                {
                    DealNumber = columns[0].ToString(),
                    CustomerName = columns[1].ToString(),
                    DealershipName = columns[2].ToString(),
                    Vehicle = columns[3].ToString(),
                    Price = "CAD$" + columns[4].ToString(),
                    Date = columns[5].ToString()
                });

                // add into dictionary to find most often vehicle sold
                if (set.ContainsKey(columns[3].ToString()))
                {
                    int count;

                    set.TryGetValue(columns[3].ToString(), out count);
                    set[columns[3].ToString()] = count + 1;
                }
                else
                {
                    set.Add(columns[3].ToString(), 1);
                }

            }



            return new VehicalSalesDataModel {
            VehicalSalesDataList = salesDataList,
            MostOftenSoldVehicle = set.OrderByDescending(x => x.Value).FirstOrDefault().Key
        };
        }
    }
}
