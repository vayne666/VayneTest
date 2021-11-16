using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;



public class VTask : MonoBehaviour {

    static List<CustonAwaiter> custonAwaiters = new List<CustonAwaiter>();


    private void Awake() {

    }

    bool isComp = false;
    private void Update() {
        foreach (var item in custonAwaiters) {
            var comp = item.Update();
            if (!isComp) isComp=comp;
        }
        if (isComp) {
            for (int i = custonAwaiters.Count; i>0; i--) {
                if (custonAwaiters[i].IsCompleted) {
                    custonAwaiters.RemoveAt(i);
                }
            }
        }
    }

    public static CustonAwaiter Delay(float time) {
        var task = new DelayTask(time);
        custonAwaiters.Add(task);
        return task;
    }

}



public class DelayTask : CustonAwaiter {

    private float time;
    private float curTime;
    public DelayTask(float time) {
        this.time=time;
    }

    public override bool Update() {
        curTime+=Time.deltaTime;
        if (curTime>=time) {
            this.SetResult();
            return true;
        }
        return false;
    }

}











public interface IAwaiter : ICriticalNotifyCompletion {
    bool IsCompleted { get; }
    void Update();
}

public class VAwaiter : ICriticalNotifyCompletion {

    protected bool isCompleted;
    public bool IsCompleted => isCompleted;
    protected Action comp;
    public void OnCompleted(Action continuation) {
        if (IsCompleted) {
            continuation?.Invoke();
        } else {
            comp+=continuation;
        }
    }

    public void UnsafeOnCompleted(Action continuation) {
        if (IsCompleted) {
            continuation?.Invoke();
        } else {
            comp+=continuation;

        }

    }

    public virtual bool Update() {
        return false;
    }
}


public class CustonAwaiter : VAwaiter {

    public void Complete() {
        comp?.Invoke();
        comp=null;

    }

    public CustonAwaiter GetAwaiter() {
        return this;
    }

    public void SetResult() {
        isCompleted=true;
        comp?.Invoke();
    }

    public void GetResult() {

    }
}

public class CustonAwaiter<T> : VAwaiter {
    T result;
    public CustonAwaiter<T> GetAwaiter() {
        return this;
    }

    public void SetResult(T res) {
        result=res;
        isCompleted=true;
        comp?.Invoke();
    }

    public T GetResult() {
        return result;
    }



}

