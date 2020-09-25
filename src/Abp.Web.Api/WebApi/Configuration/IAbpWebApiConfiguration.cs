using System.Collections.Generic;

namespace Abp.WebApi.Configuration
{
    /// <summary>
    /// Used to configure ABP WebApi module.
    /// </summary>
    public interface IAbpWebApiConfiguration
    {
        /// <summary>
        /// List of URLs to ignore on result wrapping.
        /// </summary>
        List<string> ResultWrappingIgnoreUrls { get; }
    }
}
