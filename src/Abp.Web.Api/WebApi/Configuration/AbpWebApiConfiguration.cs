using System.Collections.Generic;
using System.Web.Http;

namespace Abp.WebApi.Configuration
{
    internal class AbpWebApiConfiguration : IAbpWebApiConfiguration
    {
        public List<string> ResultWrappingIgnoreUrls { get; }

        public HttpConfiguration HttpConfiguration { get; set; }

        public AbpWebApiConfiguration(/*IDynamicApiControllerBuilder dynamicApiControllerBuilder*/)
        {
            HttpConfiguration = GlobalConfiguration.Configuration;
            ResultWrappingIgnoreUrls = new List<string>();
        }
    }
}
