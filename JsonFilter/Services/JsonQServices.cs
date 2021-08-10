using System;
using System.Collections.Generic;
using System.Linq;


namespace JsonFilter.Services
{
    public class JsonQServices
    {
        private ContentService ContentService;
        public JsonQServices(ContentService content)
        {
            ContentService = content;
        }
        //This is for all the Querys
        public object QuerySales(int intYearOne, int intYearTwo, string str)
        {
            intYearOne = intYearOne - 2;
            intYearTwo = intYearTwo + 1;

            (string, int) tup;
            var queryAllCars = ContentService.ReturnCarJson();
            if (str == "model")
            {
                var test = queryAllCars.GroupBy(u => u.model).Select(g => tup
                 =
                (
                    g.Key,
                    g.Sum(s => s.salesHistory.Where(sH => sH.year > intYearOne && sH.year < intYearTwo).Sum(c => c.vehiclesSold))
                )).ToList();

                return test;
            }
            if (str == "colour")
            {
                var test = queryAllCars.GroupBy(u => u.colour).Select(g => tup
                 =
                (
                    g.Key,
                    g.Sum(s => s.salesHistory.Where(sH => sH.year > intYearOne && sH.year < intYearTwo).Sum(c => c.vehiclesSold))
                ));

                return test.ToList();
            }
            if (str == "manufacturer")
            {
                var test = queryAllCars.GroupBy(u => u.manufacturer).Select(g => tup
                 =
                (
                    g.Key,
                    g.Sum(s => s.salesHistory.Where(sH => sH.year > intYearOne && sH.year < intYearTwo).Sum(c => c.vehiclesSold))
                ));

                return test.ToList();
            }
            return null;
        }

        //Gets the avarage of cars sold and adds them up 
        public object QueryAverage(int intYearOne, int intYearTwo)
        {
            intYearOne = intYearOne - 2;
            intYearTwo = intYearTwo + 1;

            var queryAllCars = ContentService.ReturnCarJson();
            double count = 0.0;
            var test = queryAllCars.Select(g => g.salesHistory.Where(sH => sH.year > intYearOne && sH.year < intYearTwo).Average(s => s.vehiclesSold)).ToList();

            foreach (var avg in test)
            {
                count += avg;
            }
            return Convert.ToInt32(count);

        }
        public object QueryAvarageAcrossAll(int intYearOne, int intYearTwo)
        {
            intYearOne = intYearOne - 2;
            intYearTwo = intYearTwo + 1;

            var queryAllCars = ContentService.ReturnCarJson();
            var amountIds = queryAllCars.Count();
            double count = 0.0;
            var totalSalesBetweenY = queryAllCars.Select(g => g.salesHistory.Where(sH => sH.year > intYearOne && sH.year < intYearTwo).Sum(s => s.vehiclesSold)).ToList();

            foreach (var avg in totalSalesBetweenY)
            {
                count += avg;
            }

            var avrg = count / amountIds;

            return Convert.ToInt32(avrg);
        }



        //Finds the most common string
        //If there is time deligate dont repeat
        public object QueryMostCommonStr(int intYearOne, int intYearTwo, string str)
        {
            intYearOne = intYearOne - 2;
            intYearTwo = intYearTwo + 1;

            var queryAllCars = ContentService.ReturnCarJson();

            if (str == "model")
            {
                (string, int) tup;
                var test = queryAllCars.GroupBy(u => u.model).Select(g => tup
                    =
                (
                    g.Key,
                    g.Sum(s => s.salesHistory.Where(sH => sH.year > intYearOne && sH.year < intYearTwo).Sum(c => c.vehiclesSold))
                ));

                var tmax = test.OrderByDescending(c => c.Item2);
                var onet = tmax.First().Item1;

                return onet.ToString();
            }
            if (str == "manufacturer")
            {
                (string, int) tup;
                var test = queryAllCars.GroupBy(u => u.manufacturer).Select(g => tup
                    =
                (
                    g.Key,
                    g.Sum(s => s.salesHistory.Where(sH => sH.year > intYearOne && sH.year < intYearTwo).Sum(c => c.vehiclesSold))
                ));

                var tmax = test.OrderByDescending(c => c.Item2);
                var onet = tmax.First().Item1;

                return onet.ToString();
            }
            if (str == "colour")
            {
                (string, int) tup;
                var test = queryAllCars.GroupBy(u => u.colour).Select(g => tup
                    =
                (
                    g.Key,
                    g.Sum(s => s.salesHistory.Where(sH => sH.year > intYearOne && sH.year < intYearTwo).Sum(c => c.vehiclesSold))
                ));

                var tmax = test.OrderByDescending(c => c.Item2);
                var onet = tmax.First().Item1;

                return onet.ToString();
            }

            return null;
        }

        //HOW MANY MODELS WHERE SOLD  
        public List<int> QueryModelSold(int intYearOne, int intYearTwo)
        {
            intYearOne = intYearOne - 2;
            intYearTwo = intYearTwo + 1;

            var queryAllCars = ContentService.ReturnCarJson();

            var saleshist = queryAllCars.Select(s => s.salesHistory.Where(sH => sH.year > intYearOne && sH.year < intYearTwo).Sum(x => x.vehiclesSold)).ToList();

            return saleshist;
        }


    }
}
