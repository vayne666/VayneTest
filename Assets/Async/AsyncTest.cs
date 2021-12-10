using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Threading.Tasks;

public class AsyncTest : MonoBehaviour {
    int mainThread;

    void Start() {
        mainThread=Thread.CurrentThread.ManagedThreadId;
        Debug.Log($"main thread:{mainThread}");
        Test();
    }


    async void Test() {
        Debug.Log($"start test:{Time.realtimeSinceStartup}");
        await Task.Delay(5000);
        Debug.Log($"current thread:{Thread.CurrentThread.ManagedThreadId}");
        Debug.Log($"end test:{Time.realtimeSinceStartup}");
    }
}
