using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Abp.Dependency
{
    /// <summary>
    /// Define interface for classes those are used to register dependencies.
    /// </summary>
    public interface IIocRegistrar
    {
        /// <summary>
        /// Registers types of given assembly by all conventional registrars. See <see cref="IocManager.AddConventionalRegistrar"/> method.
        /// </summary>
        /// <param name="assembly">Assembly to register</param>
        void RegisterAssemblyByConvention(Assembly assembly);
    }
}
