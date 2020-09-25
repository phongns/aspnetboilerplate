using System.Collections.Generic;

namespace Abp.WebApi.Configuration
{
    internal class AbpWebApiConfiguration : IAbpWebApiConfiguration
    {
        public List<string> ResultWrappingIgnoreUrls { get; }

        public AbpWebApiConfiguration(/*IDynamicApiControllerBuilder dynamicApiControllerBuilder*/)
        {
            ResultWrappingIgnoreUrls = new List<string>();
        }
    }
}
