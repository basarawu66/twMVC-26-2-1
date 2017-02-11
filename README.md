# twmvc26sample
淺談 ASP.NET Caching 技術與實踐範例程式碼

## 1. CacheProvider
- ICacheProvider
- MemoryCacheProvider
- NullCacheProvider

### MemoryCacheProvider / NullCacheProvider + ProviderModule + Autofac

	public class ProviderModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			var cacheProviderType = ConfigurationManager.AppSettings.Get("CacheProviderType");

			builder
				.RegisterType(Type.GetType(cacheProviderType))
				.As<ICacheProvider>()
				.InstancePerLifetimeScope();
		}
	}

### Config 設定	

	<appSettings>
      <!-- CacheProvider  -->
      <add key="CacheProviderType" value="twmvc26.MemoryCacheProvider, twmvc26" />
      <!--<add key="CacheProviderType" value="twmvc26.NullCacheProvider, twmvc26" />-->
	</appSettings>
  
### 使用方式

	public ActionResult Index()
	{
		var cacheTime = 3;	
		List<Employee> result = null;
		//// 1.從快取層取得資料
		var cacheData = this._cacheProvider.Get<List<Employee>>("Key");
		//// 2.找不到快取資料
		if (cacheData == null)
		{
			//// 3.從服務層取得資料
			result = this._employeeService.GetAll();
			//// 4.將資料設定到快取
			this._cacheProvider.Set("Key", result, cacheTime);
		}
		else
		{
			//// 3.快取有資料，直接回傳
			result = cacheData;
		}
		return View(result);
	}


## 2. DataCacheService
- IDataCacheService
- DataCacheService

### 使用方式

	public ActionResult Index()
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


----------

以上。

若有不清楚或是需要進一步協助請再讓我知道。謝謝。
