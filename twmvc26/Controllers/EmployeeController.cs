using System.Collections.Generic;
using System.Web.Mvc;
using twmvc26.Models;
using twmvc26.Services;

namespace twmvc26.Controllers
{
    public class EmployeeController : Controller
    {
        private IDataCacheService _dataCacheService;

        private IService<Employee> _employeeService;

        private ICacheProvider _cacheProvider;

        public EmployeeController(
            IDataCacheService dataCacheService,
            IService<Employee> employeeService,
            ICacheProvider cacheProvider)
        {
            this._dataCacheService = dataCacheService;
            this._employeeService = employeeService;
            this._cacheProvider = cacheProvider;
        }

        public ActionResult Index()
        {
            List<Employee> result = null;

            //// 1.從快取層取得資料
            var cacheData = this._cacheProvider.Get<List<Employee>>("Key");

            //// 2.找不到快取資料
            if (cacheData == null)
            {
                ViewBag.Status = "Data From 服務";

                //// 3.從服務層取得資料
                result = this._employeeService.GetAll();

                //// 4.將資料設定到快取
                this._cacheProvider.Set("Key", result, 5);
            }
            else
            {
                ViewBag.Status = "Data From 快取";

                //// 3.快取有資料，直接回傳
                result = cacheData;
            }

            return View(result);
        }

        public ActionResult Index2()
        {
            var enableCache = true;
            var cleanCache = false;
            var memoryCacheExpirationSecond = 5;

            var employeeList = this._dataCacheService.GetMemoryCacheData<List<Employee>>(
                "ServiceName",
                "TypeName",
                "Key2",
                () =>
                {
                    ViewBag.Status = "Data From Service";

                    var result = this._employeeService.GetAll();
                    return result;
                },
                memoryCacheExpirationSecond,
                enableCache,
                cleanCache);

            return View(employeeList);
        }
    }
}