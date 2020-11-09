using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ReadAndDisplaySalesData.Models;



namespace ReadAndDisplaySalesData.Controllers
{
    public class SalesController : Controller
    {
        private ILoggerFactory _loggerFactory;

        public SalesController(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
        }

        [HttpGet]
        public IActionResult Index(List<vehicalSalesData> salesDataList = null, string mostOftenSoldVehicle = null)
        {
            salesDataList = salesDataList == null ? new List<vehicalSalesData>() : salesDataList;
            ViewBag.MostOftenSoldVehicle = mostOftenSoldVehicle;
            var logger = _loggerFactory.CreateLogger("Info");
            logger.LogDebug("display file");
            return View(salesDataList);
        }
        [HttpPost]
        public IActionResult Index(IFormFile file, [FromServices] IHostingEnvironment hostingEnvironment)
        {
            string fileName = $"{hostingEnvironment.WebRootPath}\\files\\{file.FileName}";
            using (FileStream fileStream = System.IO.File.Create(fileName))
            {
                file.CopyTo(fileStream);
                fileStream.Flush();
            }

            var logger = _loggerFactory.CreateLogger("Info");
            logger.LogDebug("upload file");

            var salesData = Helper.GetSalesDataList(file.FileName);

            return Index(salesData.VehicalSalesDataList, salesData.MostOftenSoldVehicle);

        }
        
    }
}