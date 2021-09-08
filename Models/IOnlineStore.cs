using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BitCubeStoreImplementation.Models {
    public interface IOnlineStore {
        void AddProductsToInventory(ProductPurchaseOrder purchaseOrder);
        ProductsSoldResult SellProductsFromInventory(ProductsSellOrder itemsSoldOrder);
        InventoryItemSummary GetInventoryItemSummary(ProductType stock);
        InventorySummary GetInventorySummary();

    }
}
