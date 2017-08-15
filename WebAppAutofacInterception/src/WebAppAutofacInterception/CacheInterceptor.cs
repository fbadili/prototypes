using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Components.DictionaryAdapter;
using Castle.DynamicProxy;
using Microsoft.Extensions.Caching.Memory;

namespace WebAppAutofacInterception
{
    public class CacheInterceptor : IInterceptor
    {
        private const string KeysListName = "KeysList";
        private IMemoryCache _cache;

        public CacheInterceptor(IMemoryCache cache)
        {
            _cache = cache;
        }

        public void Intercept(IInvocation invocation)
        {
            var attributes = invocation.MethodInvocationTarget.CustomAttributes;
            var methodCacheAttribute = attributes.FirstOrDefault(o => o.AttributeType == typeof(MethodCacheAttribute));
            if (methodCacheAttribute == null)
            {
                invocation.Proceed();
                return;
            }

            var clearCacheKeyName = (string)methodCacheAttribute.ConstructorArguments[0].Value;

            var key = $"{clearCacheKeyName}:{invocation.InvocationTarget}:{invocation.Method}:{string.Join(", ", invocation.Arguments.Select(a => (a ?? "").ToString()).ToArray())}";


            HashSet<string> keysList;

            if (!_cache.TryGetValue(KeysListName, out keysList))
            {
                keysList = new HashSet<string>();
                _cache.Set(KeysListName, keysList, new MemoryCacheEntryOptions { Priority = CacheItemPriority.High, SlidingExpiration = TimeSpan.MaxValue });
            }

            keysList.Add(key);


            object cachedResult;

            var exists = _cache.TryGetValue(key, out cachedResult);

            if (exists)
            {
                invocation.ReturnValue = cachedResult;
                return;
            }

            invocation.Proceed();

            var result = invocation.ReturnValue;

            var duration = (double)methodCacheAttribute.ConstructorArguments[1].Value;

            // Set cache options.
            var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(duration));

            // Save data in cache.
            _cache.Set(key, result, cacheEntryOptions);

        }
    }
}