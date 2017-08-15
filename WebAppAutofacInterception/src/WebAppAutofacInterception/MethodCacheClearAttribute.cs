using System;

namespace WebAppAutofacInterception
{
    public class MethodCacheClearAttribute : Attribute
    {
        public string ClearCacheKeyName { get; set; }

        public MethodCacheClearAttribute(string clearCacheKeyName)
        {
            ClearCacheKeyName = clearCacheKeyName;
        }
    }
}