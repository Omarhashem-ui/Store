using Store.G02.Domain.Contracts;
using Store.G02.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Services
{
    public class CacheServices(ICacheRepository _cacheRepository): ICacheServices
    {
        public async Task<string> GetAsync(string key)
        {
          return await _cacheRepository.GetAsync(key);
        }

        public async Task SetAsync(string key, object value, TimeSpan duration)
        {
            await _cacheRepository.SetAsync(key, value, duration);
        }
    }
}
