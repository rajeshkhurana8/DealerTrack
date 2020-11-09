using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReadAndDisplaySalesData.Models
{
    public class VehicalSalesDataModel
    {
        public string MostOftenSoldVehicle { get; set; }

        public List<vehicalSalesData> VehicalSalesDataList { get; set; }
    }
}
