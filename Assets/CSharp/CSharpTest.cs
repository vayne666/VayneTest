using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Profiling;
using UnityEngine.Events;
using UnityEngine.EventSystems;
public class CSharpTest : MonoBehaviour {



    private void Start() {

    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            DelegateTest();
        }
    }

    //delegate void TsetAction();
    void DelegateTest() {
        Profiler.BeginSample("DelegateTest");
        Action t = null;
        //MulticastDelegate del1;
        for (int i = 0; i<10000; i++) {
            t+=() => { };
        }
        t.Invoke();
        Profiler.EndSample();

        Profiler.BeginSample("UnityEvent");
        UnityEvent unityEvent = new UnityEvent();
        for (int i = 0; i<10000; i++) {
            unityEvent.AddListener(() => { });
        }
        unityEvent.Invoke();
        Profiler.EndSample();

    }



    public unsafe void Test() {

    }

}
