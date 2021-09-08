using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BitCubeStoreImplementation.Models {
    public class OnlineStore : IOnlineStore {
        public List<Product> Products;

        public OnlineStore() {
            Products = new List<Product>() { 
                new Product{Category = ProductCategory.Laptop,  NumberOfItems = 5, Price = 1000},
                new Product{Category = ProductCategory.Tablet,  NumberOfItems = 3, Price = 800},
                new Product{Category = ProductCategory.Phone,  NumberOfItems = 4, Price = 500},
            };
        }

        public void AddProductsToInventory(ProductPurchaseOrder purchaseOrder) {
            var tempProducts = new List<Product>();

            foreach (var elementProduct in Products) {
                if (elementProduct.Category == purchaseOrder.Product.Category) {
                    elementProduct.NumberOfItems = elementProduct.NumberOfItems + purchaseOrder.Product.NumberOfItems;
                }
                tempProducts.Add(elementProduct);
            }
            Products = tempProducts;
        }

        public ProductsSoldResult SellProductsFromInventory(ProductsSellOrder itemsSoldOrder) {
            var soldProduct = Products.FirstOrDefault(x => x.Category == itemsSoldOrder.ProductSold.Category);
            if (soldProduct.NumberOfItems < itemsSoldOrder.ProductSold.NumberOfItems) {
                // generate appropriate error.
                //return empty product, cannot sell
                ProductsSoldResult productsSoldResult = null;
                return productsSoldResult;
            }
            itemsSoldOrder.ProductSold.Price = itemsSoldOrder.ProductSold.NumberOfItems * soldProduct.Price;

            var tempProducts = new List<Product>();

            // decrease number of products from inventory since item has been purchased.
            foreach (var elementProduct in Products) {
                if (elementProduct.Category == itemsSoldOrder.ProductSold.Category) {
                    elementProduct.NumberOfItems = elementProduct.NumberOfItems - itemsSoldOrder.ProductSold.NumberOfItems;
                }
                tempProducts.Add(elementProduct);
            }

            //update inventory with new number of product.
            Products = tempProducts;
            return new ProductsSoldResult { ProductSoldResult = new ProductsSellOrder { ProductSold = itemsSoldOrder.ProductSold }, ResultConfirmation = "Product Sold" };
        }

        public InventorySummary GetInventorySummary() {
            return new InventorySummary { Products = Products };
        }

        public InventoryItemSummary GetInventoryItemSummary(ProductType stock) {
            double price = Products.FirstOrDefault(product => product.Category == stock.ProductCategory).Price;
            int numberOfProducts = Products.FirstOrDefault(product => product.Category == stock.ProductCategory).NumberOfItems;
            Dictionary<string,string> icons =new Dictionary<string, string>() 
                {{"Laptop" ," bi-laptop" },{ "Tablet"," bi-tablet-landscape-fill" },{ "Phone"," bi-phone-vibrate" } };
            return new InventoryItemSummary {
                    Price = price, 
                    NumOfProducts = numberOfProducts,
                    productIconInfo =icons[stock.ProductCategory.ToString()],
                    ProductName = stock.ProductCategory.ToString()
                };
        }

      
    }
}
