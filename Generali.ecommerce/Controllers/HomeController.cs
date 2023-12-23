using ExcelDataReader;
using Generali.ecommerce.Models;
using Generali.Entity.DTO;
using Generali.Entity.Entities;
using Generali.Service.Services.Abstractions;
using Generali.Service.Services.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.ServiceModel.Channels;

namespace Generali.ecommerce.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;

        private readonly IWebHostEnvironment _hostingEnvironment;
        public HomeController(ILogger<HomeController> logger, IProductService productService, IWebHostEnvironment hostingEnvironment)
        {
            _logger = logger;
            this._productService = productService;
            _hostingEnvironment = hostingEnvironment;   
        }
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllProductAsync();

            List<string>pcode = new List<string>();
            var sameProductsCode = products.Select(a => a.ProductCode.Code).Distinct();
            foreach (var item in sameProductsCode)
            {
                pcode.Add(item);
            }

            List<int> SameProductCount = new List<int>();
            List<int> DistnicPCount = new List<int>();
            var Same = products.GroupBy(x => new { x.ProductCode }).Select(g => new { Key = g.Key, Count = g.Count() });
            foreach (var item in Same)
            {
                SameProductCount.Add(Same.Where(p => p.Key.ProductCode.Code == item.Key.ProductCode.Code).Count());

            }
            var distinctNumbers = SameProductCount.Distinct();
            foreach(var item in distinctNumbers)
            {
                DistnicPCount.Add(item);
            }

            var tupleModel = (products: products, pcode: pcode, DistnicPCount: DistnicPCount);

            return View(tupleModel);
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProductDTO productDTO)
        {
            await _productService.CrateProductAsync(productDTO);
            return RedirectToAction("Add", "Home", new { Area = "" });
        }
        public async Task<IActionResult> Delete(Guid productId)
        {
            await _productService.SafeDeleteAsync(productId);
            return RedirectToAction("Index", "Home", new { Area = "" });
        }
        [HttpGet]
        public IActionResult Excel(List<ProductDTO> productDTOs) 
        {
            productDTOs = productDTOs == null ? new List<ProductDTO>() : productDTOs;
            return View(productDTOs);
        }
        [HttpPost]
        public IActionResult Excel(IFormFile file)
        {
            string fileName = $"{_hostingEnvironment.WebRootPath}\\files\\{file.FileName}";
            using (FileStream filestram = System.IO.File.Create(fileName))
            {
                file.CopyTo(filestram);
                filestram.Flush();
            }
            var _productDTOforExcel = this.GetProductDTOsForExcel(file.FileName);
            return Excel(_productDTOforExcel);

        }
        private List<ProductDTO>GetProductDTOsForExcel(string Fname)
        {
            List<ProductDTO> productDTOforExcel = new List<ProductDTO>();
            var fileName = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\files"}" + "\\" + Fname;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using (var stream=System.IO.File.Open(fileName,FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    while (reader.Read())
                    {
                        productDTOforExcel.Add(new ProductDTO
                        {
                            Name = reader.GetValue(0).ToString(),
                            Description = reader.GetValue(1).ToString(),
                            Price = reader.GetValue(2).ToString().ToFloat(),
                        });
                    }
                }
            }
            return productDTOforExcel;
        }
    }
}