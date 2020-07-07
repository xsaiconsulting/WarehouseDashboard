using System;
using System.Collections.Generic;
using System.Text;

namespace SimulatePurchase.Models
{
    public class PurchaseOrder
    {
        public  int PurchaseOrderID { get; set; }
        public  int ProductID { get; set; }
        public int  SupplierID { get; set; }
        public  int Quantity { get; set; }
        public DateTime DeliveryIn { get; set; }



    }

    public class SalesOrder
    {
        public int SalesOrderID { get; set; }
        public int ProductID { get; set; }
        public int CustomerID { get; set; }
        public int Quantity { get; set; }
        public DateTime DeliveryOut { get; set; }

        public int PurchaseOrderID { get; set; }
        public int PurchaseToSalesTime { get; set; }


    }


    public class ReturnedItem
    {
        public int ReturnID { get; set; }
        public int SalesOrderID { get; set; }

        public int ReturnedQty { get; set; }

        public DateTime ReturnedTime { get; set; }

    }

}
