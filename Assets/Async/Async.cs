//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Runtime.CompilerServices;
//using System.Threading;
//using System.Threading.Tasks;
//using UnityEngine;
//using UnityEngine.Networking;

//public class Async : MonoBehaviour {


//    // Use this for initialization
//    async void Start() {
//        //Debug.Log(Thread.CurrentThread.ManagedThreadId);
//        Debug.Log(Time.realtimeSinceStartup);
//        await Task.Delay(1000);
//        Debug.Log(Time.realtimeSinceStartup);
//        Debug.Log("============================");
//        await TaskManger.Instance.Delay(2);
//        Debug.Log(Time.realtimeSinceStartup);
//    }



//    private void Update() {
//        TaskManger.Instance.Update();
//    }


//    private DelayTask Delay(int time) {
//        var task = new DelayTask(2);


//        return task;
//    }

//    IEnumerator Start1() {
//        yield return new WaitForSeconds(1);
//    }


//    public IEnumerator Get(string url) {
//        UnityWebRequest request = UnityWebRequest.Get("");
//        yield return request.SendWebRequest();
//    }



//}



//public class TaskManger {

//    private static TaskManger _instance;

//    public static TaskManger Instance {
//        get {
//            if (_instance==null) {
//                _instance=new TaskManger();
//            }
//            return _instance;
//        }
//    }

//    List<CustomTask> tasks = new List<CustomTask>();

//    public void Update() {
//        if (tasks.Count<1) return;
//        for (int i = 0; i<tasks.Count; i++) {
//            tasks[i].Update();
//        }
//        RemoveCompItem();
//    }

//    private void RemoveItem(CustomTask item) {
//        tasks.Remove(item);
//    }

//    private void RemoveCompItem() {
//        for (int i = tasks.Count-1; i>=0; i--) {
//            if (tasks[i].IsComplete) {
//                tasks.RemoveAt(i);
//            }
//        }
//    }

//    public DelayTask Delay(float time) {
//        var task = new DelayTask(time);
//        tasks.Add(task);
//        return task;
//    }

//    public void RemoveTask(DelayTask t) {
//        for (int i = 0; i<tasks.Count; i++) {
//            if (tasks[i]==t) {
//                tasks.RemoveAt(i);
//                return;
//            }
//        }
//    }


//}

//public abstract class CustomTask {
//    private bool isComp;
//    public abstract bool IsComplete { get; set; }

//    public abstract void Update();
//    public abstract void Complete();
//    public abstract void StartTask();
//    public abstract void EndTask();
//    public abstract CustomAwaiter GetAwaiter();
//}

//public abstract class CustomTask<T> : CustomTask {
//    private T result;
//    public T Result { get; }
//}


//public class DelayTask : CustomTask {

//    private CustomAwaiter awaiter;
//    private float time;
//    private float curTime = 0;
//    public override bool IsComplete { get; set; }

//    public override CustomAwaiter GetAwaiter() {
//        return awaiter;
//    }
//    public DelayTask(float time) {
//        awaiter=new CustomAwaiter();
//        this.time=time;
//    }

//    public override void Update() {
//        curTime+=Time.deltaTime;
//        if (curTime>=time) {
//            Complete();
//        }
//    }

//    public override void Complete() {
//        awaiter.Complete();
//        IsComplete=true;

//    }

//    public override void StartTask() {
//        curTime=0;
//    }

//    public override void EndTask() {

//    }

//}

//public class HttpRsp {
//    public int errorCode;
//    public string data;
//}


//public class CustomAwaiter : ICriticalNotifyCompletion {

//    public CustomTask task;

//    public bool IsCompleted { get; private set; }

//    private Action comp;
//    //private 

//    public void OnCompleted(Action continuation) {
//        if (IsCompleted) {
//            continuation?.Invoke();
//        } else {
//            comp+=continuation;
//        }

//    }

//    public void UnsafeOnCompleted(Action continuation) {
//        if (IsCompleted) {
//            continuation?.Invoke();
//        } else {
//            comp+=continuation;

//        }

//    }

//    public void Complete() {
//        IsCompleted=true;
//        comp?.Invoke();
//        comp=null;

//    }

//    public void GetResult() {

//    }
//}


//public class CustomAwaiter<T> : ICriticalNotifyCompletion {

//    public CustomTask<T> task;

//    public CustomAwaiter(CustomTask<T> task) {
//        this.task=task;
//    }

//    public T GetResult() {
//        return task.Result;
//    }

//    public void OnCompleted(Action continuation) {

//    }

//    public void UnsafeOnCompleted(Action continuation) {

//    }
//}