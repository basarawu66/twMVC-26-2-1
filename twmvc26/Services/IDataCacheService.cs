using System;

namespace twmvc26.Services
{
    /// <summary>
    /// IDataCacheService
    /// </summary>
    public interface IDataCacheService
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
        T GetMemoryCacheData<T>(string cacheName, string cacheType, string key, Func<T> source, int memoryCacheExpirationSecond = 10, bool enabledCache = true, bool cleanCache = false) where T : class;
    }
}