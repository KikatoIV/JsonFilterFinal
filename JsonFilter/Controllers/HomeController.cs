using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using JsonFilter.Models;
using JsonFilter.Services;

namespace JsonFilter.Controllers
{
    public class HomeController : Controller
    {
        private ContentService ContentService;
        private JsonQServices JsonQServices;
        public HomeController(ContentService content, JsonQServices qServices)
        {
            ContentService = content;
            JsonQServices = qServices;
        }

        public IActionResult Index()
        {
            return View(ContentService.ReturnCarJson());
        }
        //this controlls all the actions on the DataFilter page
        public ActionResult DataFilter(ResultModel model)
        {
            var strCarinfo = model.strResult;
            int intYearOne = model.intYearOne;
            int intYearTwo = model.intYearTwo;
            try
            {
                //Output will always be a list of items!
                if (intYearTwo > 1 && intYearOne > 1)
                {
                    ViewBag.strCarinfo = strCarinfo;
                    ViewBag.Results = JsonQServices.QuerySales(intYearOne, intYearTwo, strCarinfo);
                    ViewBag.Avarg = JsonQServices.QueryAvarageAcrossAll(intYearOne, intYearTwo);
                    ViewBag.Comn = JsonQServices.QueryMostCommonStr(intYearOne, intYearTwo, strCarinfo);
                }
                else
                {
                    if (intYearTwo < 1 || intYearOne < 1)
                    {
                        ViewBag.MessageEror = "Something seems wrong, make sure you type year as a number";
                    }
                }
                return View();
            }
            catch
            {
                throw ViewBag.MessageEror = "Something seems wrong, make sure you type year as a number";
            }
        }

        //Look at/understand what dependency injection 
        public async Task<IActionResult> JsonAsync(JsonPageModel json, string jsonUrl)
        {
            //This grabs the url from the text box in view which is stored in TempData that is kept

            _ = json.JsonUrl;
            ViewBag.JsonUrl = jsonUrl;

            if (jsonUrl != null)
            {
                //this downloads the data from the URL and converts it into a JArray
                //PUT UP TEST
                ContentService.cars = await ContentService.JsonListDownloadAsync(jsonUrl);

                //writes all data from url to file in wwwroot, this can be used now for data request
                //this will clear out old data and replace it with new data

                ContentService.Save();

                //ContentService.seedData();
                return View("Json");
            }

            return View("Json");
        }
    } 

}