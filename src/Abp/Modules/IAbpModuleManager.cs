using System;
using System.Collections.Generic;
using System.Text;

namespace Abp.Modules
{
    public interface IAbpModuleManager
    {
        //AbpModuleInfo StartupModule { get; }

        //IReadOnlyList<AbpModuleInfo> Modules { get; }

        void Initialize(Type startupModule);

        void StartModules();

        void ShutdownModules();
    }
}
