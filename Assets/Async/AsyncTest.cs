using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System;

public class AsyncTest : MonoBehaviour {
    int mainThread;
    public static Action onTick;
    void Start() {
        mainThread = Thread.CurrentThread.ManagedThreadId;
        Debug.Log($"main thread:{mainThread}");
        Test();
    }


    async void Test() {
        //var old = Time.realtimeSinceStartup;
        ////
        //await Task.Delay(5000);
        //Debug.Log($"current thread:{Thread.CurrentThread.ManagedThreadId}");
        ////
        //Debug.Log($"end test:{Time.realtimeSinceStartup - old}");

        //await new CustomTask();
        Debug.Log($"start test:{Time.realtimeSinceStartup}");
        await DelayTask1.Delay(2);
        Debug.Log($"end test:{Time.realtimeSinceStartup}");

        Debug.Log("stsrt");
        var t = await DelayTask2.Delay(2);
        Debug.Log("end :" + t);

    }

    private void Update() {
        onTick?.Invoke();
    }
}


public enum CustomTaskStatus {
    None,
    Complete,
    Fault,
}

public class DelayTask1 {

    float curTime;
    float totalTime;
    CustomTask task;
    bool isComp;
    public DelayTask1(float time) {
        totalTime = time;
        task = new CustomTask();
        AsyncTest.onTick += Update;
    }

    public void Update() {
        if (isComp) return;
        curTime += Time.deltaTime;
        if (curTime >= totalTime) {
            task.SetResult();
            isComp = true;
            AsyncTest.onTick -= Update;
        }
    }

    public static CustomTask Delay(float time) {
        DelayTask1 delayTask1 = new DelayTask1(time);
        return delayTask1.task;
    }
}



public class DelayTask2 {

    float curTime;
    float totalTime;
    CustomTask<float> task;
    bool isComp;
    public DelayTask2(float time) {
        totalTime = time;
        task = new CustomTask<float>();
        AsyncTest.onTick += Update;
    }

    public void Update() {
        if (isComp) return;
        curTime += Time.deltaTime;
        if (curTime >= totalTime) {
            task.SetResult(totalTime);
            isComp = true;
            AsyncTest.onTick -= Update;
        }
    }

    public static CustomTask<float> Delay(float time) {
        DelayTask2 delayTask1 = new DelayTask2(time);
        return delayTask1.task;
    }
}

public class CustomTask : ICriticalNotifyCompletion {

    public CustomTask GetAwaiter() {
        return this;
    }
    private CustomTask task;
    CustomTaskStatus taskStatus = CustomTaskStatus.None;
    Action callback;
    public bool IsCompleted => taskStatus != CustomTaskStatus.None;

    public void OnCompleted(System.Action continuation) {
        Debug.LogError("OnCompleted");
        callback = continuation;
    }
    public void GetResult() {
        Debug.LogError("GetResult");
    }

    public void SetResult() {
        Debug.LogError("SetResult");
        taskStatus = CustomTaskStatus.Complete;
        callback?.Invoke();
    }

    public void SetException() {
        taskStatus = CustomTaskStatus.Fault;
    }

    public void UnsafeOnCompleted(System.Action continuation) {
        Debug.LogError("UnsafeOnCompleted");
        callback = continuation;
    }
}




public class CustomTask<T> : ICriticalNotifyCompletion {

    T result;

    public CustomTask<T> GetAwaiter() {
        return this;
    }
    private CustomTask<T> task;
    CustomTaskStatus taskStatus = CustomTaskStatus.None;
    Action callback;
    public bool IsCompleted => taskStatus != CustomTaskStatus.None;

    public void OnCompleted(System.Action continuation) {
        Debug.LogError("OnCompleted");
        callback = continuation;
    }
    public T GetResult() {
        Debug.LogError("GetResult");
        return result;
    }

    public void SetResult(T result) {
        Debug.LogError("SetResult");
        this.result = result;
        taskStatus = CustomTaskStatus.Complete;
        callback?.Invoke();
    }

    public void SetException() {
        taskStatus = CustomTaskStatus.Fault;
    }

    public void UnsafeOnCompleted(System.Action continuation) {
        Debug.LogError("UnsafeOnCompleted");
        callback = continuation;
    }
}














//public class CustomAwaiter : ICriticalNotifyCompletion {


//}