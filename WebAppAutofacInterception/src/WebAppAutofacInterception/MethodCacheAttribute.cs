using System;

namespace WebAppAutofacInterception
{
    public class MethodCacheAttribute : Attribute
    {
        public double Duration { get; set; }

        public string ClearCacheKeyName { get; set; }

        public MethodCacheAttribute(string clearCacheKeyName, double duration = 30)
        {
            Duration = duration;
            ClearCacheKeyName = clearCacheKeyName;
        }
    }
}