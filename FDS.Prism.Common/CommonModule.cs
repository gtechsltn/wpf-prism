using Prism.Mef.Modularity;
using Prism.Modularity;
using System.ComponentModel.Composition;

namespace FDS.Prism.Common
{
    [ModuleExport("CommonModule", typeof(CommonModule))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class CommonModule : IModule
    {
        public CommonModule() { }
        public void Initialize() { }
    }
}
