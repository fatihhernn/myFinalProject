using Core.Utilities.IoC;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using System.Text.RegularExpressions;
using System.Linq;

namespace Core.CrossCuttingConcerns.Caching.Microsoft
{
    public class MemoryCacheManager : ICacheManager
    {
        //varolan sistemi kendi sistemime uyarlıyorum. =>ADAPTER PATTERN
        //ben sana göre değil, sen benim sistemime göre çalışacaksın

        //core => dependencyResolver ekle. => yarın redise geçersen başın ağrımasın
        IMemoryCache _memoryCache;

        //ctor yaparsan çalışmaz => coreModule altında dependecy resolver ekle
        //public MemoryCacheManager(IMemoryCache memoryCache)
        //{
        //    _memoryCache = memoryCache;
        //}

        public MemoryCacheManager()
        {
            _memoryCache = ServiceTool.ServiceProvider.GetService<IMemoryCache>();
        }

        public void Add(string key, object value, int duration)
        {
            _memoryCache.Set(key, value, TimeSpan.FromMinutes(duration));
        }

        public T Get<T>(string key)
        {
            return _memoryCache.Get<T>(key);
        }

        public object Get(string key)
        {
            return _memoryCache.Get(key);
        }

        public bool IsAdd(string key)
        {
            return _memoryCache.TryGetValue(key,out _);//out _ ben senden değer döndürmeni istemiyorum
        }

        //bellekten silmeye yarıyor => bellekte class'ın instance var, çalışma anında müdahale etmek istoruz=> bunu reflection ile yaparız
        //reflection ile, çalışma anında elimizdeki nesnelere, hatta olmayanları da yeniden oluşturma gibi işlemlemleri reflection ile yaparım
        public void Remove(string pattern)
        {
            //cache dataları EntriesCollection diye birşeyin içine atıyorum => onu bul
            var cacheEntriesCollectionDefinition = typeof(MemoryCache).GetProperty("EntriesCollection", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            //Definition : _memoryCache olanları bul
            var cacheEntriesCollection = cacheEntriesCollectionDefinition.GetValue(_memoryCache) as dynamic;

            List<ICacheEntry> cacheCollectionValues = new List<ICacheEntry>();

            //her bir cache elemanlarını gez
            foreach (var cacheItem in cacheEntriesCollection)
            {
                ICacheEntry cacheItemValue = cacheItem.GetType().GetProperty("Value").GetValue(cacheItem, null);
                cacheCollectionValues.Add(cacheItemValue);
            }

            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var keysToRemove = cacheCollectionValues.Where(d => regex.IsMatch(d.Key.ToString())).Select(d => d.Key).ToList();

            foreach (var key in keysToRemove)
            {
                _memoryCache.Remove(key);
            }
        }

        public void RemoveByPattern(string pattern)
        {
            
        }
    }
}
