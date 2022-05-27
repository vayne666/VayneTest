using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.Build.Pipeline;
using UnityEditor.Build.Pipeline.Injector;
using UnityEditor.Build.Pipeline.Interfaces;
namespace Assets.AssetBundle.Editor {
    class CalculateDependency : IBuildTask {
        [InjectContext(ContextUsage.In)]
        IBuildContent m_Content;

        [InjectContext]
        IDependencyData m_DependencyData;
        public int Version => 1;
        //Dictionary<string,>
        public ReturnCode Run() {
            foreach (var item in m_DependencyData.SceneInfo) {
                
            }
            return ReturnCode.Success;
        }

        
    }
}
