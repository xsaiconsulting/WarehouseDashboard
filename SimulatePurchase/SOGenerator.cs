using SimulatePurchase.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace SimulatePurchase
{
    public class SOGenerator
    {
        private readonly static SOGenerator _instance = new SOGenerator();
        private readonly Random _random = new Random();
        private const int MAX_PRODUCTS = 15;
        private const int MAX_CUSTOMERS = 10;
        private const int MIN_QTY = 100;
        private const int MAX_QTY = 500;
        private const int DELIVERY_FREQ_MIN = 4;
        private const int DELIVERY_FREQ_MAX = 8;

        private int[] _delivery_out_min = {36,32,28,25,22,19 };
        private int[] _delivery_out_max = { 60, 53, 46, 40, 35, 31 };

        private SOGenerator()
        {

        }

        public static SOGenerator Instance
        {
            get { return _instance; }
        }

        public SalesOrder SalesOrder
        {
            get => default;
            set
            {
            }
        }

        public ReturnedItem ReturnedItem
        {
            get => default;
            set
            {
            }
        }

        public List<SalesOrder> GenerateSOs(List<PurchaseOrder> poList)
        {

            List<SalesOrder> soList = new List<SalesOrder>();

            DateTime minDate = poList.Min(po => po.DeliveryIn);
            int minMonth = minDate.Month;
            int soID = 1;
            foreach (var po in poList)
            {

                int monthIndex = po.DeliveryIn.Month - minMonth;

                var deliverytol = (_random.Next(90, 100)) / 100.0;
                if(monthIndex <0 || monthIndex >=_delivery_out_max.Length)
                {
                    monthIndex = 0;
                }
                int qty = (int)(po.Quantity * deliverytol);
                int addHours = _random.Next(_delivery_out_min[monthIndex], _delivery_out_max[monthIndex]);

                SalesOrder so = new SalesOrder()
                {
                    SalesOrderID = soID,
                    ProductID = po.ProductID,
                    PurchaseOrderID = po.PurchaseOrderID,
                    Quantity = qty,
                    DeliveryOut = po.DeliveryIn.AddHours(addHours),
                    CustomerID = _random.Next(1, MAX_CUSTOMERS),
                    PurchaseToSalesTime = addHours,
                };
                soID++;
                soList.Add(so);
            }
                    
            return soList;

        }

        public List<ReturnedItem> GenerateReturns(List<SalesOrder> soList)
        {

            List<ReturnedItem> reList = new List<ReturnedItem>();

            int orderChunk = soList.Count / 6;
            int orderIndex = 0;
            int returnID = 1;
            int nextOrder = 0;
            foreach (var sorder in soList)
            {
                double factor = _random.Next(8,12)*0.01;  // 6 to 10%;
                int noRejections = (int)(orderChunk *factor);
                nextOrder += orderChunk;
                for(int j=0;j< noRejections;j++)
                {
                    int retOrderIndex = _random.Next(orderIndex, nextOrder);
                    if(retOrderIndex < soList.Count)
                    {
                        var so = soList[retOrderIndex];
                        ReturnedItem ri = new ReturnedItem();
                        ri.ReturnID = returnID;
                        ri.SalesOrderID = so.SalesOrderID;
                        double fr = _random.Next(6, 10) * 0.01;  // 6 to 10%;

                        ri.ReturnedQty = (int)(so.Quantity * fr);
                        ri.ReturnedTime = so.DeliveryOut.AddDays(3);
                        reList.Add(ri);
                        returnID++;
;                    }
                  

                }

               
             
            
            }

            return reList;

        }

    }
}
