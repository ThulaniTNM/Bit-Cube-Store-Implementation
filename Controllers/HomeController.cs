using BitCubeStoreImplementation.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BitCubeStoreImplementation.Controllers {
    public class HomeController : Controller {
        private readonly ILogger<HomeController> _logger;
        private readonly IOnlineStore _onlineStore;

        public HomeController(ILogger<HomeController> logger,IOnlineStore onlineStore) {
            _logger = logger;
            _onlineStore = onlineStore;
        }

        public IActionResult Sell() {
            return View();
        }

        [HttpPost]
        public IActionResult Sell(Product product) {
           var productSoldResult = _onlineStore.SellProductsFromInventory(new ProductsSellOrder { ProductSold = product });
            if (productSoldResult == null) {
                ViewBag.NumberOfInputProducts = product.NumberOfItems;
                return View("ErrorProducts");
            }
            else {
                return View("ProductSold", productSoldResult);
            }
        }

        public IActionResult Purchase() {
            return View();
        }

        [HttpPost]
        public IActionResult Purchase(Product product) {
            _onlineStore.AddProductsToInventory(new ProductPurchaseOrder { Product = product });
            product.Price = new OnlineStore().Products.FirstOrDefault(x => x.Category == product.Category).Price;
            return View("ProductPurchaseResult",product);
        }

        public IActionResult Item() {
            return View();
        }
        [HttpPost]
        public IActionResult Item(ProductType stock) {

            return View("ItemSummary",_onlineStore.GetInventoryItemSummary(stock));
        }
        public IActionResult Inventory() {
            
            return View(_onlineStore.GetInventorySummary().Products);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
