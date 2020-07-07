using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace SimulatePurchase
{
    class Program
    {

        public const string basePath= @"C:\\articles\\simulation\";
        static void Main(string[] args)
        {
           

            Console.WriteLine("Hello World!");
            var poList =POGenerator.Instance.GeneratePOs(new DateTime(2020,01,01),180);
            var soList = SOGenerator.Instance.GenerateSOs(poList);
            var reList = SOGenerator.Instance.GenerateReturns(soList);
            string poFile = basePath + "pofile.txt";
            using (var sw = new StreamWriter(poFile))
            {
                sw.WriteLine("PurchaseOrderID,ProductID,SupplierID,Quantity,TimeStamp");
                foreach(var po in poList)
                {
                    string line = string.Format("{0},{1},{2},{3},{4}", po.PurchaseOrderID, po.ProductID, po.SupplierID, po.Quantity, po.DeliveryIn);

                    sw.WriteLine(line);
                }
               
            }

            string soFile = basePath + "sofile.txt";
            using (var sw = new StreamWriter(soFile))
            {
                sw.WriteLine("SalesOrderID,ProductID,CustomerID,Quantity,TimeStamp,PurchaseOderID,POToSalesInHours");
                foreach (var so in soList)
                {
                    string line = string.Format("{0},{1},{2},{3},{4},{5},{6}", so.SalesOrderID, so.ProductID, so.CustomerID, so.Quantity, so.DeliveryOut,so.PurchaseOrderID,so.PurchaseToSalesTime);

                    sw.WriteLine(line);
                }

            }

            string reFile = basePath + "retfile.txt";
            using (var sw = new StreamWriter(reFile))
            {
                sw.WriteLine("ReturnID,SalesOrderID,Return Qunatity,TimeStamp");
                foreach (var re in reList)
                {
                    string line = string.Format("{0},{1},{2},{3}", re.ReturnID, re.SalesOrderID, re.ReturnedQty, re.ReturnedTime);

                    sw.WriteLine(line);
                }

            }

            Console.ReadKey();
        }
    }
}
