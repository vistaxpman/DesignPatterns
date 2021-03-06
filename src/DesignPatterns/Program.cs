﻿using DesignPatterns.Facade;
using DesignPatterns.Factory;
using DesignPatterns.Strategy;
using DesignPatterns.Visitor;
using System;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    class Program
    {
        private static string listenUrl = "http://localhost:5000/api/listen";
        static async Task Main(string[] args)
        {
            //await StrategyPattern();
            //await FactoryPattern();
            VisitorPattern();

        }

        public static async Task StrategyPattern()
        {
            var xmlBuilder = new OrderSummaryXmlRequestBuilder();
            var jsonBuilder = new OrderSummaryJsonRequestBuilder();

            var orderSummary = new OrderSummary
            {
                UserId = 1,
                ItemId = 20,
                PurchaseDate = DateTime.UtcNow
            };

            var xmlClient = new ApiClient(xmlBuilder);
            var xmlResponse = await xmlClient.SendOrderSummary(listenUrl, orderSummary);
            Console.WriteLine(xmlResponse);

            var jsonClient = new ApiClient(jsonBuilder);
            var jsonResponse = await jsonClient.SendOrderSummary(listenUrl, orderSummary);
            Console.WriteLine(jsonResponse);
            
        }

        public static async Task FactoryPattern()
        {
            var lawnChairOrderSummary = new FactoryOrderSummary
            {
                UserId = 1,
                ItemId = 20,
                Vendor = Vendor.LawnChairCo,
                Name = "Good Chair",
                PurchaseDate = DateTime.UtcNow
            };

            var jsonOrderSummary = new FactoryOrderSummary
            {
                UserId = 2,
                ItemId = 21,
                Vendor = Vendor.BestComputersInc,
                Name = "Good Computer",
                PurchaseDate = DateTime.UtcNow
            };

            var xmlClient = new FactoryApiClient(new OrderSummaryFactory());

            await xmlClient.SendOrderSummary(listenUrl, lawnChairOrderSummary);
            await xmlClient.SendOrderSummary(listenUrl, jsonOrderSummary);
        }

        public static void FacadePattern()
        {
            //bad way
            var nonFacade = new NonFacadeApp();
            var appHealthy = nonFacade.IsDatabaseAHealthy();
            appHealthy = appHealthy && nonFacade.IsDatabaseBHealthy();
            appHealthy = appHealthy && nonFacade.IsServiceAHealthy();
            appHealthy = appHealthy && nonFacade.IsServiceBHealthy();

            Console.WriteLine($"Is Non Facade App Healthy?: {appHealthy}");


            //good way
            var facade = new FacadeApp();
            Console.WriteLine($"Is Facade App Healthy?: {facade.IsSystemHealthy()}");

        }

        public static void VisitorPattern()
        {
            var wiki = new WikipediaExample();
            wiki.Run();
            Console.ReadKey();
            
        }
    }
}
