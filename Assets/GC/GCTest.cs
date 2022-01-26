using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GCTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log($"start {UnityEngine.Profiling.Profiler.GetMonoHeapSizeLong()}");


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W)) {
            byte[] data = new byte[1024*1024*300];
            Debug.Log($"new {UnityEngine.Profiling.Profiler.GetMonoHeapSizeLong()}");
        }
        if (Input.GetKeyDown(KeyCode.Space)) {
            GC.Collect();
            Debug.Log($"gc:{UnityEngine.Profiling.Profiler.GetMonoHeapSizeLong()}");
        }
    }
}
