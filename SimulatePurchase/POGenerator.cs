using SimulatePurchase.Models;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Text;

namespace SimulatePurchase
{
    public class POGenerator
    {
        private readonly static POGenerator _instance = new POGenerator();
        private readonly Random _random = new Random();
        private const int MAX_PRODUCTS = 15;
        private const int MAX_SUPPLIERS = 8;
        private const int MIN_QTY = 100;
        private const int MAX_QTY = 500;
        private const int DELIVERY_FREQ_MIN = 4;
        private const int DELIVERY_FREQ_MAX = 8;


        private POGenerator()
        {

        }

        public static  POGenerator Instance
        {
            get { return _instance; }
        }

        public PurchaseOrder PurchaseOrder
        {
            get => default;
            set
            {
            }
        }

        public List<PurchaseOrder> GeneratePOs(DateTime startDate, int days)
        {
            List<PurchaseOrder> poList = new List<PurchaseOrder>();
            int poID = 1;
            for(int i =0;i < days; i++)
            {
               int no_deliveries = _random.Next(DELIVERY_FREQ_MIN, DELIVERY_FREQ_MAX);
                DateTime curDate = startDate.AddDays(i);
                for (int j = 0; j < no_deliveries; j++)
                {
                    var po = new PurchaseOrder()
                    {
                        PurchaseOrderID = poID,
                        ProductID = _random.Next(1, MAX_PRODUCTS),
                        SupplierID = _random.Next(1, MAX_SUPPLIERS),
                        Quantity = _random.Next(MIN_QTY, MAX_QTY),
                        DeliveryIn = curDate
                        
                    };
                    poList.Add(po);
                    poID++;
                }

               
            }

            return poList;
        }
    }
}
