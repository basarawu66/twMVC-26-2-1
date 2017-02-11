using System;
using System.Runtime.Caching;

namespace twmvc26.Services
{
    public class DataCacheService : IDataCacheService
    {
        /// <summary>
        /// 取得Memory快取內容
        /// </summary>
        /// <typeparam name="T">快取內容型別</typeparam>
        /// <param name="cacheName">快取名稱</param>
        /// <param name="cacheType">快取類型</param>
        /// <param name="key">快取鍵值</param>
        /// <param name="source">新增快取內容</param>
        /// <param name="memoryCacheExpirationSecond">記憶體快取過期時間(秒)預設10秒</param>
        /// <param name="enabledCache">是否啟用Cache</param>
        /// <param name="cleanCache">是否清除快取</param>
        /// <returns>快取內容</returns>
        public T GetMemoryCacheData<T>(string cacheName, string cacheType, string key, Func<T> source, int memoryCacheExpirationSecond = 10, bool enabledCache = true, bool cleanCache = false) where T : class
        {
            T returnResult;

            //// 沒有開啟cache的話就直接回傳
            if (!enabledCache)
            {
                returnResult = source();
                return returnResult;
            }

            string fullCacheName = string.Format("{0}-{1}-{2}", cacheName, cacheType, key);
            var memoryCache = MemoryCache.Default;

            //// 設定清除快取
            if (cleanCache)
            {
                memoryCache.Remove(fullCacheName);
            }

            var memoryCacheObject = memoryCache.Get(fullCacheName);

            //// 找不到Cache時直接取得資料
            if (memoryCacheObject == null || cleanCache)
            {
                memoryCacheObject = source();

                if (memoryCacheObject != null)
                {
                    //// 寫入MemoryCache
                    var absoluteExpiration = DateTimeOffset.Now.AddSeconds(memoryCacheExpirationSecond);

                    memoryCache.Set(fullCacheName, memoryCacheObject, absoluteExpiration);
                }
            }

            //// 泛型宣告時有指定 T 為 class (參考型別)，所以如果 memoryCacheObject 為 null，就會回傳 null
            returnResult = (T)Convert.ChangeType(memoryCacheObject, typeof(T));

            return returnResult;
        }
    }
}