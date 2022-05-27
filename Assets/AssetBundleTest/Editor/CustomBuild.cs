using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.Build.Pipeline;
using UnityEditor.Build.Pipeline.Injector;
using UnityEditor.Build.Pipeline.Interfaces;
namespace Assets.AssetBundleTest.Editor {
    class CustomBuild {


        public void CreateTask() {
            List<IBuildTask> tasks = new List<IBuildTask>();
            // create asset build
            //asset dependency
            //scene dependency      //scene 的依赖中只有 perfab 会有其他依赖
            //merge dependency


        }


    }
}
