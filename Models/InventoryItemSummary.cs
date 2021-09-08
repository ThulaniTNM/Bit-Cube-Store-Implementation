using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BitCubeStoreImplementation.Models {
    public class InventoryItemSummary {
        public double Price { get; set; }
        public int NumOfProducts { get; set; }

        public string  productIconInfo { get; set; }
        public string ProductName { get; set; }
    }
}
