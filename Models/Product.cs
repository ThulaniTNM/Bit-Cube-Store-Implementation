using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BitCubeStoreImplementation.Models {
    public class Product {
        public ProductCategory Category { get; set; }
        public double Price { get; set; }
        public int NumberOfItems { get; set; }
    }
}
