using Newtonsoft.Json;

using System.Runtime.Caching;

namespace Coditech.Common.CacheManager
{
    public class CacheManager : ICacheManager
    {
        /// <summary>
        /// Creates an instance of ObjectCache and assigns it to the _cacheManager variable.
        /// </summary>
        ObjectCache _cacheManager = MemoryCache.Default;


        /// <summary>
        /// Retrieves data from the cache based on the specified key.
        /// </summary>
        /// <typeparam name="T">The type of the data to be retrieved.</typeparam>
        /// <param name="key">The key of the data to be retrieved.</param>
        /// <returns>The data associated with the specified key.</returns>
        public T GetData<T>(string key)
        {
            try
            {
                string _cacheData = Convert.ToString(_cacheManager.Get(key));

                if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(_cacheData))
                    return JsonConvert.DeserializeObject<T>(_cacheData);

                return default(T);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Removes data from the cache manager
        /// </summary>
        /// <param name="key">The key of the data to be removed</param>
        /// <returns>True if the data was removed, false otherwise</returns>
        public bool RemoveData(string key)
        {
            try
            {
                if (!string.IsNullOrEmpty(key))
                {
                    _cacheManager.Remove(key);
                    return true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return false;
        }

        /// <summary>
        /// Sets the data in the cache with the given key, value, absolute expiration and table.
        /// </summary>
        /// <typeparam name="T">The type of the data to be stored.</typeparam>
        /// <param name="key">The key of the data to be stored.</param>
        /// <param name="value">The value of the data to be stored.</param>
        /// <param name="absoluteExpiration">The absolute expiration of the data.</param>
        /// <param name="table">The table of the data.</param>
        /// <returns>A boolean indicating whether the data was successfully stored.</returns>
        public bool SetData<T>(string key, T value, DateTimeOffset absoluteExpiration, params string[]? table)
        {
            bool response = true;
            try
            {
                List<string> tablename = new List<string>();

                CacheItemPolicy policy = new CacheItemPolicy();
                policy.AbsoluteExpiration =
                    absoluteExpiration;
                policy.SlidingExpiration = TimeSpan.Zero;


                if (!string.IsNullOrEmpty(key))
                {
                    _cacheManager.Set(key, JsonConvert.SerializeObject(value, new JsonSerializerSettings() { ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore }), policy);
                }
                else
                {
                    response = false;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }

        /// <summary>
        /// Retrieves a CacheItem from the cache manager using the specified key.
        /// </summary>
        /// <param name="key">The key of the CacheItem to retrieve.</param>
        /// <returns>The CacheItem associated with the specified key.</returns>
        public CacheItem GetCacheItem<T>(string key)
        {
            try
            {
                CacheItem item = _cacheManager.GetCacheItem(key);
                return item;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
